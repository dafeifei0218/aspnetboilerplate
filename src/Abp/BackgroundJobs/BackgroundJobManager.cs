using System;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Json;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;
using Newtonsoft.Json;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// Default implementation of <see cref="IBackgroundJobManager"/>.
    /// 后台工作管理类
    /// </summary>
    public class BackgroundJobManager : PeriodicBackgroundWorkerBase, IBackgroundJobManager
    {
        /// <summary>
        /// Interval between polling jobs from <see cref="IBackgroundJobStore"/>.
        /// Default value: 5000 (5 seconds).
        /// 工作周期，
        /// 从<see cref="IBackgroundJobStore"/>后台作业存储中轮训作业的间隔。
        /// 默认值：5000（5秒钟）。
        /// </summary>
        public static int JobPollPeriod { get; set; }

        private readonly IIocResolver _iocResolver;
        private readonly IBackgroundJobStore _store;

        /// <summary>
        /// 构造函数
        /// </summary>
        static BackgroundJobManager()
        {
            JobPollPeriod = 5000;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundJobManager"/> class.
        /// 实例化一个新的<see cref="BackgroundJobManager"/>类。
        /// </summary>
        public BackgroundJobManager(
            IIocResolver iocResolver,
            IBackgroundJobStore store,
            AbpTimer timer)
            : base(timer)
        {
            _store = store;
            _iocResolver = iocResolver;

            Timer.Period = JobPollPeriod;
        }

        /// <summary>
        /// 执行任务-异步
        /// </summary>
        /// <typeparam name="TJob"> 工作类型</typeparam>
        /// <typeparam name="TArgs"> 工作参数的类型</typeparam>
        /// <param name="args">参数</param>
        /// <param name="priority">后台工作优先级</param>
        /// <param name="delay">延迟</param>
        /// <returns></returns>
        public async Task EnqueueAsync<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJob<TArgs>
        {
            var jobInfo = new BackgroundJobInfo
            {
                JobType = typeof(TJob).AssemblyQualifiedName,
                JobArgs = args.ToJsonString(),
                Priority = priority
            };

            if (delay.HasValue)
            {
                jobInfo.NextTryTime = Clock.Now.Add(delay.Value);
            }

            await _store.InsertAsync(jobInfo);
        }

        /// <summary>
        /// 做工作
        /// </summary>
        protected override void DoWork()
        {
            //是PeriodicBackgroundWorkerBase一个派生类，
            //其具体实现了DoWork方法：从BackgroundJobStore（可以自定义实现从数据库中读取）取最多1000个BackgroundJobInfo，
            //然后反射执行BackgroundJobInfo中定义的任务。
            var waitingJobs = AsyncHelper.RunSync(() => _store.GetWaitingJobsAsync(1000));

            foreach (var job in waitingJobs)
            {
                TryProcessJob(job);
            }
        }

        /// <summary>
        /// 尝试处理工作
        /// </summary>
        /// <param name="jobInfo">后台工作信息</param>
        private void TryProcessJob(BackgroundJobInfo jobInfo)
        {
            try
            {
                jobInfo.TryCount++;
                jobInfo.LastTryTime = Clock.Now;

                var jobType = Type.GetType(jobInfo.JobType);
                using (var job = _iocResolver.ResolveAsDisposable(jobType))
                {
                    try
                    {
                        var jobExecuteMethod = job.Object.GetType().GetMethod("Execute");
                        var argsType = jobExecuteMethod.GetParameters()[0].ParameterType;
                        var argsObj = JsonConvert.DeserializeObject(jobInfo.JobArgs, argsType);

                        jobExecuteMethod.Invoke(job.Object, new[] { argsObj });

                        AsyncHelper.RunSync(() => _store.DeleteAsync(jobInfo));
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn(ex.Message, ex);

                        var nextTryTime = jobInfo.CalculateNextTryTime();
                        if (nextTryTime.HasValue)
                        {
                            jobInfo.NextTryTime = nextTryTime.Value;
                        }
                        else
                        {
                            jobInfo.IsAbandoned = true;
                        }

                        TryUpdate(jobInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);

                jobInfo.IsAbandoned = true;

                TryUpdate(jobInfo);
            }
        }

        /// <summary>
        /// 尝试修改
        /// </summary>
        /// <param name="jobInfo">后台工作信息</param>
        private void TryUpdate(BackgroundJobInfo jobInfo)
        {
            try
            {
                _store.UpdateAsync(jobInfo);
            }
            catch (Exception updateEx)
            {
                Logger.Warn(updateEx.ToString(), updateEx);
            }
        }
    }
}

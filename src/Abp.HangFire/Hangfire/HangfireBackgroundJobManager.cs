using System;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Hangfire.Configuration;
using Abp.Threading.BackgroundWorkers;
using Hangfire;
using HangfireBackgroundJob = Hangfire.BackgroundJob;

namespace Abp.Hangfire
{
    /// <summary>
    /// Hangfire后台工作管理类
    /// </summary>
    public class HangfireBackgroundJobManager : BackgroundWorkerBase, IBackgroundJobManager
    {
        /// <summary>
        /// 后台工作配置
        /// </summary>
        private readonly IBackgroundJobConfiguration _backgroundJobConfiguration;
        /// <summary>
        /// Hangfire配置
        /// </summary>
        private readonly IAbpHangfireConfiguration _hangfireConfiguration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="backgroundJobConfiguration">后台工作配置</param>
        /// <param name="hangfireConfiguration">Hangfire配置</param>
        public HangfireBackgroundJobManager(
            IBackgroundJobConfiguration backgroundJobConfiguration, 
            IAbpHangfireConfiguration hangfireConfiguration)
        {
            _backgroundJobConfiguration = backgroundJobConfiguration;
            _hangfireConfiguration = hangfireConfiguration;
        }

        /// <summary>
        /// 开始
        /// </summary>
        public override void Start()
        {
            base.Start();

            if (_hangfireConfiguration.Server == null && _backgroundJobConfiguration.IsJobExecutionEnabled)
            {
                _hangfireConfiguration.Server = new BackgroundJobServer();
            }
        }

        /// <summary>
        /// 等待停止
        /// </summary>
        public override void WaitToStop()
        {
            if (_hangfireConfiguration.Server != null)
            {
                try
                {
                    _hangfireConfiguration.Server.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.ToString(), ex);
                }
            }

            base.WaitToStop();
        }

        /// <summary>
        /// 异步队列
        /// </summary>
        /// <typeparam name="TJob">工作类型</typeparam>
        /// <typeparam name="TArgs">参数类型</typeparam>
        /// <param name="args">参数</param>
        /// <param name="priority">后台工作优先级</param>
        /// <param name="delay">延迟</param>
        /// <returns></returns>
        public Task EnqueueAsync<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal,
            TimeSpan? delay = null) where TJob : IBackgroundJob<TArgs>
        {
            HangfireBackgroundJob.Enqueue<TJob>(job => job.Execute(args));
            return Task.FromResult(0);
        }
    }
}

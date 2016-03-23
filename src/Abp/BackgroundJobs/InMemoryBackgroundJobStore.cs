using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Abp.Timing;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// In memory implementation of <see cref="IBackgroundJobStore"/>.
    /// It's used if <see cref="IBackgroundJobStore"/> is not implemented by actual persistent store
    /// and job execution is enabled (<see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>) for the application.
    /// 内存后台作业存储类。
    /// </summary>
    public class InMemoryBackgroundJobStore : IBackgroundJobStore
    {
        private readonly Dictionary<long, BackgroundJobInfo> _jobs;
        private long _lastId;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryBackgroundJobStore"/> class.
        /// 初始化<see cref="InMemoryBackgroundJobStore"/>的内存后台作业存储类的一个新实例
        /// </summary>
        public InMemoryBackgroundJobStore()
        {
            _jobs = new Dictionary<long, BackgroundJobInfo>();
        }

        /// <summary>
        /// 插入后台工作-异步
        /// </summary>
        /// <param name="jobInfo">后台工作信息</param>
        /// <returns></returns>
        public Task InsertAsync(BackgroundJobInfo jobInfo)
        {
            jobInfo.Id = Interlocked.Increment(ref _lastId);
            _jobs[jobInfo.Id] = jobInfo;

            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取等待工作
        /// </summary>
        /// <param name="maxResultCount">最大结果数</param>
        /// <returns></returns>
        public Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
        {
            var waitingJobs = _jobs.Values
                .Where(t => !t.IsAbandoned && t.NextTryTime <= Clock.Now)
                .OrderByDescending(t => t.Priority)
                .ThenBy(t => t.TryCount)
                .ThenBy(t => t.NextTryTime)
                .Take(maxResultCount)
                .ToList();

            return Task.FromResult(waitingJobs);
        }

        /// <summary>
        /// 删除工作-异步
        /// </summary>
        /// <param name="jobInfo">后台工作信息</param>
        /// <returns></returns>
        public Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            if (!_jobs.ContainsKey(jobInfo.Id))
            {
                return Task.FromResult(0);
            }

            _jobs.Remove(jobInfo.Id);

            return Task.FromResult(0);
        }

        /// <summary>
        /// 更新工作-异步
        /// </summary>
        /// <param name="jobInfo">后台工作信息</param>
        /// <returns></returns>
        public Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            if (jobInfo.IsAbandoned)
            {
                return DeleteAsync(jobInfo);
            }

            return Task.FromResult(0);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// Null pattern implementation of <see cref="IBackgroundJobStore"/>.
    /// It's used if <see cref="IBackgroundJobStore"/> is not implemented by actual persistent store
    /// and job execution is not enabled (<see cref="IBackgroundJobConfiguration.IsJobExecutionEnabled"/>) for the application.
    /// 空后台工作存储类，实现<see cref="IBackgroundJobStore"/>
    /// </summary>
    public class NullBackgroundJobStore : IBackgroundJobStore
    {
        /// <summary>
        /// 插入后台工作-异步
        /// </summary>
        /// <param name="jobInfo">后台工作信息</param>
        /// <returns></returns>
        public Task InsertAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 获取等待工作
        /// </summary>
        /// <param name="maxResultCount">最大结果数</param>
        /// <returns></returns>
        public Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
        {
            return Task.FromResult(new List<BackgroundJobInfo>());
        }

        /// <summary>
        /// 删除工作-异步
        /// </summary>
        /// <param name="jobInfo">后台工作信息</param>
        /// <returns></returns>
        public Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// 更新工作-异步
        /// </summary>
        /// <param name="jobInfo">后台工作信息</param>
        /// <returns></returns>
        public Task UpdateAsync(BackgroundJobInfo jobInfo)
        {
            return Task.FromResult(0);
        }
    }
}
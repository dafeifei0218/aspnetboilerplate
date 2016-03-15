using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// Defines interface to store/get background jobs.
    /// 后台工作存储接口，定义接口来存储/设置后台工作
    /// </summary>
    public interface IBackgroundJobStore
    {
        /// <summary>
        /// Inserts a background job.
        /// 插入后台工作-异步
        /// </summary>
        /// <param name="jobInfo">Job information. 后台工作信息</param>
        Task InsertAsync(BackgroundJobInfo jobInfo);

        /// <summary>
        /// Gets waiting jobs. It should get jobs based on these:
        /// Conditions: !IsAbandoned && NextTryTime &lt;= Clock.Now.
        /// Order by: Priority DESC, TryCount ASC, NextTryTime ASC.
        /// Maximum result: <see cref="maxResultCount"/>.
        /// 获取等待工作,
        /// 它应该得到工作的基础上这些：
        /// 条件：
        /// 排序：优先级降序，数量升序，下次升序
        /// 最大结果：<see cref="maxResultCount"/>
        /// </summary>
        /// <param name="maxResultCount">Maximum result count. 最大结果数</param>
        Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount);

        /// <summary>
        /// Deletes a job.
        /// 删除工作-异步
        /// </summary>
        /// <param name="jobInfo">Job information. 后台工作信息</param>
        Task DeleteAsync(BackgroundJobInfo jobInfo);

        /// <summary>
        /// Updates a job.
        /// 更新工作-异步
        /// </summary>
        /// <param name="jobInfo">Job information. 后台工作信息</param>
        Task UpdateAsync(BackgroundJobInfo jobInfo);
    }
}
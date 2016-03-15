using System;
using System.Threading.Tasks;
using Abp.Threading.BackgroundWorkers;

namespace Abp.BackgroundJobs
{
    //TODO: Create a non-generic EnqueueAsync extension method to IBackgroundJobManager which takes types as input parameters rather than generic parameters.
    /// <summary>
    /// Defines interface of a job manager.
    /// 后台工作管理类
    /// </summary>
    public interface IBackgroundJobManager : IBackgroundWorker
    {
        /// <summary>
        /// Enqueues a job to be executed.
        /// 执行任务-异步
        /// </summary>
        /// <typeparam name="TJob">Type of the job. 工作类型</typeparam>
        /// <typeparam name="TArgs">Type of the arguments of job. 工作参数的类型</typeparam>
        /// <param name="args">Job arguments. 工作参数</param>
        /// <param name="priority">Job priority. 后台工作优先级</param>
        /// <param name="delay">Job delay (wait duration before first try). 工作延迟（第一次尝试）</param>
        Task EnqueueAsync<TJob, TArgs>(TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJob<TArgs>;
    }
}
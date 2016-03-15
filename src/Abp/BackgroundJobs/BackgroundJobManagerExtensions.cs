using System;
using Abp.Threading;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// Some extension methods for <see cref="IBackgroundJobManager"/>.
    /// 后台作业管理扩展类
    /// </summary>
    public static class BackgroundJobManagerExtensions
    {
        /// <summary>
        /// Enqueues a job to be executed.
        /// 入队的工作被执行
        /// </summary>
        /// <typeparam name="TJob">Type of the job. 工作类型</typeparam>
        /// <typeparam name="TArgs">Type of the arguments of job. 工作参数</typeparam>
        /// <param name="backgroundJobManager">Background job manager reference 后台工作管理类</param>
        /// <param name="args">Job arguments. 工作参数</param>
        /// <param name="priority">Job priority. 后台工作优先级</param>
        /// <param name="delay">Job delay (wait duration before first try). 工作延迟（第一次尝试）</param>
        public static void Enqueue<TJob, TArgs>(this IBackgroundJobManager backgroundJobManager, TArgs args, BackgroundJobPriority priority = BackgroundJobPriority.Normal, TimeSpan? delay = null)
            where TJob : IBackgroundJob<TArgs>
        {
            AsyncHelper.RunSync(() => backgroundJobManager.EnqueueAsync<TJob, TArgs>(args, priority, delay));
        }
    }
}

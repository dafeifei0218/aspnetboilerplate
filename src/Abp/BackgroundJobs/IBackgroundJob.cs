namespace Abp.BackgroundJobs
{
    /// <summary>
    /// Defines interface of a background job.
    /// 定义后台工作接口
    /// </summary>
    public interface IBackgroundJob<in TArgs>
    {
        /// <summary>
        /// Executes the job with the <see cref="args"/>.
        /// 执行这项工作<see cref="args"/>.
        /// </summary>
        /// <param name="args">Job arguments. 工作参数</param>
        void Execute(TArgs args);
    }
}
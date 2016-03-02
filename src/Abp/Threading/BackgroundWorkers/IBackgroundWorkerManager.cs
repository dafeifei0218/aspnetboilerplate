namespace Abp.Threading.BackgroundWorkers
{
    /// <summary>
    /// Used to manage background workers.
    /// 后台工作管理接口
    /// </summary>
    public interface IBackgroundWorkerManager : IRunnable
    {
        /// <summary>
        /// Adds a new worker. Starts the worker immediately if <see cref="IBackgroundWorkerManager"/> has started.
        /// 添加一个工作。开始工作
        /// </summary>
        /// <param name="worker">
        /// The worker. It should be resolved from IOC.
        /// 后台工作。应该从IOC容器解析
        /// </param>
        void Add(IBackgroundWorker worker);
    }
}
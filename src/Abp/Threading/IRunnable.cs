namespace Abp.Threading
{
    /// <summary>
    /// Interface to start/stop self threaded services.
    /// 启动/停止自线程服务的接口。
    /// </summary>
    public interface IRunnable
    {
        /// <summary>
        /// Starts the service.
        /// 启动服务
        /// </summary>
        void Start();

        /// <summary>
        /// Sends stop command to the service.
        /// Service may return immediately and stop asynchronously.
        /// A client should then call <see cref="WaitToStop"/> method to ensure it's stopped.
        /// 停止服务，
        /// 发送停止命令到服务。
        /// 服务可以立即返回并停止异步。
        /// 客户端应该调用<see cref="WaitToStop"/>方法保证它停止。
        /// </summary>
        void Stop();

        /// <summary>
        /// Waits the service to stop.
        /// 等待服务停止
        /// </summary>
        void WaitToStop();
    }
}

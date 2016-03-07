namespace Abp.Threading
{
    /// <summary>
    /// Base implementation of <see cref="IRunnable"/>.
    /// 启动/停止自线程服务的基类，实现<see cref="IRunnable"/>
    /// </summary>
    public abstract class RunnableBase : IRunnable
    {
        /// <summary>
        /// A boolean value to control the running.
        /// 是否运行，一个布尔值来控制运行
        /// </summary>
        public bool IsRunning { get { return _isRunning; } }

        private volatile bool _isRunning;

        /// <summary>
        /// 启动服务
        /// </summary>
        public virtual void Start()
        {
            _isRunning = true;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public virtual void Stop()
        {
            _isRunning = false;
        }

        /// <summary>
        /// 等待服务停止
        /// </summary>
        public virtual void WaitToStop()
        {

        }
    }
}
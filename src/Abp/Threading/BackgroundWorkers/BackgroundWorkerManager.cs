using System;
using System.Collections.Generic;
using Abp.Dependency;

namespace Abp.Threading.BackgroundWorkers
{
    /// <summary>
    /// Implements <see cref="IBackgroundWorkerManager"/>.
    /// 后台工作管理类
    /// </summary>
    public class BackgroundWorkerManager : RunnableBase, IBackgroundWorkerManager, ISingletonDependency, IDisposable
    {
        private readonly IIocResolver _iocResolver;
        private readonly List<IBackgroundWorker> _backgroundJobs;

        /// <summary>
        /// Initializes a new instance of the <see cref="BackgroundWorkerManager"/> class.
        /// 初始化一个新实例<see cref="BackgroundWorkerManager"/>类
        /// </summary>
        public BackgroundWorkerManager(IIocResolver iocResolver)
        {
            _iocResolver = iocResolver;
            _backgroundJobs = new List<IBackgroundWorker>();
        }

        /// <summary>
        /// 启动
        /// </summary>
        public override void Start()
        {
            base.Start();

            _backgroundJobs.ForEach(job => job.Start());
        }

        /// <summary>
        /// 停止
        /// </summary>
        public override void Stop()
        {
            _backgroundJobs.ForEach(job => job.Stop());

            base.WaitToStop();
        }

        /// <summary>
        /// 等待停止
        /// </summary>
        public override void WaitToStop()
        {
            _backgroundJobs.ForEach(job => job.WaitToStop());

            base.WaitToStop();
        }

        /// <summary>
        /// 添加一个工作。开始工作
        /// </summary>
        /// <param name="worker">后台工作</param>
        public void Add(IBackgroundWorker worker)
        {
            _backgroundJobs.Add(worker);

            if (IsRunning)
            {
                worker.Start();
            }
        }

        private bool _isDisposed;

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            _backgroundJobs.ForEach(_iocResolver.Release);
            _backgroundJobs.Clear();
        }
    }
}
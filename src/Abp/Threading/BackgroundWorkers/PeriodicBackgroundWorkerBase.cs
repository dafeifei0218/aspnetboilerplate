using System;
using Abp.Threading.Timers;

namespace Abp.Threading.BackgroundWorkers
{
    /// <summary>
    /// Extends <see cref="BackgroundWorkerBase"/> to add a periodic running Timer. 
    /// 周期后台工作基类，
    /// </summary>
    public abstract class PeriodicBackgroundWorkerBase : BackgroundWorkerBase
    {
        protected readonly AbpTimer Timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodicBackgroundWorkerBase"/> class.
        /// 初始化一个新的<see cref="PeriodicBackgroundWorkerBase"/>类
        /// </summary>
        /// <param name="timer">A timer. 时间</param>
        protected PeriodicBackgroundWorkerBase(AbpTimer timer)
        {
            Timer = timer;
            Timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// 开始
        /// </summary>
        public override void Start()
        {
            base.Start();
            Timer.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public override void Stop()
        {
            Timer.Stop();
            base.Stop();
        }

        /// <summary>
        /// 等待停止
        /// </summary>
        public override void WaitToStop()
        {
            Timer.WaitToStop();
            base.WaitToStop();
        }

        /// <summary>
        /// Handles the Elapsed event of the Timer.
        /// 处理定时器的运行事件
        /// </summary>
        private void Timer_Elapsed(object sender, System.EventArgs e)
        {
            try
            {
                DoWork();
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }

        /// <summary>
        /// Periodic works should be done by implementing this method.
        /// 定期工作应通过实施这一方法
        /// </summary>
        protected abstract void DoWork();
    }
}
using System;
using System.Threading;
using Abp.Dependency;

namespace Abp.Threading.Timers
{
    /// <summary>
    /// A roboust timer implementation that ensures no overlapping occurs. It waits exactly specified <see cref="Period"/> between ticks.
    /// Abp定时器，确保没有发生重叠。
    /// </summary>
    //TODO: Extract interface or make all members virtual to make testing easier.
    /// 提取接口或使所有成员的虚拟化简化测试。
    public class AbpTimer : RunnableBase, ITransientDependency
    {
        /// <summary>
        /// This event is raised periodically according to Period of Timer.
        /// 过期，这个事件是根据定时器周期周期性地提出的。
        /// </summary>
        public event EventHandler Elapsed;

        /// <summary>
        /// Task period of timer (as milliseconds).
        /// 周期，定时器任务周期（毫秒）。
        /// </summary>
        public int Period { get; set; }

        /// <summary>
        /// Indicates whether timer raises Elapsed event on Start method of Timer for once.
        /// Default: False.
        /// 是否开始运行，
        /// 指示是否为一次启动定时器时引发的事件。
        /// 默认：false
        /// </summary>
        public bool RunOnStart { get; set; }

        /// <summary>
        /// This timer is used to perfom the task at spesified intervals.
        /// 任务定时器，这个定时器是用来执行任务指定的间隔。
        /// </summary>
        private readonly Timer _taskTimer;

        /// <summary>
        /// Indicates that whether timer is running or stopped.
        /// 是否运行，指示定时器是否正在运行或停止。
        /// </summary>
        private volatile bool _running;

        /// <summary>
        /// Indicates that whether performing the task or _taskTimer is in sleep mode.
        /// This field is used to wait executing tasks when stopping Timer.
        /// 执行任务，
        /// 指示是否执行任务或任务定时器在睡眠模式。
        /// 这一领域是用来等待执行任务时，停止计时器。
        /// </summary>
        private volatile bool _performingTasks;

        /// <summary>
        /// Creates a new Timer.
        /// 创建一个新的定时器
        /// </summary>
        public AbpTimer()
        {
            _taskTimer = new Timer(TimerCallBack, null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// Creates a new Timer.
        /// 创建一个新的定时器
        /// </summary>
        /// <param name="period">Task period of timer (as milliseconds) 定时器任务周期（毫秒）</param>
        /// <param name="runOnStart">Indicates whether timer raises Elapsed event on Start method of Timer for once 是否开始运行，指示是否为一次启动计时器时引发的事件</param>
        public AbpTimer(int period, bool runOnStart = false)
            : this()
        {
            Period = period;
            RunOnStart = runOnStart;
        }

        /// <summary>
        /// Starts the timer.
        /// 启动定时器
        /// </summary>
        public override void Start()
        {
            if (Period <= 0)
            {
                throw new AbpException("Period should be set before starting the timer!");
            }

            base.Start();

            _running = true;
            _taskTimer.Change(RunOnStart ? 0 : Period, Timeout.Infinite);
        }

        /// <summary>
        /// Stops the timer.
        /// 停止定时器
        /// </summary>
        public override void Stop()
        {
            lock (_taskTimer)
            {
                _running = false;
                _taskTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            base.Stop();
        }

        /// <summary>
        /// Waits the service to stop.
        /// 等待服务停止。
        /// </summary>
        public override void WaitToStop()
        {
            //第二，如何知道一个Timer真正结束了呢？
            //也就是说如何知道一个Timer要执行的任务已经完成（这里定义为A效果），同时timer已失效(这里定义为B效果)？
            //ABP通过stop方法实现B，通过WaitToStop实现A效果。
            //WaitToStop会一直阻塞调用他的线程直到_performingTasks变成false,
            //也就是说Timer要执行的任务已经完成（任务完成时会将_performingTasks设为False，并且释放锁）。
            lock (_taskTimer)
            {
                while (_performingTasks)
                {
                    Monitor.Wait(_taskTimer);
                }
            }

            base.WaitToStop();
        }

        /// <summary>
        /// This method is called by _taskTimer.
        /// 定时器调用，这个方法调用任务定时器
        /// </summary>
        /// <param name="state">Not used argument 不使用参数</param>
        private void TimerCallBack(object state)
        {
            lock (_taskTimer)
            {
                if (!_running || _performingTasks)
                {
                    return;
                }

                // Abp是整个ABP框架实现后台工作的核心类，其实现原理就是通过一个CLR中的timer定时启动执行任务。
                // 这里有两个要点值得留意：
                // 第一，用timer有一个弊端，就是当timer间隔时间内，事件没执行完，timer就会新建一个线程，
                // 从头开始执行这个事件，而上一个线程继续执行，这样就会出现，系统中线程n多，一会儿系统的资源就耗尽了。
                // ABP的解决思路是在执行真正的业务方法之前，通过将timer的duetime设为无限大，从而timer就失效了。
                // 业务方法执行完以后在恢复timer的设置。
                _taskTimer.Change(Timeout.Infinite, Timeout.Infinite);
                _performingTasks = true;
            }

            try
            {
                if (Elapsed != null)
                {
                    Elapsed(this, new EventArgs());
                }
            }
            catch
            {

            }
            finally
            {
                lock (_taskTimer)
                {
                    _performingTasks = false;
                    if (_running)
                    {
                        _taskTimer.Change(Period, Timeout.Infinite);
                    }

                    Monitor.Pulse(_taskTimer);
                }
            }
        }
    }
}
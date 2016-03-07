using System;
using Abp.Threading.Timers;

namespace Abp.Threading.BackgroundWorkers
{
    /// <summary>
    /// Extends <see cref="BackgroundWorkerBase"/> to add a periodic running Timer. 
    /// ���ں�̨�������࣬
    /// </summary>
    public abstract class PeriodicBackgroundWorkerBase : BackgroundWorkerBase
    {
        protected readonly AbpTimer Timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeriodicBackgroundWorkerBase"/> class.
        /// ��ʼ��һ���µ�<see cref="PeriodicBackgroundWorkerBase"/>��
        /// </summary>
        /// <param name="timer">A timer. ʱ��</param>
        protected PeriodicBackgroundWorkerBase(AbpTimer timer)
        {
            Timer = timer;
            Timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// ��ʼ
        /// </summary>
        public override void Start()
        {
            base.Start();
            Timer.Start();
        }

        /// <summary>
        /// ֹͣ
        /// </summary>
        public override void Stop()
        {
            Timer.Stop();
            base.Stop();
        }

        /// <summary>
        /// �ȴ�ֹͣ
        /// </summary>
        public override void WaitToStop()
        {
            Timer.WaitToStop();
            base.WaitToStop();
        }

        /// <summary>
        /// Handles the Elapsed event of the Timer.
        /// ����ʱ���������¼�
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
        /// ���ڹ���Ӧͨ��ʵʩ��һ����
        /// </summary>
        protected abstract void DoWork();
    }
}
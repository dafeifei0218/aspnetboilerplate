namespace Abp.Threading
{
    /// <summary>
    /// Base implementation of <see cref="IRunnable"/>.
    /// ����/ֹͣ���̷߳���Ļ��࣬ʵ��<see cref="IRunnable"/>
    /// </summary>
    public abstract class RunnableBase : IRunnable
    {
        /// <summary>
        /// A boolean value to control the running.
        /// �Ƿ����У�һ������ֵ����������
        /// </summary>
        public bool IsRunning { get { return _isRunning; } }

        private volatile bool _isRunning;

        /// <summary>
        /// ��������
        /// </summary>
        public virtual void Start()
        {
            _isRunning = true;
        }

        /// <summary>
        /// ֹͣ����
        /// </summary>
        public virtual void Stop()
        {
            _isRunning = false;
        }

        /// <summary>
        /// �ȴ�����ֹͣ
        /// </summary>
        public virtual void WaitToStop()
        {

        }
    }
}
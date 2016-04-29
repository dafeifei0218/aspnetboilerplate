using System;

namespace Abp.Utils.Etc
{
    /// <summary>
    /// This class is used to simulate a Disposable that does nothing.
    /// �������࣬�����������ģ��һ�������ٵ�
    /// </summary>
    internal sealed class NullDisposable : IDisposable
    {
        /// <summary>
        /// ʵ��
        /// </summary>
        public static NullDisposable Instance { get { return SingletonInstance; } }
        private static readonly NullDisposable SingletonInstance = new NullDisposable();

        /// <summary>
        /// ���캯��
        /// </summary>
        private NullDisposable()
        {
            
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Dispose()
        {

        }
    }
}
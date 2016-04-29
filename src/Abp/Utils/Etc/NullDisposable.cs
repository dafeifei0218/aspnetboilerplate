using System;

namespace Abp.Utils.Etc
{
    /// <summary>
    /// This class is used to simulate a Disposable that does nothing.
    /// 空销毁类，这个类是用来模拟一个可销毁的
    /// </summary>
    internal sealed class NullDisposable : IDisposable
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static NullDisposable Instance { get { return SingletonInstance; } }
        private static readonly NullDisposable SingletonInstance = new NullDisposable();

        /// <summary>
        /// 构造函数
        /// </summary>
        private NullDisposable()
        {
            
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {

        }
    }
}
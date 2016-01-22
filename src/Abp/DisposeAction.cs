using System;

namespace Abp
{
    /// <summary>
    /// This class can be used to provide an action when
    /// Dipose method is called.
    /// 销毁动作
    /// </summary>
    public class DisposeAction : IDisposable
    {
        private readonly Action _action;

        /// <summary>
        /// Creates a new <see cref="DisposeAction"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="action">Action to be executed when this object is disposed. 销毁动作</param>
        public DisposeAction(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            _action = action;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            _action();
        }
    }
}

using System;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// Used as event arguments on <see cref="IActiveUnitOfWork.Failed"/> event.
    /// 工作单元失败事件参数
    /// </summary>
    public class UnitOfWorkFailedEventArgs : EventArgs
    {
        /// <summary>
        /// Exception that caused failure.
        /// 异常
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkFailedEventArgs"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="exception">Exception that caused failure 异常</param>
        public UnitOfWorkFailedEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}

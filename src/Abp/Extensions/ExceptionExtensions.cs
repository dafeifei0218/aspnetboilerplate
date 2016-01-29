using System;
using System.Runtime.ExceptionServices;

namespace Abp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Exception"/> class.
    /// Exception异常扩展类
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Uses <see cref="ExceptionDispatchInfo.Capture"/> method to re-throws exception
        /// while preserving stack trace.
        /// 重新抛出异常，使用ExceptionDispatchInfo.Capture方法重新抛出异常，同事保留堆栈跟踪
        /// </summary>
        /// <param name="exception">Exception to be re-thrown 异常，被重新抛出的异常</param>
        public static void ReThrow(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }
}
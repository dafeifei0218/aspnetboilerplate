using System;
using System.Runtime.ExceptionServices;

namespace Abp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Exception"/> class.
    /// Exception�쳣��չ��
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Uses <see cref="ExceptionDispatchInfo.Capture"/> method to re-throws exception
        /// while preserving stack trace.
        /// �����׳��쳣��ʹ��ExceptionDispatchInfo.Capture���������׳��쳣��ͬ�±�����ջ����
        /// </summary>
        /// <param name="exception">Exception to be re-thrown �쳣���������׳����쳣</param>
        public static void ReThrow(this Exception exception)
        {
            ExceptionDispatchInfo.Capture(exception).Throw();
        }
    }
}
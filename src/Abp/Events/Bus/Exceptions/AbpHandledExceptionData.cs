using System;

namespace Abp.Events.Bus.Exceptions
{
    /// <summary>
    /// This type of events are used to notify for exceptions handled by ABP infrastructure.
    /// Abp处理异常数据类
    /// </summary>
    public class AbpHandledExceptionData : ExceptionData
    {
        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="exception">Exception object 异常对象</param>
        public AbpHandledExceptionData(Exception exception)
            : base(exception)
        {

        }
    }
}
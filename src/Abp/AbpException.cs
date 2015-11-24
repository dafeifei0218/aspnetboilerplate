using System;
using System.Runtime.Serialization;

namespace Abp
{
    /// <summary>
    /// Base exception type for those are thrown by Abp system for Abp specific exceptions.
    /// Abp异常类
    /// </summary>
    [Serializable]
    public class AbpException : Exception
    {
        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// 构造函数，创建一个新的异常类
        /// </summary>
        public AbpException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="serializationInfo">引发的异常的序列化对象数据</param> 
        /// <param name="context">源或目标的上下文信息</param>
        public AbpException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        public AbpException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpException"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        /// <param name="innerException">Inner exception 导致当前异常的异常</param>
        public AbpException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}

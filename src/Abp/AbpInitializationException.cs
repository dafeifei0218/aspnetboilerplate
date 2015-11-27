using System;
using System.Runtime.Serialization;

namespace Abp
{
    /// <summary>
    /// This exception is thrown if a problem on ABP initialization progress.
    /// Abp初始化过程中印发此异常
    /// </summary>
    [Serializable]
    public class AbpInitializationException : AbpException
    {
        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        public AbpInitializationException()
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// 构造函数用户序列化
        /// </summary>
        /// <param name="serializationInfo">存储对象序列化信息</param>
        /// <param name="context">描述给定的序列化流的源和目标，并提供一个由调用方定义的附加上下文。</param>
        public AbpInitializationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        public AbpInitializationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        /// <param name="innerException">Inner exception 异常</param>
        public AbpInitializationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}

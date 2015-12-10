using System;
using System.Runtime.Serialization;

namespace Abp.Authorization
{
    /// <summary>
    /// This exception is thrown on an unauthorized request.
    /// ABP权限异常
    /// </summary>
    [Serializable]
    public class AbpAuthorizationException : AbpException
    {
        /// <summary>
        /// Creates a new <see cref="AbpAuthorizationException"/> object.
        /// 创建一个AbpAuthorizationException对象
        /// </summary>
        public AbpAuthorizationException()
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpAuthorizationException"/> object.
        /// 创建一个AbpAuthorizationException对象
        /// </summary>
        /// <param name="serializationInfo">序列化信息</param>
        /// <param name="context">序列化流的源和目标上下文</param>
        public AbpAuthorizationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpAuthorizationException"/> object.
        /// 创建一个AbpAuthorizationException对象
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        public AbpAuthorizationException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpAuthorizationException"/> object.
        /// 创建一个AbpAuthorizationException对象
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        /// <param name="innerException">Inner exception 内部异常</param>
        public AbpAuthorizationException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
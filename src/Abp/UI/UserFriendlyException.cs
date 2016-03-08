using System;
using System.Runtime.Serialization;

namespace Abp.UI
{
    /// <summary>
    /// This exception type is directly shown to the user.
    /// TODO: Move to Abp namespace
    /// 用户友好异常
    /// </summary>
    [Serializable]
    public class UserFriendlyException : AbpException
    {
        /// <summary>
        /// Additional information about the exception.
        /// 关于异常的附加信息
        /// </summary>
        public string Details { get; private set; }

        /// <summary>
        /// An arbitrary error code.
        /// 任意错误代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        public UserFriendlyException()
        {

        }

        /// <summary>
        /// Constructor for serializing.
        /// 构造函数用于序列化
        /// </summary>
        /// <param name="serializationInfo">序列化信息</param>
        /// <param name="context">序列化流上下文</param>
        public UserFriendlyException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        public UserFriendlyException(string message)
            : base(message)
        {

        }


        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="code">Error code 错误代码</param>
        /// <param name="message">Exception message 异常信息</param>
        public UserFriendlyException(int code, string message)
            : this(message)
        {
            Code = code;
        }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        /// <param name="details">Additional information about the exception 关于异常的附加信息</param>
        public UserFriendlyException(string message, string details)
            : base(message)
        {
            Details = details;
        }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="code">Error code 错误代码</param>
        /// <param name="message">Exception message 异常信息</param>
        /// <param name="details">Additional information about the exception 关于异常的附加信息</param>
        public UserFriendlyException(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        /// <param name="innerException">Inner exception 内部异常</param>
        public UserFriendlyException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        /// <param name="details">Additional information about the exception 关于异常的附加信息</param>
        /// <param name="innerException">Inner exception 内部异常</param>
        public UserFriendlyException(string message, string details, Exception innerException)
            : base(message, innerException)
        {
            Details = details;
        }
    }
}
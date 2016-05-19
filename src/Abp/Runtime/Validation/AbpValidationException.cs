using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Abp.Logging;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// This exception type is used to throws validation exceptions.
    /// Abp验证异常
    /// </summary>
    [Serializable]
    public class AbpValidationException : AbpException
    {
        /// <summary>
        /// Detailed list of validation errors for this exception.
        /// 此异常的验证错误列表
        /// </summary>
        public List<ValidationResult> ValidationErrors { get; set; }


        /// <summary>
        /// Exception severity.
        /// Default: Warn.
        /// 异常严重程度。
        /// 默认：严重
        /// </summary>
        public LogSeverity Severity { get; set; }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        public AbpValidationException()
        {
            ValidationErrors = new List<ValidationResult>();
            Severity = LogSeverity.Warn;
        }

        /// <summary>
        /// Constructor for serializing.
        /// 构造函数
        /// </summary>
        public AbpValidationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            ValidationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        public AbpValidationException(string message)
            : base(message)
        {
            ValidationErrors = new List<ValidationResult>();
        }


        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        /// <param name="validationErrors">Validation errors 验证错误列表</param>
        public AbpValidationException(string message, List<ValidationResult> validationErrors)
            : base(message)
        {
            ValidationErrors = validationErrors;
        }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="message">Exception message 异常信息</param>
        /// <param name="innerException">Inner exception 内部异常</param>
        public AbpValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            ValidationErrors = new List<ValidationResult>();
        }
    }
}

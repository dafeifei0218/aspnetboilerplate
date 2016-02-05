using System;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Runtime.Validation;
using Castle.Core.Logging;

namespace Abp.Logging
{
    /// <summary>
    /// This class can be used to write logs from somewhere where it's a hard to get a reference to the <see cref="ILogger"/>.
    /// Normally, use <see cref="ILogger"/> with property injection wherever it's possible.
    /// 日志帮助类
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// A reference to the logger.
        /// 日志
        /// </summary>
        public static ILogger Logger { get; private set; }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static LogHelper()
        {
            Logger = IocManager.Instance.IsRegistered(typeof (ILoggerFactory))
                ? IocManager.Instance.Resolve<ILoggerFactory>().Create(typeof(LogHelper))
                : NullLogger.Instance;
        }

        /// <summary>
        /// 日志异常
        /// </summary>
        /// <param name="ex">异常</param>
        public static void LogException(Exception ex)
        {
            LogException(Logger, ex);
        }

        /// <summary>
        /// 日志异常
        /// </summary>
        /// <param name="logger">日志</param>
        /// <param name="ex">异常</param>
        public static void LogException(ILogger logger, Exception ex)
        {
            logger.Error(ex.ToString(), ex);
            LogValidationErrors(ex);
        }

        /// <summary>
        /// 验证错误日志
        /// </summary>
        /// <param name="exception">异常</param>
        private static void LogValidationErrors(Exception exception)
        {
            if (exception is AggregateException && exception.InnerException != null)
            {
                var aggException = exception as AggregateException;
                if (aggException.InnerException is AbpValidationException)
                {
                    exception = aggException.InnerException;
                }
            }

            if (!(exception is AbpValidationException))
            {
                return;
            }

            var validationException = exception as AbpValidationException;
            if (validationException.ValidationErrors.IsNullOrEmpty())
            {
                return;
            }

            Logger.Warn("There are " + validationException.ValidationErrors.Count + " validation errors:");
            foreach (var validationResult in validationException.ValidationErrors)
            {
                var memberNames = "";
                if (validationResult.MemberNames != null && validationResult.MemberNames.Any())
                {
                    memberNames = " (" + string.Join(", ", validationResult.MemberNames) + ")";
                }

                Logger.Warn(validationResult.ErrorMessage + memberNames);
            }
        }
    }
}

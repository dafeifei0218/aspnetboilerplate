using System;

namespace Abp.Web.Models
{
    /// <summary>
    /// Used to determine how ABP should wrap response on the web layer.
    /// 包装结果属性，
    /// 用于确定如何ABP包装web层的响应。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class WrapResultAttribute : Attribute
    {
        /// <summary>
        /// Gets default <see cref="WrapResultAttribute"/>.
        /// 获取默认<see cref="WrapResultAttribute"/>。
        /// </summary>
        public static WrapResultAttribute Default { get { return _default; } }
        private static readonly WrapResultAttribute _default = new WrapResultAttribute();

        /// <summary>
        /// Wrap result on success.
        /// 包装成功的结果。
        /// </summary>
        public bool WrapOnSuccess { get; set; }

        /// <summary>
        /// Wrap result on error.
        /// 包装错误的结果。
        /// </summary>
        public bool WrapOnError { get; set; }

        /// <summary>
        /// Log errors.
        /// Default: true.
        /// 错误日志。
        /// 默认：true
        /// </summary>
        public bool LogError { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WrapResultAttribute"/> class.
        /// 实例化一个<see cref="WrapResultAttribute"/>类
        /// </summary>
        /// <param name="wrapOnSuccess">Wrap result on success. 包装成功的结果</param>
        /// <param name="wrapOnError">Wrap result on error. 包装错误的结果</param>
        public WrapResultAttribute(bool wrapOnSuccess = true, bool wrapOnError = true)
        {
            WrapOnSuccess = wrapOnSuccess;
            WrapOnError = wrapOnError;

            LogError = true;
        }
    }
}

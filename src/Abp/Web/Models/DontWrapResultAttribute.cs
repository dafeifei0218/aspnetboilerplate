using System;

namespace Abp.Web.Models
{
    /// <summary>
    /// A shortcut for <see cref="WrapResultAttribute"/> to disable wrapping by default.
    /// It sets false to <see cref="WrapResultAttribute.WrapOnSuccess"/> and <see cref="WrapResultAttribute.WrapOnError"/>  properties.
    /// 禁用默认的包装。
    /// 如果设置为false，<see cref="WrapResultAttribute.WrapOnSuccess"/>和<see cref="WrapResultAttribute.WrapOnError"/>属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class DontWrapResultAttribute : WrapResultAttribute
    {
        /// <summary>
        /// Gets default <see cref="DontWrapResultAttribute"/>.
        /// 获取默认<see cref="DontWrapResultAttribute"/>.
        /// </summary>
        public new static DontWrapResultAttribute Default { get { return _default; } }
        private static readonly DontWrapResultAttribute _default = new DontWrapResultAttribute();

        /// <summary>
        /// Initializes a new instance of the <see cref="DontWrapResultAttribute"/> class.
        /// 实例化一个<see cref="DontWrapResultAttribute"/>类
        /// </summary>
        public DontWrapResultAttribute()
            : base(false, false)
        {

        }
    }
}
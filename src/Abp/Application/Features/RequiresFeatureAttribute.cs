using System;

namespace Abp.Application.Features
{
    /// <summary>
    /// This attribute can be used on a class/method to declare that given class/method is available
    /// only if required feature(s) are enabled.
    /// 要求功能自定义属性，
    /// 此属性可用于类/方法声明，如果所需的功能启用，该类/方法是可用的。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequiresFeatureAttribute : Attribute
    {
        /// <summary>
        /// A list of features to be checked if they are enabled.
        /// 功能列表，如果启用，检查功能列表
        /// </summary>
        public string[] Features { get; private set; }

        /// <summary>
        /// If this property is set to true, all of the <see cref="Features"/> must be enabled.
        /// If it's false, at least one of the <see cref="Features"/> must be enabled.
        /// Default: false.
        /// 必须全部启用，
        /// true：所有必须启用
        /// false：至少一个必须启用
        /// 默认为：false
        /// </summary>
        public bool RequiresAll { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="RequiresFeatureAttribute"/> class.
        /// 构造函数
        /// </summary>
        /// <param name="features">A list of features to be checked if they are enabled 功能集合</param>
        public RequiresFeatureAttribute(params string[] features)
        {
            Features = features;
        }
    }
}
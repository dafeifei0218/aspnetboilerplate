using System.Collections.Generic;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// 值验证器接口
    /// </summary>
    public interface IValueValidator
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 所引器
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        object this[string key] { get; set; }

        /// <summary>
        /// 属性字典
        /// </summary>
        IDictionary<string, object> Attributes { get; }

        /// <summary>
        /// 是否有效
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        bool IsValid(object value);
    }
}
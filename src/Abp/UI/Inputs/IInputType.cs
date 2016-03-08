using System.Collections.Generic;
using Abp.Runtime.Validation;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// 输入类型接口
    /// </summary>
    public interface IInputType
    {
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 所引器
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        object this[string key] { get; set; }

        /// <summary>
        /// 属性字典
        /// </summary>
        IDictionary<string, object> Attributes { get; }

        /// <summary>
        /// 验证
        /// </summary>
        IValueValidator Validator { get; set; }
    }
}
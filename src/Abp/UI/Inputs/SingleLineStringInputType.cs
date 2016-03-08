using System;
using Abp.Runtime.Validation;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// 单行字符串输入类型
    /// </summary>
    [Serializable]
    [InputType("SINGLE_LINE_STRING")]
    public class SingleLineStringInputType : InputTypeBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SingleLineStringInputType()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="validator">验证器</param>
        public SingleLineStringInputType(IValueValidator validator)
            : base(validator)
        {
        }
    }
}
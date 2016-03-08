using System;
using Abp.Runtime.Validation;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// 多选复选框输入类型
    /// </summary>
    [Serializable]
    [InputType("CHECKBOX")]
    public class CheckboxInputType : InputTypeBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public CheckboxInputType()
            : this(new BooleanValueValidator())
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="validator"></param>
        public CheckboxInputType(IValueValidator validator)
            : base(validator)
        {
            
        }
    }
}
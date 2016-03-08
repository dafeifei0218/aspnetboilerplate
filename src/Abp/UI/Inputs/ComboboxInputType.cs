using System;
using Abp.Runtime.Validation;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// Combobox value UI type.
    /// 下拉框输入类型
    /// </summary>
    [Serializable]
    [InputType("COMBOBOX")]
    public class ComboboxInputType : InputTypeBase
    {
        /// <summary>
        /// 项目源，本地化下拉框项目源
        /// </summary>
        public ILocalizableComboboxItemSource ItemSource { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ComboboxInputType()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="itemSource">项目源</param>
        public ComboboxInputType(ILocalizableComboboxItemSource itemSource)
        {
            ItemSource = itemSource;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="itemSource">项目源</param>
        /// <param name="validator">验证器</param>
        public ComboboxInputType(ILocalizableComboboxItemSource itemSource, IValueValidator validator)
            : base(validator)
        {
            ItemSource = itemSource;
        }
    }
}
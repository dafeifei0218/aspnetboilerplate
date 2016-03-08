using System;
using Abp.Localization;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// 本地化下拉框项目
    /// </summary>
    [Serializable]
    public class LocalizableComboboxItem : ILocalizableComboboxItem
    {
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        public ILocalizableString DisplayText { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public LocalizableComboboxItem()
        {
            
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="displayText">显示文本</param>
        public LocalizableComboboxItem(string value, ILocalizableString displayText)
        {
            Value = value;
            DisplayText = displayText;
        }
    }
}
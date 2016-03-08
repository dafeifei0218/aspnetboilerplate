using System;
using Abp.Localization;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// ���ػ���������Ŀ
    /// </summary>
    [Serializable]
    public class LocalizableComboboxItem : ILocalizableComboboxItem
    {
        /// <summary>
        /// ֵ
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// ��ʾ�ı�
        /// </summary>
        public ILocalizableString DisplayText { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        public LocalizableComboboxItem()
        {
            
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="value">ֵ</param>
        /// <param name="displayText">��ʾ�ı�</param>
        public LocalizableComboboxItem(string value, ILocalizableString displayText)
        {
            Value = value;
            DisplayText = displayText;
        }
    }
}
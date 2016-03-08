using System;
using Abp.Runtime.Validation;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// Combobox value UI type.
    /// ��������������
    /// </summary>
    [Serializable]
    [InputType("COMBOBOX")]
    public class ComboboxInputType : InputTypeBase
    {
        /// <summary>
        /// ��ĿԴ�����ػ���������ĿԴ
        /// </summary>
        public ILocalizableComboboxItemSource ItemSource { get; set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        public ComboboxInputType()
        {

        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="itemSource">��ĿԴ</param>
        public ComboboxInputType(ILocalizableComboboxItemSource itemSource)
        {
            ItemSource = itemSource;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="itemSource">��ĿԴ</param>
        /// <param name="validator">��֤��</param>
        public ComboboxInputType(ILocalizableComboboxItemSource itemSource, IValueValidator validator)
            : base(validator)
        {
            ItemSource = itemSource;
        }
    }
}
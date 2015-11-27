using System;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This DTO can be used as a simple item for a combobox/list.
    /// ��Ͽ�/�б����Ŀ���ݴ������
    /// </summary>
    [Serializable]
    public class ComboboxItemDto : IDto
    {
        /// <summary>
        /// Value of the item.
        /// ��Ŀֵ
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Display text of the item.
        /// ��Ŀ��ʾ�ı�
        /// </summary>
        public string DisplayText { get; set; }

        /// <summary>
        /// Is selected?
        /// �Ƿ�ѡ��
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Creates a new <see cref="ComboboxItemDto"/>.
        /// ���캯��
        /// </summary>
        public ComboboxItemDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="ComboboxItemDto"/>.
        /// ���캯��
        /// </summary>
        /// <param name="value">Value of the item��Ŀֵ </param>
        /// <param name="displayText">Display text of the item ��Ŀ��ʾ�ı�</param>
        public ComboboxItemDto(string value, string displayText)
        {
            Value = value;
            DisplayText = displayText;
        }
    }
}
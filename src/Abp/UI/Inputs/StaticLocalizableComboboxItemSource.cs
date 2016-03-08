using System;
using System.Collections.Generic;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// ��̬���ػ�������ĿԴ
    /// </summary>
    public class StaticLocalizableComboboxItemSource : ILocalizableComboboxItemSource
    {
        /// <summary>
        /// ��Ŀ
        /// </summary>
        public ICollection<ILocalizableComboboxItem> Items { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="items">��Ŀ</param>
        public StaticLocalizableComboboxItemSource(params ILocalizableComboboxItem[] items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (items.Length <= 0)
            {
                throw new ArgumentException("Items can not be empty!");
            }

            Items = items;
        }
    }
}
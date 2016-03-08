using System;
using System.Collections.Generic;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// 静态本地化下拉项目源
    /// </summary>
    public class StaticLocalizableComboboxItemSource : ILocalizableComboboxItemSource
    {
        /// <summary>
        /// 项目
        /// </summary>
        public ICollection<ILocalizableComboboxItem> Items { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="items">项目</param>
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
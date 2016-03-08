using System.Collections.Generic;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// 本地化下拉框项目源接口
    /// </summary>
    public interface ILocalizableComboboxItemSource
    {
        /// <summary>
        /// 项目
        /// </summary>
        ICollection<ILocalizableComboboxItem> Items { get; }
    }
}
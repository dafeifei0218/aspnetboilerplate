using System.Collections.Generic;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// Declares common interface for classes those have menu items.
    /// 菜单项定义
    /// </summary>
    public interface IHasMenuItemDefinitions
    {
        /// <summary>
        /// List of menu items.
        /// 菜单项列表
        /// </summary>
        IList<MenuItemDefinition> Items { get; }
    }
}
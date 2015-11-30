using System.Collections.Generic;
using System.Linq;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// Extension methods for <see cref="MenuItemDefinition"/>.
    /// 菜单项定义扩展方法
    /// </summary>
    public static class MenuItemDefinitionExtensions
    {
        /// <summary>
        /// Moves a menu item to top in the list.
        /// 移动菜单项到列表头部
        /// </summary>
        /// <param name="menuItems">List of menu items 菜单列表</param>
        /// <param name="menuItemName">Name of the menu item to move 菜单项名称</param>
        public static void MoveMenuItemToTop(this IList<MenuItemDefinition> menuItems, string menuItemName)
        {
            var menuItem = GetMenuItem(menuItems, menuItemName);
            menuItems.Remove(menuItem);
            menuItems.Insert(0, menuItem);
        }

        /// <summary>
        /// Moves a menu item to bottom in the list.
        /// 移动菜单项到列表底部
        /// </summary>
        /// <param name="menuItems">List of menu items 菜单列表</param>
        /// <param name="menuItemName">Name of the menu item to move 菜单项名称</param>
        public static void MoveMenuItemToBottom(this IList<MenuItemDefinition> menuItems, string menuItemName)
        {
            var menuItem = GetMenuItem(menuItems, menuItemName);
            menuItems.Remove(menuItem);
            menuItems.Insert(menuItems.Count, menuItem);
        }

        /// <summary>
        /// Moves a menu item in the list after another menu item in the list.
        /// 移动菜单项到目标菜单项之前
        /// </summary>
        /// <param name="menuItems">List of menu items 菜单列表</param>
        /// <param name="menuItemName">Name of the menu item to move 菜单项名称</param>
        /// <param name="targetMenuItemName">Target menu item (to move before it) 目标菜单项（移动到此菜单项之前）</param>
        public static void MoveMenuItemBefore(this IList<MenuItemDefinition> menuItems, string menuItemName, string targetMenuItemName)
        {
            var menuItem = GetMenuItem(menuItems, menuItemName);
            var targetMenuItem = GetMenuItem(menuItems, targetMenuItemName);
            menuItems.Remove(menuItem);
            menuItems.Insert(menuItems.IndexOf(targetMenuItem), menuItem);
        }

        /// <summary>
        /// Moves a menu item in the list before another menu item in the list.
        /// 移动菜单项到目标菜单项之后
        /// </summary>
        /// <param name="menuItems">List of menu items 菜单列表</param>
        /// <param name="menuItemName">Name of the menu item to move 菜单项名称</param>
        /// <param name="targetMenuItemName">Target menu item (to move after it) 目标菜单项（移动到此菜单项之后）</param>
        public static void MoveMenuItemAfter(this IList<MenuItemDefinition> menuItems, string menuItemName, string targetMenuItemName)
        {
            var menuItem = GetMenuItem(menuItems, menuItemName);
            var targetMenuItem = GetMenuItem(menuItems, targetMenuItemName);
            menuItems.Remove(menuItem);
            menuItems.Insert(menuItems.IndexOf(targetMenuItem) + 1, menuItem);
        }

        /// <summary>
        /// 获取菜单项
        /// </summary>
        /// <param name="menuItems">菜单列表</param>
        /// <param name="menuItemName">菜单项名称</param>
        /// <returns></returns>
        private static MenuItemDefinition GetMenuItem(IEnumerable<MenuItemDefinition> menuItems, string menuItemName)
        {
            var menuItem = menuItems.FirstOrDefault(i => i.Name == menuItemName);
            if (menuItem == null)
            {
                throw new AbpException("Can not find menu item: " + menuItemName);
            }

            return menuItem;
        }
    }
}
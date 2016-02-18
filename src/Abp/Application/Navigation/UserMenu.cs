using System.Collections.Generic;
using Abp.Localization;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// Represents a menu shown to the user.
    /// 代表用户显示的菜单
    /// </summary>
    public class UserMenu
    {
        /// <summary>
        /// Unique name of the menu in the application. 
        /// 菜单名称（在应用程序中的菜单的唯一名称）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name of the menu.
        /// 菜单显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// A custom object related to this menu item.
        /// 自定义数据（与此菜单项相关的自定义对象）
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// Menu items (first level).
        /// 菜单项
        /// </summary>
        public IList<UserMenuItem> Items { get; set; }

        /// <summary>
        /// Creates a new <see cref="UserMenu"/> object.
        /// 构造函数
        /// </summary>
        public UserMenu()
        {
            
        }

        /// <summary>
        /// Creates a new <see cref="UserMenu"/> object from given <see cref="MenuDefinition"/>.
        /// 构造函数
        /// <param name="menuDefinition"></param>
        /// </summary>
        internal UserMenu(MenuDefinition menuDefinition, ILocalizationContext localizationContext)
        {
            Name = menuDefinition.Name;
            DisplayName = menuDefinition.DisplayName.Localize(localizationContext);
            CustomData = menuDefinition.CustomData;
            Items = new List<UserMenuItem>();
        }
    }
}
using System.Collections.Generic;
using Abp.Localization;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// Represents a menu shown to the user.
    /// 表示一个用户显示的菜单
    /// </summary>
    /// <remarks>
    /// UserMenu/UserMenuItem：封装了用于显示给用户的菜单/以及子菜单集合。
    /// ABP通过MenuDefinition/MenuItemDefinition构成了完整的系统菜单集合（超集）。
    /// 而UserMenu/UserMenuItem只构成用户所能访问的菜单集合，并且其DisplayName是本地化以后的DisplayName。
    /// </remarks>
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
        /// 菜单项（第一级）
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
        /// <param name="menuDefinition">菜单定义</param>
        /// <param name="localizationContext">本地化上下文</param>
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
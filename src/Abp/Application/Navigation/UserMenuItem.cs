using System.Collections.Generic;
using Abp.Localization;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// Represents an item in a <see cref="UserMenu"/>.
    /// 用户菜单的项，表示<see cref="UserMenu"/>中的一个菜单项
    /// </summary>
    public class UserMenuItem
    {
        /// <summary>
        /// Unique name of the menu item in the application. 
        /// 菜单名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Icon of the menu item if exists.
        /// 菜单图标（如果存在）
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// Display name of the menu item.
        /// 菜单项显示名称
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// The Display order of the menu. Optional.
        /// 菜单项的显示顺序，可选
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// The URL to navigate when this menu item is selected.
        /// 如果菜单项被选中，导航的链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// A custom object related to this menu item.
        /// 自定义数据，与菜单项关联的自定义对象。
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// Sub items of this menu item.
        /// 该菜单项的子项集合
        /// </summary>
        public IList<UserMenuItem> Items { get; private set; }

        /// <summary>
        /// Creates a new <see cref="UserMenuItem"/> object.
        /// 构造函数
        /// </summary>
        public UserMenuItem()
        {
            
        }

        /// <summary>
        /// Creates a new <see cref="UserMenuItem"/> object from given <see cref="MenuItemDefinition"/>.
        /// 构造函数
        /// </summary>
        /// <param name="menuItemDefinition">菜单项定义</param>
        /// <param name="localizationContext">本地化上下文</param>
        internal UserMenuItem(MenuItemDefinition menuItemDefinition,ILocalizationContext localizationContext)
        {
            Name = menuItemDefinition.Name;
            Icon = menuItemDefinition.Icon;
            DisplayName = menuItemDefinition.DisplayName.Localize(localizationContext);
            Order = menuItemDefinition.Order;
            Url = menuItemDefinition.Url;
            CustomData = menuItemDefinition.CustomData;
            Items = new List<UserMenuItem>();
        }
    }
}
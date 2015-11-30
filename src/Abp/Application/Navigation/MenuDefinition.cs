using System;
using System.Collections.Generic;
using Abp.Localization;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// Represents a navigation menu for an application.
    /// 菜单定义
    /// </summary>
    public class MenuDefinition : IHasMenuItemDefinitions
    {
        /// <summary>
        /// Unique name of the menu in the application. Required.
        /// 菜单名称。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Display name of the menu. Required.
        /// 菜单显示名称，必填。
        /// </summary>
        public ILocalizableString DisplayName { get; set; }

        /// <summary>
        /// Can be used to store a custom object related to this menu. Optional.
        /// 自定义数据，可选。
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// Menu items (first level).
        /// 菜单项
        /// </summary>
        public IList<MenuItemDefinition> Items { get; set; }

        /// <summary>
        /// Creates a new <see cref="MenuDefinition"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="name">Unique name of the menu 菜单名称</param>
        /// <param name="displayName">Display name of the menu 菜单显示名称</param>
        /// <param name="customData">Can be used to store a custom object related to this menu. 自定义数据</param>
        public MenuDefinition(string name, ILocalizableString displayName, object customData = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name", "Menu name can not be empty or null.");
            }

            if (displayName == null)
            {
                throw new ArgumentNullException("displayName", "Display name of the menu can not be null.");
            }

            Name = name;
            DisplayName = displayName;
            CustomData = customData;

            Items = new List<MenuItemDefinition>();
        }

        /// <summary>
        /// Adds a <see cref="MenuItemDefinition"/> to <see cref="Items"/>.
        /// 添加菜单项
        /// </summary>
        /// <param name="menuItem"><see cref="MenuItemDefinition"/> to be added 菜单项定义</param>
        /// <returns>This <see cref="MenuDefinition"/> object</returns>
        public MenuDefinition AddItem(MenuItemDefinition menuItem)
        {
            Items.Add(menuItem);
            return this;
        }
    }
}

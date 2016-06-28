using System;
using System.Collections.Generic;
using Abp.Application.Features;
using Abp.Collections.Extensions;
using Abp.Localization;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// Represents an item in a <see cref="MenuDefinition"/>.
    /// 菜单项定义，表示<see cref="MenuDefinition"/>中的一项
    /// </summary>
    public class MenuItemDefinition : IHasMenuItemDefinitions
    {
        /// <summary>
        /// Unique name of the menu item in the application. 
        /// Can be used to find this menu item later.
        /// 菜单名称。
        /// 可用于查找该菜单项
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Display name of the menu item. Required.
        /// 菜单显示名称，必填
        /// </summary>
        public ILocalizableString DisplayName { get; set; }
        
        /// <summary>
        /// The Display order of the menu. Optional.
        /// 菜单的显示顺序，可选
        /// </summary>
        public int Order { get; set; }
        
        /// <summary>
        /// Icon of the menu item if exists. Optional.
        /// 菜单图标（如果存在），可选
        /// </summary>
        public string Icon { get; set; }
        
        /// <summary>
        /// The URL to navigate when this menu item is selected. Optional.
        /// 当菜单被选中时，导航的链接，可选
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// A permission name. Only users that has this permission can see this menu item.
        /// Optional.
        /// 权限名称，只有当用户拥有此权限时，才能看到这个菜单项，可选。
        /// </summary>
        public string RequiredPermissionName { get; set; }

        /// <summary>
        /// A feature dependency.
        /// Optional.
        /// 特征依赖。
        /// 可选。
        /// </summary>
        public IFeatureDependency FeatureDependency { get; set; }

        /// <summary>
        /// This can be set to true if only authenticated users should see this menu item.
        /// 必须认证，只有授权用户才能看到该菜单项，将其设置为true。
        /// </summary>
        public bool RequiresAuthentication { get; set; }

        /// <summary>
        /// Returns true if this menu item has no child <see cref="Items"/>.
        /// 是否末节点，如果该菜单项没有<see cref="Items"/>子菜单项，则返回true
        /// </summary>
        public bool IsLeaf
        {
            get { return Items.IsNullOrEmpty(); }
        }

        /// <summary>
        /// Can be used to store a custom object related to this menu item. Optional.
        /// 自定义数据，可用于存储与该菜单项关联的自字义数据[可选]
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// Sub items of this menu item. Optional.
        /// 该菜单项的子项，可选
        /// </summary>
        public virtual IList<MenuItemDefinition> Items { get; private set; }

        /// <summary>
        /// Creates a new <see cref="MenuItemDefinition"/> object.
        /// 构造函数
        /// </summary>
        public MenuItemDefinition(
            string name, 
            ILocalizableString displayName, 
            string icon = null, 
            string url = null, 
            bool requiresAuthentication = false, 
            string requiredPermissionName = null, 
            int order = 0, 
            object customData = null,
            IFeatureDependency featureDependency = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (displayName == null)
            {
                throw new ArgumentNullException("displayName");
            }

            Name = name;
            DisplayName = displayName;
            Icon = icon;
            Url = url;
            RequiresAuthentication = requiresAuthentication;
            RequiredPermissionName = requiredPermissionName;
            Order = order;
            CustomData = customData;
            FeatureDependency = featureDependency;

            Items = new List<MenuItemDefinition>();
        }

        /// <summary>
        /// Adds a <see cref="MenuItemDefinition"/> to <see cref="Items"/>.
        /// 添加菜单
        /// </summary>
        /// <param name="menuItem"><see cref="MenuItemDefinition"/> to be added. 菜单项定义</param>
        /// <returns>This <see cref="MenuItemDefinition"/> object. 返回菜单项</returns>
        public MenuItemDefinition AddItem(MenuItemDefinition menuItem)
        {
            Items.Add(menuItem);
            return this;
        }
    }
}

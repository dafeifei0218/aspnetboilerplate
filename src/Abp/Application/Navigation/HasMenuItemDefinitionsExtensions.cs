using System;
using Abp.Collections.Extensions;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// Defines extension methods for <see cref="IHasMenuItemDefinitions"/>.
    /// 菜单项定义扩展方法
    /// </summary>
    public static class HasMenuItemDefinitionsExtensions
    {
        /// <summary>
        /// Searches and gets a <see cref="MenuItemDefinition"/> by it's unique name.
        /// Throws exception if can not find.
        /// 根据菜单名称获取项目。
        /// 如果未找到，返回异常
        /// </summary>
        /// <param name="source">Source object 数据源</param>
        /// <param name="name">Unique name of the source 菜单项名称</param>
        public static MenuItemDefinition GetItemByName(this IHasMenuItemDefinitions source, string name)
        {
            var item = GetItemByNameOrNull(source, name);
            if (item == null)
            {
                throw new ArgumentException("There is no source item with given name: " + name, "name");
            }

            return item;
        }

        /// <summary>
        /// Searches all menu items (recursively) in the source and gets a <see cref="MenuItemDefinition"/> by it's unique name.
        /// Returns null if can not find.
        /// 使用递归方式根据菜单名称获取项目。
        /// 如果未找到，返回null
        /// </summary>
        /// <param name="source">Source object 数据源</param>
        /// <param name="name">Unique name of the source 菜单项名称</param>
        public static MenuItemDefinition GetItemByNameOrNull(this IHasMenuItemDefinitions source, string name)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Items.IsNullOrEmpty())
            {
                return null;
            }

            foreach (var subItem in source.Items)
            {
                if (subItem.Name == name)
                {
                    return subItem;
                }

                var subItemSearchResult = GetItemByNameOrNull(subItem, name);
                if (subItemSearchResult != null)
                {
                    return subItemSearchResult;
                }
            }

            return null;
        }
    }
}
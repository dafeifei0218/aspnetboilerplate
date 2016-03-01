using System;
using Abp.Collections.Extensions;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// Defines extension methods for <see cref="IHasMenuItemDefinitions"/>.
    /// �˵������չ����
    /// </summary>
    public static class HasMenuItemDefinitionsExtensions
    {
        /// <summary>
        /// Searches and gets a <see cref="MenuItemDefinition"/> by it's unique name.
        /// Throws exception if can not find.
        /// ���ݲ˵����ƻ�ȡ��Ŀ��
        /// ���δ�ҵ��������쳣
        /// </summary>
        /// <param name="source">Source object ����Դ</param>
        /// <param name="name">Unique name of the source �˵�������</param>
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
        /// ʹ�õݹ鷽ʽ���ݲ˵����ƻ�ȡ��Ŀ��
        /// ���δ�ҵ�������null
        /// </summary>
        /// <param name="source">Source object ����Դ</param>
        /// <param name="name">Unique name of the source �˵�������</param>
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
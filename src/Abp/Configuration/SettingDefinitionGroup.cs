using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Localization;

namespace Abp.Configuration
{
    /// <summary>
    /// A setting group is used to group some settings togehter.
    /// A group can be child of another group and can has child groups.
    /// 设置定义组
    /// </summary>
    public class SettingDefinitionGroup
    {
        /// <summary>
        /// Unique name of the setting group.
        /// 设置组名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Display name of the setting.
        /// This can be used to show setting to the user.
        /// 设置组显示名称
        /// </summary>
        public ILocalizableString DisplayName { get; private set; }

        /// <summary>
        /// Gets parent of this group.
        /// 获取设置组的上级
        /// </summary>
        public SettingDefinitionGroup Parent { get; private set; }

        /// <summary>
        /// Gets a list of all children of this group.
        /// 获取子设置组列表
        /// </summary>
        public IReadOnlyList<SettingDefinitionGroup> Children
        {
            get { return _children.ToImmutableList(); }
        }
        private readonly List<SettingDefinitionGroup> _children;

        /// <summary>
        /// Creates a new <see cref="SettingDefinitionGroup"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="name">Unique name of the setting group 设置组名称</param>
        /// <param name="displayName">Display name of the setting 设置组显示名称</param>
        public SettingDefinitionGroup(string name, ILocalizableString displayName)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name parameter is invalid! It can not be null or empty or whitespace", "name"); //TODO: Simpify throwing such exceptions
            }

            Name = name;
            DisplayName = displayName;
            _children = new List<SettingDefinitionGroup>();
        }

        /// <summary>
        /// Adds a <see cref="SettingDefinitionGroup"/> as child of this group.
        /// 添加子设置组
        /// </summary>
        /// <param name="child">Child to be added 子设置组</param>
        /// <returns>This child group to be able to add more child</returns>
        public SettingDefinitionGroup AddChild(SettingDefinitionGroup child)
        {
            if (child.Parent != null)
            {
                throw new AbpException("Setting group " + child.Name + " has already a Parent (" + child.Parent.Name + ").");
            }

            _children.Add(child);
            child.Parent = this;
            return this;
        }
    }
}
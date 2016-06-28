using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Application.Features;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Abp.Authorization
{
    /// <summary>
    /// Represents a permission.
    /// A permission is used to restrict functionalities of the application from unauthorized users.
    /// 权限类。
    /// 一个权限是用来限制应用程序的功能，从未经授权的用户。
    /// </summary>
    /// <remarks>
    /// 用于定义一个Permission，一个Permission可以包含多个子Permission.
    /// </remarks>
    public sealed class Permission
    {
        /// <summary>
        /// Parent of this permission if one exists.
        /// If set, this permission can be granted only if parent is granted.
        /// 赋权限
        /// </summary>
        public Permission Parent { get; private set; }

        /// <summary>
        /// Unique name of the permission.
        /// This is the key name to grant permissions.
        /// 权限名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Display name of the permission.
        /// This can be used to show permission to the user.
        /// 权限显示名称
        /// </summary>
        public ILocalizableString DisplayName { get; set; }

        /// <summary>
        /// A brief description for this permission.
        /// 权限描述
        /// </summary>
        public ILocalizableString Description { get; set; }

        /// <summary>
        /// Is this permission granted by default.
        /// Default value: false.
        /// 是否授予权限
        /// 默认值：false 不授予
        /// </summary>
        public bool IsGrantedByDefault { get; set; }

        /// <summary>
        /// Which side can use this permission.
        /// 多租户双方，哪一方可以使用此权限
        /// </summary>
        public MultiTenancySides MultiTenancySides { get; set; }

        /// <summary>
        /// Depended feature(s) of this permission.
        /// 此权限的依赖特征
        /// </summary>
        public IFeatureDependency FeatureDependency { get; set; }

        /// <summary>
        /// List of child permissions. A child permission can be granted only if parent is granted.
        /// 子权限
        /// </summary>
        public IReadOnlyList<Permission> Children
        {
            get { return _children.ToImmutableList(); }
        }
        private readonly List<Permission> _children;

        /// <summary>
        /// Creates a new Permission.
        /// 构造函数
        /// </summary>
        /// <param name="name">Unique name of the permission 权限名称</param>
        /// <param name="displayName">Display name of the permission 权限显示名称</param>
        /// <param name="isGrantedByDefault">Is this permission granted by default. Default value: false. 是否授予权限</param>
        /// <param name="description">A brief description for this permission 权限描述</param>
        /// <param name="multiTenancySides">Which side can use this permission 多租户双方，哪一方可以使用此权限</param>
        /// <param name="featureDependency">Depended feature(s) of this permission 此权限的依赖特征</param>
        public Permission(
            string name,
            ILocalizableString displayName = null,
            bool isGrantedByDefault = false,
            ILocalizableString description = null,
            MultiTenancySides multiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant,
            IFeatureDependency featureDependency = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
            DisplayName = displayName;
            IsGrantedByDefault = isGrantedByDefault;
            Description = description;
            MultiTenancySides = multiTenancySides;
            FeatureDependency = featureDependency;

            _children = new List<Permission>();
        }

        /// <summary>
        /// Adds a child permission.
        /// A child permission can be granted only if parent is granted.
        /// 添加子权限
        /// </summary>
        /// <returns>Returns newly created child permission</returns>
        public Permission CreateChildPermission(
            string name, 
            ILocalizableString displayName = null, 
            bool isGrantedByDefault = false, 
            ILocalizableString description = null, 
            MultiTenancySides multiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant,
            IFeatureDependency featureDependency = null)
        {
            var permission = new Permission(name, displayName, isGrantedByDefault, description, multiTenancySides, featureDependency) { Parent = this };
            _children.Add(permission);
            return permission;
        }

        public override string ToString()
        {
            return string.Format("[Permission: {0}]", Name);
        }
    }
}

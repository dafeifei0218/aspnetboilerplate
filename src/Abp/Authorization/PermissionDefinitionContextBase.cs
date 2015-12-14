using Abp.Application.Features;
using Abp.Collections.Extensions;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Abp.Authorization
{
    /// <summary>
    /// 权限定义上下文基类
    /// </summary>
    internal abstract class PermissionDefinitionContextBase : IPermissionDefinitionContext
    {
        /// <summary>
        /// 权限字典
        /// </summary>
        protected readonly PermissionDictionary Permissions;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected PermissionDefinitionContextBase()
        {
            Permissions = new PermissionDictionary();
        }

        /// <summary>
        /// 创建权限
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <param name="displayName">权限显示名称</param>
        /// <param name="isGrantedByDefault"></param>
        /// <param name="description">权限描述</param>
        /// <param name="multiTenancySides">多租户</param>
        /// <param name="featureDependency">特征依赖</param>
        /// <returns></returns>
        public Permission CreatePermission(
            string name, 
            ILocalizableString displayName = null, 
            bool isGrantedByDefault = false, 
            ILocalizableString description = null, 
            MultiTenancySides multiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant,
            IFeatureDependency featureDependency = null)
        {
            //权限字典中是否包含指定的键
            if (Permissions.ContainsKey(name))
            {
                //权限字典中已经包含这个权限，名称为：
                throw new AbpException("There is already a permission with name: " + name);
            }

            var permission = new Permission(name, displayName, isGrantedByDefault, description, multiTenancySides, featureDependency);
            Permissions[permission.Name] = permission;
            return permission;
        }

        /// <summary>
        /// 获取给定权限名称的权限，如果没有给定名称的权限返回null
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <returns></returns>
        public Permission GetPermissionOrNull(string name)
        {
            return Permissions.GetOrDefault(name);
        }
    }
}

using Abp.Application.Features;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Abp.Authorization
{
    /// <summary>
    /// This context is used on <see cref="AuthorizationProvider.SetPermissions"/> method.
    /// 权限定义上下文接口
    /// </summary>
    public interface IPermissionDefinitionContext
    {
        /// <summary>
        /// Creates a new permission under this group.
        /// 创建权限
        /// </summary>
        /// <param name="name">Unique name of the permission 权限名称</param>
        /// <param name="displayName">Display name of the permission 权限显示名称</param>
        /// <param name="isGrantedByDefault">Is this permission granted by default. Default value: false. 默认是否授权，默认值：false</param>
        /// <param name="description">A brief description for this permission 权限描述</param>
        /// <param name="multiTenancySides">Which side can use this permission 多租户双方，哪一方可以使用权限</param>
        /// <param name="featureDependency">Depended feature(s) of this permission 依赖的特征，这个权限依赖的特征</param>
        /// <returns>New created permission 新创建的权限</returns>
        Permission CreatePermission(
            string name, 
            ILocalizableString displayName, 
            bool isGrantedByDefault = false, 
            ILocalizableString description = null, 
            MultiTenancySides multiTenancySides = MultiTenancySides.Host | MultiTenancySides.Tenant,
            IFeatureDependency featureDependency = null
            );

        /// <summary>
        /// Gets a permission with given name or null if can not find.
        /// 获取给定权限名称的权限，如果没有给定名称的权限返回null
        /// </summary>
        /// <param name="name">Unique name of the permission 权限名称</param>
        /// <returns>Permission object or null 权限对象或null</returns>
        Permission GetPermissionOrNull(string name);
    }
}
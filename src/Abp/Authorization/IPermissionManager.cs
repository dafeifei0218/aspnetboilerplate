using System.Collections.Generic;
using Abp.MultiTenancy;

namespace Abp.Authorization
{
    /// <summary>
    /// Permission manager.
    /// 权限管理类
    /// </summary>
    public interface IPermissionManager
    {
        /// <summary>
        /// Gets <see cref="Permission"/> object with given <paramref name="name"/> or throws exception
        /// if there is no permission with given <paramref name="name"/>.
        /// 获取给定权限名称的权限，如果没有给定名称的权限引发异常
        /// </summary>
        /// <param name="name">Unique name of the permission 权限名称</param>
        Permission GetPermission(string name);

        /// <summary>
        /// Gets <see cref="Permission"/> object with given <paramref name="name"/> or returns null
        /// if there is no permission with given <paramref name="name"/>.
        /// 获取给定权限名称的权限，如果没有给定名称的权限返回null
        /// </summary>
        /// <param name="name">Unique name of the permission 权限名称</param>
        Permission GetPermissionOrNull(string name);

        /// <summary>
        /// Gets all permissions.
        /// 获取全部权限
        /// </summary>
        /// <param name="tenancyFilter">Can be passed false to disable tenancy filter.</param>
        IReadOnlyList<Permission> GetAllPermissions(bool tenancyFilter = true);

        /// <summary>
        /// Gets all permissions.
        /// 获取全部权限
        /// </summary>
        /// <param name="multiTenancySides">Multi-tenancy side to filter 多租户过滤</param>
        IReadOnlyList<Permission> GetAllPermissions(MultiTenancySides multiTenancySides);
    }
}

using System.Collections.Generic;
using System.Collections.Immutable;

namespace Abp.Authorization
{
    /// <summary>
    /// This class is used to get permissions out of the system.
    /// Normally, you should inject and use <see cref="IPermissionManager"/> and use it.
    /// This class can be used in database migrations or in unit tests where Abp is not initialized.
    /// 权限查找器
    /// </summary>
    public static class PermissionFinder
    {
        /// <summary>
        /// Collects and gets all permissions in given providers.
        /// This method can be used to get permissions in database migrations or in unit tests where Abp is not initialized.
        /// Otherwise, use <see cref="IPermissionManager.GetAllPermissions(bool)"/> method.
        /// 获取全部权限
        /// 收集并获取给定供应商的所有权限。
        /// 该方法可用于在数据库迁移或在单元测试中，ABP未初始化获取权限。
        /// 否则，使用IPermissionmanager.GetAllPermissions(bool)方法。
        /// </summary>
        /// <param name="authorizationProviders">Authorization providers 授权提供者数组</param>
        /// <returns>List of permissions 返回权限列表</returns>
        /// <remarks>
        /// This method creates instances of <see cref="authorizationProviders"/> by order and
        /// calls <see cref="AuthorizationProvider.SetPermissions"/> to build permission list.
        /// So, providers should not use dependency injection.
        /// </remarks>
        public static IReadOnlyList<Permission> GetAllPermissions(params AuthorizationProvider[] authorizationProviders)
        {
            return new InternalPermissionFinder(authorizationProviders).GetAllPermissions();
        }

        /// <summary>
        /// 内部权限查找器
        /// </summary>
        internal class InternalPermissionFinder : PermissionDefinitionContextBase
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="authorizationProviders">授权提供者数组</param>
            public InternalPermissionFinder(params AuthorizationProvider[] authorizationProviders)
            {
                foreach (var provider in authorizationProviders)
                {
                    provider.SetPermissions(this);
                }

                Permissions.AddAllPermissions();
            }

            /// <summary>
            /// 获取全部权限
            /// </summary>
            /// <returns></returns>
            public IReadOnlyList<Permission> GetAllPermissions()
            {
                return Permissions.Values.ToImmutableList();
            }
        }
    }
}
using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// Null (and default) implementation of <see cref="IPermissionChecker"/>.
    /// 空（默认）权限检查实现
    /// </summary>
    public sealed class NullPermissionChecker : IPermissionChecker
    {
        /// <summary>
        /// Singleton instance.
        /// 单例实例。
        /// </summary>
        public static NullPermissionChecker Instance { get { return SingletonInstance; } }
        private static readonly NullPermissionChecker SingletonInstance = new NullPermissionChecker();

        /// <summary>
        /// 检查用户是否授予权限-异步
        /// </summary>
        /// <param name="permissionName">权限名称</param>
        /// <returns></returns>
        public Task<bool> IsGrantedAsync(string permissionName)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// 检查用户是否授予权限-异步
        /// </summary>
        /// <param name="userId">Id of the user to check 用户Id</param>
        /// <param name="permissionName">Name of the permission 权限名称</param>
        /// <returns><c>true</c> if this instance is granted the specified userId permissionName; otherwise, <c>false</c>.</returns>
        public Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// 私有的构造函数
        /// </summary>
        private NullPermissionChecker()
        {

        }
    }
}
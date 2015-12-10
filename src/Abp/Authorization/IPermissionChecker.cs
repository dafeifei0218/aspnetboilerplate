using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// This class is used to permissions for users.
    /// 权限检查接口
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        /// Checks if current user is granted for a permission.
        /// 检查用户是否授予权限-异步
        /// </summary>
        /// <param name="permissionName">Name of the permission 权限名称</param>
        Task<bool> IsGrantedAsync(string permissionName);

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// 检查用户是否授予权限-异步
        /// </summary>
        /// <param name="userId">Id of the user to check 用户Id</param>
        /// <param name="permissionName">Name of the permission 权限名称</param>
        Task<bool> IsGrantedAsync(long userId, string permissionName);
    }
}
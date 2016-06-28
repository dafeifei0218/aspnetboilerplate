using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// This class is used to permissions for users.
    /// 权限检查接口
    /// </summary>
    /// <remarks>
    /// 定义了实际上用于完成permission check的方法，其实现一般都要访问数据库的。
    /// 所以在ABP底层框架中只有一个其dummy的实现-NullPermissionChecker。
    /// </remarks>
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
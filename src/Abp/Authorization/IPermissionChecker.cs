using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// This class is used to permissions for users.
    /// Ȩ�޼��ӿ�
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        /// Checks if current user is granted for a permission.
        /// ����û��Ƿ�����Ȩ��-�첽
        /// </summary>
        /// <param name="permissionName">Name of the permission Ȩ������</param>
        Task<bool> IsGrantedAsync(string permissionName);

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// ����û��Ƿ�����Ȩ��-�첽
        /// </summary>
        /// <param name="userId">Id of the user to check �û�Id</param>
        /// <param name="permissionName">Name of the permission Ȩ������</param>
        Task<bool> IsGrantedAsync(long userId, string permissionName);
    }
}
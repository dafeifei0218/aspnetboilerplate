using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// Null (and default) implementation of <see cref="IPermissionChecker"/>.
    /// �գ�Ĭ�ϣ�Ȩ�޼��ʵ��
    /// </summary>
    public sealed class NullPermissionChecker : IPermissionChecker
    {
        /// <summary>
        /// Singleton instance.
        /// ����ʵ����
        /// </summary>
        public static NullPermissionChecker Instance { get { return SingletonInstance; } }
        private static readonly NullPermissionChecker SingletonInstance = new NullPermissionChecker();

        /// <summary>
        /// ����û��Ƿ�����Ȩ��-�첽
        /// </summary>
        /// <param name="permissionName">Ȩ������</param>
        /// <returns></returns>
        public Task<bool> IsGrantedAsync(string permissionName)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// ����û��Ƿ�����Ȩ��-�첽
        /// </summary>
        /// <param name="userId">Id of the user to check �û�Id</param>
        /// <param name="permissionName">Name of the permission Ȩ������</param>
        /// <returns><c>true</c> if this instance is granted the specified userId permissionName; otherwise, <c>false</c>.</returns>
        public Task<bool> IsGrantedAsync(long userId, string permissionName)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// ˽�еĹ��캯��
        /// </summary>
        private NullPermissionChecker()
        {

        }
    }
}
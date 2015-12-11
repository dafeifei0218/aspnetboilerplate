using System;
using System.Threading.Tasks;
using Abp.Collections.Extensions;
using Abp.Threading;

namespace Abp.Authorization
{
    /// <summary>
    /// Extension methods for <see cref="IPermissionChecker"/>
    /// Ȩ�޼����չ����
    /// </summary>
    public static class PermissionCheckerExtensions
    {
        /// <summary>
        /// Checks if current user is granted for a permission.
        /// �Ƿ���Ȩ������Ȩ�����Ƽ�鵱ǰ�û��Ƿ������Ȩ��
        /// </summary>
        /// <param name="permissionChecker">Permission checker Ȩ�޼��</param>
        /// <param name="permissionName">Name of the permission Ȩ������</param>
        public static bool IsGranted(this IPermissionChecker permissionChecker, string permissionName)
        {
            return AsyncHelper.RunSync(() => permissionChecker.IsGrantedAsync(permissionName));
        }

        /// <summary>
        /// Checks if a user is granted for a permission.
        /// �Ƿ���Ȩ�������û�Id��Ȩ�����Ƽ�鵱ǰ�û��Ƿ������Ȩ��
        /// </summary>
        /// <param name="permissionChecker">Permission checker Ȩ�޼��</param>
        /// <param name="userId">Id of the user to check</param>
        /// <param name="permissionName">Name of the permission Ȩ������</param>
        public static bool IsGranted(this IPermissionChecker permissionChecker, long userId, string permissionName)
        {
            return AsyncHelper.RunSync(() => permissionChecker.IsGrantedAsync(userId, permissionName));
        }

        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="AbpAuthorizationException"/> if not authorized.
        /// User it authorized if any of the <see cref="permissionNames"/> are granted.
        /// ��Ȩ�����赱ǰ�û�Ȩ��
        /// </summary>
        /// <param name="permissionChecker">Permission checker Ȩ�޼��</param>
        /// <param name="permissionNames">Name of the permissions to authorize ��Ȩ��Ȩ����������</param>
        /// <exception cref="AbpAuthorizationException">Throws authorization exception if</exception>
        public static void Authorize(this IPermissionChecker permissionChecker, params string[] permissionNames)
        {
            Authorize(permissionChecker, false, permissionNames);
        }

        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="AbpAuthorizationException"/> if not authorized.
        /// User it authorized if any of the <see cref="permissionNames"/> are granted.
        /// ��Ȩ
        /// </summary>
        /// <param name="permissionChecker">Permission checker Ȩ�޼��</param>
        /// <param name="requireAll">
        /// If this is set to true, all of the <see cref="permissionNames"/> must be granted.
        /// If it's false, at least one of the <see cref="permissionNames"/> must be granted.
        /// </param>
        /// <param name="permissionNames">Name of the permissions to authorize</param>
        /// <exception cref="AbpAuthorizationException">Throws authorization exception if</exception>
        public static void Authorize(this IPermissionChecker permissionChecker, bool requireAll, params string[] permissionNames)
        {
            AsyncHelper.RunSync(() => AuthorizeAsync(permissionChecker, requireAll, permissionNames));
        }

        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="AbpAuthorizationException"/> if not authorized.
        /// User it authorized if any of the <see cref="permissionNames"/> are granted.
        /// ��Ȩ-�첽
        /// </summary>
        /// <param name="permissionChecker">Permission checker Ȩ�޼��</param>
        /// <param name="permissionNames">Name of the permissions to authorize ��Ȩ��Ȩ����������</param>
        /// <exception cref="AbpAuthorizationException">Throws authorization exception if</exception>
        public static Task AuthorizeAsync(this IPermissionChecker permissionChecker, params string[] permissionNames)
        {
            return AuthorizeAsync(permissionChecker, false, permissionNames);
        }

        /// <summary>
        /// Authorizes current user for given permission or permissions,
        /// throws <see cref="AbpAuthorizationException"/> if not authorized.
        /// ��Ȩ-�첽
        /// </summary>
        /// <param name="permissionChecker">Permission checker Ȩ�޼��</param>
        /// <param name="requireAll">
        /// If this is set to true, all of the <see cref="permissionNames"/> must be granted.
        /// If it's false, at least one of the <see cref="permissionNames"/> must be granted.
        /// true��ȫ��Ȩ�ޱ�����Ȩ
        /// false������һ��Ȩ�ޱ�����Ȩ
        /// </param>
        /// <param name="permissionNames">Name of the permissions to authorize ��Ȩ��Ȩ����������</param>
        /// <exception cref="AbpAuthorizationException">Throws authorization exception if</exception>
        public static async Task AuthorizeAsync(this IPermissionChecker permissionChecker, bool requireAll, params string[] permissionNames)
        {
            if (permissionNames.IsNullOrEmpty())
            {
                return;
            }

            if (requireAll)
            {
                foreach (var permissionName in permissionNames)
                {
                    if (!(await permissionChecker.IsGrantedAsync(permissionName)))
                    {
                        throw new AbpAuthorizationException(
                            "Required permissions are not granted. All of these permissions must be granted: " +
                            String.Join(", ", permissionNames)
                            );
                    }
                }
            }
            else
            {
                foreach (var permissionName in permissionNames)
                {
                    if (await permissionChecker.IsGrantedAsync(permissionName))
                    {
                        return;
                    }
                }

                throw new AbpAuthorizationException(
                    "Required permissions are not granted. At least one of these permissions must be granted: " +
                    String.Join(", ", permissionNames)
                    );
            }
        }
    }
}
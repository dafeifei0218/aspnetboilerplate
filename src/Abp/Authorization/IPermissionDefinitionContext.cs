using Abp.Application.Features;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Abp.Authorization
{
    /// <summary>
    /// This context is used on <see cref="AuthorizationProvider.SetPermissions"/> method.
    /// Ȩ�޶��������Ľӿ�
    /// </summary>
    public interface IPermissionDefinitionContext
    {
        /// <summary>
        /// Creates a new permission under this group.
        /// ����Ȩ��
        /// </summary>
        /// <param name="name">Unique name of the permission Ȩ������</param>
        /// <param name="displayName">Display name of the permission Ȩ����ʾ����</param>
        /// <param name="isGrantedByDefault">Is this permission granted by default. Default value: false. Ĭ���Ƿ���Ȩ��Ĭ��ֵ��false</param>
        /// <param name="description">A brief description for this permission Ȩ������</param>
        /// <param name="multiTenancySides">Which side can use this permission ���⻧˫������һ������ʹ��Ȩ��</param>
        /// <param name="featureDependency">Depended feature(s) of this permission ���������������Ȩ������������</param>
        /// <returns>New created permission �´�����Ȩ��</returns>
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
        /// ��ȡ����Ȩ�����Ƶ�Ȩ�ޣ����û�и������Ƶ�Ȩ�޷���null
        /// </summary>
        /// <param name="name">Unique name of the permission Ȩ������</param>
        /// <returns>Permission object or null Ȩ�޶����null</returns>
        Permission GetPermissionOrNull(string name);
    }
}
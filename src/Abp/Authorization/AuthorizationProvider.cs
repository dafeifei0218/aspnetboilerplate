using Abp.Dependency;

namespace Abp.Authorization
{
    /// <summary>
    /// This is the main interface to define permissions for an application.
    /// Implement it to define permissions for your module.
    /// ��Ȩ�ṩ�ߣ�
    /// ���Ƕ���Ӧ�ó����Ȩ�޵���Ҫ�ӿڡ�ʵ����Ϊ����ģ�鶨��Ȩ�ޡ�
    /// </summary>
    /// <remarks>
    /// ����������FeatureProvider��
    /// ������࣬��������PermissionManager��PermissionDictionary��
    /// Abp���ֻ�ṩ�˳����࣬���������һ���򵥵�ʾ����
    /// ʵ����Ŀ�п��Դ����Զ���AuthorizationProvider�������ݿ��ж�ȡPermission��Ϣ����䵽PermissionManager�����С�
    /// </remarks>
    public abstract class AuthorizationProvider : ITransientDependency
    {
        /// <summary>
        /// This method is called once on application startup to allow to define permissions.
        /// ����Ȩ�ޣ�
        /// Ӧ�ó�������ʱ������Ȩ�ޡ�
        /// </summary>
        /// <param name="context">Permission definition context Ȩ�޶���������</param>
        public abstract void SetPermissions(IPermissionDefinitionContext context);
    }
}
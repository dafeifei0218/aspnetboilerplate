using Abp.Dependency;

namespace Abp.Authorization
{
    /// <summary>
    /// This is the main interface to define permissions for an application.
    /// Implement it to define permissions for your module.
    /// 授权提供者，
    /// 这是定义应用程序的权限的主要接口。实现它为您的模块定义权限。
    /// </summary>
    public abstract class AuthorizationProvider : ITransientDependency
    {
        /// <summary>
        /// This method is called once on application startup to allow to define permissions.
        /// 设置权限，
        /// 应用程序启动时允许定义权限。
        /// </summary>
        /// <param name="context">Permission definition context 权限定义上下文</param>
        public abstract void SetPermissions(IPermissionDefinitionContext context);
    }
}
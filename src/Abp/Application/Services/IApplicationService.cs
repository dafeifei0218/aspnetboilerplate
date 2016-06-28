using Abp.Dependency;

namespace Abp.Application.Services
{
    /// <summary>
    /// This interface must be implemented by all application services to identify them by convention.
    /// 应用服务接口，此接口必须由所有应用程序服务来实现，以确定它们的约定。
    /// </summary>
    /// <remarks>
    /// 空接口，起标识作用。所有实现了IApplicationService的类都会被自动注入到容器中。
    /// 同时所有IApplicationService对象都会被注入一些拦截器（例如：Auditing, UnitOfWork）以实现AOP。
    /// </remarks>
    public interface IApplicationService : ITransientDependency
    {

    }
}

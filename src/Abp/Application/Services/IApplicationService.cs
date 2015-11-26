using Abp.Dependency;

namespace Abp.Application.Services
{
    /// <summary>
    /// This interface must be implemented by all application services to identify them by convention.
    /// 应用服务接口，此接口必须由所有应用程序服务来实现，以确定它们的约定。
    /// </summary>
    public interface IApplicationService : ITransientDependency
    {

    }
}

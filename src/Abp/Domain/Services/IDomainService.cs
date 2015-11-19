using Abp.Dependency;

namespace Abp.Domain.Services
{
    /// <summary>
    /// This interface must be implemented by all domain services to identify them by convention.
    /// 领域服务接口
    /// </summary>
    public interface IDomainService : ITransientDependency
    {

    }
}
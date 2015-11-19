using Abp.Dependency;

namespace Abp.Domain.Repositories
{
    /// <summary>
    /// This interface must be implemented by all repositories to identify them by convention.
    /// Implement generic version instead of this one.
    /// ²Ö´¢½Ó¿Ú
    /// </summary>
    public interface IRepository : ITransientDependency
    {
        
    }
}
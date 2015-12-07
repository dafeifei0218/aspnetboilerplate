using System.Threading.Tasks;

namespace Abp.Application.Features
{
    /// <summary>
    /// Defines a feature dependency.
    /// 功能依赖
    /// </summary>
    public interface IFeatureDependency
    {
        /// <summary>
        /// Checks depended features and returns true if dependencies are satisfied.
        /// 如果依赖关系，检查功能和返回是否为true
        /// </summary>
        Task<bool> IsSatisfiedAsync(IFeatureDependencyContext context);
    }
}
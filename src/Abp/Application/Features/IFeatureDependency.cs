using System.Threading.Tasks;

namespace Abp.Application.Features
{
    /// <summary>
    /// Defines a feature dependency.
    /// 功能依赖
    /// </summary>
    /// <remarks>
    /// IFeatureDependency/SimpleFeatureDependency：
    /// 如果某项功能要先进行Feature检查，可以加上一个IFeatureDependency属性。
    /// IFeatureDependency对象通过调用IFeatureChecker对象完成真正的检查。
    /// 具体用例，可查看MenuItemDefinition和UserNavigationManager的用法。
    /// </remarks>
    public interface IFeatureDependency
    {
        /// <summary>
        /// Checks depended features and returns true if dependencies are satisfied.
        /// 如果依赖关系，检查功能和返回是否为true
        /// </summary>
        Task<bool> IsSatisfiedAsync(IFeatureDependencyContext context);
    }
}
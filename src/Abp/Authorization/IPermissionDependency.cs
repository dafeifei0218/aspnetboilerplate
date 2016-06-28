using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// Defines interface to check a dependency.
    /// 定义一个用于检查权限的接口。
    /// </summary>
    /// <remarks>
    /// 定义了一个用于check permission方法的接口。
    /// SimplePermissionDependency是其一个最简单的实现。
    /// 其可以用作为其他对象的一个属性，以帮助其他对象得到检查Permission的能力。
    /// 比如NotificationDefinition定义了一个IPermissionDependency类型的属性。
    /// 当ABP获取某个用户可见的Notification种类时，可以通过NotificationDefinition的IPermissionDependency类型的属性去检查用户是否对该类Notification有接受权限。
    /// </remarks>
    public interface IPermissionDependency
    {
        /// <summary>
        /// Checks if permission dependency is satisfied.
        /// 是否满足，
        /// 检查权限依赖是否满足。
        /// </summary>
        /// <param name="context">Context. 权限依赖上下文</param>
        Task<bool> IsSatisfiedAsync(IPermissionDependencyContext context);
    }
}
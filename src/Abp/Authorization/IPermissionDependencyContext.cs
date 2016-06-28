using Abp.Application.Features;
using Abp.Dependency;

namespace Abp.Authorization
{
    /// <summary>
    /// Permission dependency context.
    /// 权限依赖上下文
    /// </summary>
    /// <remarks>
    /// 上下文类，作为方法的参数。没有特别的业务逻辑。
    /// </remarks>
    public interface IPermissionDependencyContext
    {
        /// <summary>
        /// The user which requires permission.
        /// 用户Id，需要权限的用户。
        /// </summary>
        long? UserId { get; }

        /// <summary>
        /// Gets the <see cref="IIocResolver"/>.
        /// 获取IOC控制反转解析器
        /// </summary>
        /// <value>
        /// The ioc resolver.
        /// </value>
        IIocResolver IocResolver { get; }

        /// <summary>
        /// Gets the <see cref="IFeatureChecker"/>.
        /// 获取权限检查
        /// </summary>
        /// <value>
        /// The feature checker.
        /// </value>
        IPermissionChecker PermissionChecker { get; }
    }
}
using Abp.Dependency;

namespace Abp.Authorization
{
    /// <summary>
    /// 权限依赖上下文
    /// </summary>
    /// <remarks>
    /// 这对接口和实现用于新建一个Permission到PermissionDictionary中，和根据Permission的Name从PermissionDictionary返回一个Permission。
    /// </remarks>
    internal class PermissionDependencyContext : IPermissionDependencyContext, ITransientDependency
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// IOC控制反转解析器
        /// </summary>
        public IIocResolver IocResolver { get; private set; }
        
        /// <summary>
        /// 权限检查
        /// </summary>
        public IPermissionChecker PermissionChecker { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocResolver">IOC控制反转解析器</param>
        public PermissionDependencyContext(IIocResolver iocResolver)
        {
            IocResolver = iocResolver;
            PermissionChecker = NullPermissionChecker.Instance;
        }
    }
}
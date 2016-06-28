using System;
using Abp.Dependency;

namespace Abp.Authorization
{
    /// <summary>
    /// 静态权限检查
    /// </summary>
    /// <remarks>
    /// 用于从容器生成IPermissionChecker接口的实现，
    /// 如果没有自定义的IPermissionChecker实现被注入到容器中则返回NullPermissionChecker。
    /// 这边通过Lazy实现延迟加载。
    /// </remarks>
    internal static class StaticPermissionChecker
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static IPermissionChecker Instance { get { return LazyInstance.Value; } }
        /// <summary>
        /// 延迟实例
        /// </summary>
        private static readonly Lazy<IPermissionChecker> LazyInstance;

        /// <summary>
        /// 构造函数
        /// </summary>
        static StaticPermissionChecker()
        {
            //如果IOC管理类已经注册IPermissionChecker权限检查，则解析IPermissionChecker，否则使用默认权限检查
            LazyInstance = new Lazy<IPermissionChecker>(
                () => IocManager.Instance.IsRegistered<IPermissionChecker>()
                    ? IocManager.Instance.Resolve<IPermissionChecker>()
                    : NullPermissionChecker.Instance
                );
        }
    }
}
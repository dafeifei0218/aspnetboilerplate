using System;
using Abp.Dependency;

namespace Abp.Authorization
{
    /// <summary>
    /// 静态权限检查
    /// </summary>
    internal static class StaticPermissionChecker
    {
        //实例
        public static IPermissionChecker Instance { get { return LazyInstance.Value; } }
        //延迟实例
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
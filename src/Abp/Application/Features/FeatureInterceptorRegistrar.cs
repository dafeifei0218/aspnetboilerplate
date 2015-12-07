using System;
using System.Linq;
using System.Reflection;
using Abp.Dependency;
using Castle.Core;
using Castle.MicroKernel;

namespace Abp.Application.Features
{
    /// <summary>
    /// Used to register <see cref="FeatureInterceptor"/> for needed classes.
    /// 功能拦截器注册类
    /// </summary>
    internal static class FeatureInterceptorRegistrar
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="iocManager">依赖注入管理类</param>
        public static void Initialize(IIocManager iocManager)
        {
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        /// <summary>
        /// 内核组件注册
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="handler">处理程序</param>
        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (ShouldIntercept(handler.ComponentModel.Implementation))
            {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(FeatureInterceptor)));
            }
        }

        /// <summary>
        /// 应该拦截
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static bool ShouldIntercept(Type type)
        {
            if (type.IsDefined(typeof(RequiresFeatureAttribute), true))
            {
                return true;
            }

            if (type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any(m => m.IsDefined(typeof(RequiresFeatureAttribute), true)))
            {
                return true;
            }

            return false;
        }
    }
}

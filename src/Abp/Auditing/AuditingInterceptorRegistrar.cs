using System;
using System.Linq;
using Abp.Dependency;
using Castle.Core;
using Castle.MicroKernel;

namespace Abp.Auditing
{
    /// <summary>
    /// 审计拦截器注册类
    /// </summary>
    internal static class AuditingInterceptorRegistrar
    {
        private static IAuditingConfiguration _auditingConfiguration;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="iocManager">IOC管理类</param>
        public static void Initialize(IIocManager iocManager)
        {
            _auditingConfiguration = iocManager.Resolve<IAuditingConfiguration>();

            if (!_auditingConfiguration.IsEnabled)
            {
                return;
            }

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
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(AuditingInterceptor)));
            }
        }

        /// <summary>
        /// 应该拦截
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static bool ShouldIntercept(Type type)
        {
            if (_auditingConfiguration.Selectors.Any(selector => selector.Predicate(type)))
            {
                return true;
            }

            if (type.IsDefined(typeof(AuditedAttribute), true)) //TODO: true or false?
            {
                return true;
            }

            if (type.GetMethods().Any(m => m.IsDefined(typeof(AuditedAttribute), true))) //TODO: true or false?
            {
                return true;
            }

            return false;
        }
    }
}
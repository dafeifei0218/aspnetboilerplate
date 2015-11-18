using System.Linq;
using System.Reflection;
using Abp.Dependency;
using Castle.Core;
using Castle.MicroKernel;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// This class is used to register interceptor for needed classes for Unit Of Work mechanism.
    /// </summary>
    internal static class UnitOfWorkRegistrar
    {
        /// <summary>
        /// Initializes the registerer.
        /// 初始化注册
        /// </summary>
        /// <param name="iocManager">IOC manager</param>
        public static void Initialize(IIocManager iocManager)
        {
            //该方法会在应用程序启动的时候调用，进行事件注册
            iocManager.IocContainer.Kernel.ComponentRegistered += ComponentRegistered;
        }

        /// <summary>
        /// 拦截注册事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="handler"></param>
        private static void ComponentRegistered(string key, IHandler handler)
        {
            if (UnitOfWorkHelper.IsConventionalUowClass(handler.ComponentModel.Implementation))
            {
                //判断类型是否实现了IRepository或IApplicationService，如果是，则为该类型注册拦截器（UnitOfWorkInterceptor）
                //Intercept all methods of all repositories.
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
            else if (handler.ComponentModel.Implementation.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any(UnitOfWorkHelper.HasUnitOfWorkAttribute))
            {
                //或者类型中任何一个方法上应用了UnitOfWorkAttribute，同时为类型注册拦截器（UnitOfWorkInterceptor）
                //Intercept all methods of classes those have at least one method that has UnitOfWork attribute.
                //TODO: Intecept only UnitOfWork methods, not other methods!
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }
    }
}
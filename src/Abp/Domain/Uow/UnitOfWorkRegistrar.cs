using System.Linq;
using System.Reflection;
using Abp.Dependency;
using Castle.Core;
using Castle.MicroKernel;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// This class is used to register interceptor for needed classes for Unit Of Work mechanism.
    /// 工作单元注册类
    /// </summary>
    /// 通过UnitOfWorkRegistrar将UnitOfWorkInterceptor在某个类被注册到IOCContainner的时候，
    /// 一并添加到该类在容器中对应的ComponentModel的Interceptors集合中。
    /// 总结一句话就是，UOW的功能是通过自定义Castle拦截器来实现的。
    /// 本文主要介绍ABP核心框架中的UnitOfWork的实现，后续会分别介绍ABP其他模块是如何具体实现IUnitOfWork的
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
        /// <param name="key">键</param>
        /// <param name="handler"></param>
        private static void ComponentRegistered(string key, IHandler handler)
        {
            //判断类型是否实现了IRepository或IApplicationService，如果是，则为该类型注册拦截器（UnitOfWorkInterceptor）
            if (UnitOfWorkHelper.IsConventionalUowClass(handler.ComponentModel.Implementation))
            {
                //Intercept all methods of all repositories.
                //拦截全部仓储的全部方法
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
            else if (handler.ComponentModel.Implementation.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any(UnitOfWorkHelper.HasUnitOfWorkAttribute))
            {
                //或者类型中任何一个方法上应用了UnitOfWorkAttribute，同时为类型注册拦截器（UnitOfWorkInterceptor）
                //Intercept all methods of classes those have at least one method that has UnitOfWork attribute.
                //TODO: Intecept only UnitOfWork methods, not other methods!
                //只有拦截UnitOfWork方法，没有其他方法！
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }
    }
}
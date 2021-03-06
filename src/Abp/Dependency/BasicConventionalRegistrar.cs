﻿using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;

namespace Abp.Dependency
{
    /// <summary>
    /// This class is used to register basic dependency implementations such as <see cref="ITransientDependency"/> and <see cref="ISingletonDependency"/>.
    /// 基本常规注册类，
    /// 这个类用来注册所有依赖实现ITransientDependency和ISingletonDependency和IInterceptor接口的类。
    /// </summary>
    public class BasicConventionalRegistrar : IConventionalDependencyRegistrar
    {
        /// <summary>
        /// 注册程序集
        /// </summary>
        /// <param name="context">常规注册上下文</param>
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            //Transient
            //租户
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ITransientDependency>()
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                );

            //Singleton
            //单例
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ISingletonDependency>()
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleSingleton()
                );

            //Windsor Interceptors
            //Windsor拦截器
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<IInterceptor>()
                    .WithService.Self()
                    .LifestyleTransient()
                );
        }
    }
}
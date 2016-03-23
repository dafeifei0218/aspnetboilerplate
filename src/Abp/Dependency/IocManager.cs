using System;
using System.Collections.Generic;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Abp.Dependency
{
    /// <summary>
    /// This class is used to directly perform dependency injection tasks.
    /// IOC控制反转管理类
    /// </summary>
    public class IocManager : IIocManager
    {
        /// <summary>
        /// The Singleton instance.
        /// 单例实例
        /// </summary>
        public static IocManager Instance { get; private set; }

        /// <summary>
        /// Reference to the Castle Windsor Container.
        /// IOC容器
        /// </summary>
        public IWindsorContainer IocContainer { get; private set; }

        /// <summary>
        /// List of all registered conventional registrars.
        /// 常规依赖注册列表
        /// </summary>
        private readonly List<IConventionalDependencyRegistrar> _conventionalRegistrars;

        /// <summary>
        /// 构造函数
        /// </summary>
        static IocManager()
        {
            Instance = new IocManager();
        }

        /// <summary>
        /// Creates a new <see cref="IocManager"/> object.
        /// Normally, you don't directly instantiate an <see cref="IocManager"/>.
        /// This may be useful for test purposes.
        /// 构造函数，
        /// 通常情况下，你不能直接实例化IocManager
        /// 测试目的可能是有用的。
        /// </summary>
        public IocManager()
        {
            IocContainer = new WindsorContainer();
            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();

            //Register self!
            //注册自己
            IocContainer.Register(
                Component.For<IocManager, IIocManager, IIocRegistrar, IIocResolver>().UsingFactoryMethod(() => this)
                );
        }

        /// <summary>
        /// Adds a dependency registrar for conventional registration.
        /// 添加常规注册
        /// </summary>
        /// <param name="registrar">dependency registrar 常规依赖注册</param>
        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar)
        {
            _conventionalRegistrars.Add(registrar);
        }

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="AddConventionalRegistrar"/> method.
        /// 根据常规方式注册程序集，
        /// 通过所有常规的注册，指定的组件类型。
        /// </summary>
        /// <param name="assembly">Assembly to register 注册程序集</param>
        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            RegisterAssemblyByConvention(assembly, new ConventionalRegistrationConfig());
        }

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="AddConventionalRegistrar"/> method.
        /// 根据常规方式注册程序集
        /// </summary>
        /// <param name="assembly">Assembly to register 注册程序集</param>
        /// <param name="config">Additional configuration 常规注册配置</param>
        public void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config)
        {
            var context = new ConventionalRegistrationContext(assembly, this, config);

            foreach (var registerer in _conventionalRegistrars)
            {
                registerer.RegisterAssembly(context);
            }

            if (config.InstallInstallers)
            {
                IocContainer.Install(FromAssembly.Instance(assembly));
            }
        }

        /// <summary>
        /// Registers a type as self registration.
        /// 注册，默认注册类型为自注册
        /// </summary>
        /// <typeparam name="TType">Type of the class 类型为class</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type 注册对象的依赖注入生命周期</param>
        public void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>(), lifeStyle));
        }

        /// <summary>
        /// Registers a type as self registration.
        /// 注册，默认注册类型为自注册
        /// </summary>
        /// <param name="type">Type of the class 类型为class</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type 注册对象的依赖注入生命周期</param>
        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        /// <summary>
        /// Registers a type with it's implementation.
        /// 注册，注册类型与它的实现
        /// </summary>
        /// <typeparam name="TType">Registering type 注册类型</typeparam>
        /// <typeparam name="TImpl">The type that implements <see cref="TType"/> </typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type 注册对象的依赖注入生命周期</param>
        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }

        /// <summary>
        /// Registers a class as self registration.
        /// Registers a type with it's implementation.
        /// 注册，默认注册类型为自注册
        /// </summary>
        /// <param name="type">Type of the class 类型为class</param>
        /// <param name="impl">The type that implements <paramref name="type"/> 实现的类型</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type 注册对象的依赖注入生命周期</param>
        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type, impl).ImplementedBy(impl), lifeStyle));
        }

        /// <summary>
        /// Checks whether given type is registered before.
        /// 是否已经注册，在注册前检查给定类型是否。
        /// </summary>
        /// <param name="type">Type to check 检查类型</param>
        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        /// <summary>
        /// Checks whether given type is registered before.
        /// 是否已经注册，在注册前检查给定类型是否。
        /// </summary>
        /// <typeparam name="TType">Type to check 检查类型</typeparam>
        public bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// 从容器获取对象。
        /// 返回对象必须被释放（见IIocResolver.Release）
        /// </summary> 
        /// <typeparam name="T">Type of the object to get 获取对象类型</typeparam>
        /// <returns>The instance object</returns>
        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="Release"/>) after usage.
        /// </summary> 
        /// <typeparam name="T">Type of the object to cast</typeparam>
        /// <param name="type">Type of the object to resolve</param>
        /// <returns>The object instance</returns>
        public T Resolve<T>(Type type)
        {
            return (T)IocContainer.Resolve(type);
        }

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// 从容器获取对象。
        /// 返回对象必须被释放（见IIocResolver.Release）
        /// </summary> 
        /// <typeparam name="T">Type of the object to get 获取对象类型</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The instance object</returns>
        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<T>(argumentsAsAnonymousType);
        }

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// 从容器获取对象。
        /// 返回对象必须被释放（见IIocResolver.Release）
        /// </summary> 
        /// <param name="type">Type of the object to get 获取对象类型</param>
        /// <returns>The instance object 实例对象</returns>
        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// 从容器获取对象。
        /// 返回对象必须被释放（见IIocResolver.Release）
        /// </summary> 
        /// <param name="type">Type of the object to get 获取对象类型</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments 构造函数参数</param>
        /// <returns>The instance object 实例对象</returns>
        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentsAsAnonymousType);
        }

        /// <summary>
        /// Releases a pre-resolved object. See Resolve methods.
        /// 释放，释放预处理对象。见Resolve方法
        /// </summary>
        /// <param name="obj">Object to be released 释放对象</param>
        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <inheritdoc/>
        public void Dispose()
        {
            IocContainer.Dispose();
        }

        /// <summary>
        /// 应用生命周期
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="registration">注册</param>
        /// <param name="lifeStyle">生命周期</param>
        /// <returns></returns>
        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, DependencyLifeStyle lifeStyle)
            where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }
    }
}
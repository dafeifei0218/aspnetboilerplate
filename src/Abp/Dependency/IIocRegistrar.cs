using System;
using System.Reflection;

namespace Abp.Dependency
{
    /// <summary>
    /// Define interface for classes those are used to register dependencies.
    /// IOC注册类，定义类的接口用于注册依赖关系。
    /// </summary>
    public interface IIocRegistrar
    {
        /// <summary>
        /// Adds a dependency registrar for conventional registration.
        /// 添加常规依赖注册
        /// </summary>
        /// <param name="registrar">dependency registrar 依赖注册</param>
        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="IocManager.AddConventionalRegistrar"/> method.
        /// 注册程序集
        /// </summary>
        /// <param name="assembly">Assembly to register 程序集注册</param>
        void RegisterAssemblyByConvention(Assembly assembly);

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="IocManager.AddConventionalRegistrar"/> method.
        /// 注册程序集
        /// </summary>
        /// <param name="assembly">Assembly to register 程序集注册</param>
        /// <param name="config">Additional configuration 常规注册配置</param>
        void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config);

        /// <summary>
        /// Registers a type as self registration.
        /// 注册类型
        /// </summary>
        /// <typeparam name="T">Type of the class 类的类型</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type 依赖注入生命周期</param>
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class;

        /// <summary>
        /// Registers a type as self registration.
        /// 注册类型实现
        /// </summary>
        /// <param name="type">Type of the class 类的类型</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type 依赖注入生命周期</param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// Registers a type with it's implementation.
        /// 注册类型实现
        /// </summary>
        /// <typeparam name="TType">Registering type 注册类型</typeparam>
        /// <typeparam name="TImpl">The type that implements <see cref="TType"/> 实现类型</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type 依赖注入生命周期</param>
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;

        /// <summary>
        /// Registers a type with it's implementation.
        /// 注册类型
        /// </summary>
        /// <param name="type">Type of the class 类的类型</param>
        /// <param name="impl">The type that implements <paramref name="type"/> 实现类型</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type 依赖注入生命周期</param>
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// Checks whether given type is registered before.
        /// 是否已经注册，在注册前检查给定的类型是否已经注册
        /// </summary>
        /// <param name="type">Type to check 类型检查</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// Checks whether given type is registered before.
        /// 是否已经注册，在注册前检查给定的类型是否已经注册
        /// </summary>
        /// <typeparam name="TType">Type to check 类型检查</typeparam>
        bool IsRegistered<TType>();
    }
}
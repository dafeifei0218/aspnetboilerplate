using System;
using Castle.Windsor;

namespace Abp.Dependency
{
    /// <summary>
    /// This interface is used to directly perform dependency injection tasks.
    /// IOC管理类接口
    /// </summary>
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        /// <summary>
        /// Reference to the Castle Windsor Container.
        /// IOC容器，Castle Windsor容器
        /// </summary>
        IWindsorContainer IocContainer { get; }

        /// <summary>
        /// Checks whether given type is registered before.
        /// 检查类型是否注册
        /// </summary>
        /// <param name="type">Type to check 检查类型</param>
        new bool IsRegistered(Type type);

        /// <summary>
        /// Checks whether given type is registered before.
        /// 检查类型是否注册
        /// </summary>
        /// <typeparam name="T">Type to check 检查类型</typeparam>
        new bool IsRegistered<T>();
    }
}
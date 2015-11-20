using System;
using Castle.Windsor;

namespace Abp.Dependency
{
    /// <summary>
    /// This interface is used to directly perform dependency injection tasks.
    /// IOC������ӿ�
    /// </summary>
    public interface IIocManager : IIocRegistrar, IIocResolver, IDisposable
    {
        /// <summary>
        /// Reference to the Castle Windsor Container.
        /// IOC������Castle Windsor����
        /// </summary>
        IWindsorContainer IocContainer { get; }

        /// <summary>
        /// Checks whether given type is registered before.
        /// ��������Ƿ�ע��
        /// </summary>
        /// <param name="type">Type to check �������</param>
        new bool IsRegistered(Type type);

        /// <summary>
        /// Checks whether given type is registered before.
        /// ��������Ƿ�ע��
        /// </summary>
        /// <typeparam name="T">Type to check �������</typeparam>
        new bool IsRegistered<T>();
    }
}
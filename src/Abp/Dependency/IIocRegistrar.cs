using System;
using System.Reflection;

namespace Abp.Dependency
{
    /// <summary>
    /// Define interface for classes those are used to register dependencies.
    /// IOCע���࣬������Ľӿ�����ע��������ϵ��
    /// </summary>
    public interface IIocRegistrar
    {
        /// <summary>
        /// Adds a dependency registrar for conventional registration.
        /// ��ӳ�������ע��
        /// </summary>
        /// <param name="registrar">dependency registrar ����ע��</param>
        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="IocManager.AddConventionalRegistrar"/> method.
        /// ע�����
        /// </summary>
        /// <param name="assembly">Assembly to register ����ע��</param>
        void RegisterAssemblyByConvention(Assembly assembly);

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="IocManager.AddConventionalRegistrar"/> method.
        /// ע�����
        /// </summary>
        /// <param name="assembly">Assembly to register ����ע��</param>
        /// <param name="config">Additional configuration ����ע������</param>
        void RegisterAssemblyByConvention(Assembly assembly, ConventionalRegistrationConfig config);

        /// <summary>
        /// Registers a type as self registration.
        /// ע������
        /// </summary>
        /// <typeparam name="T">Type of the class �������</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type ����ע����������</param>
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class;

        /// <summary>
        /// Registers a type as self registration.
        /// ע������ʵ��
        /// </summary>
        /// <param name="type">Type of the class �������</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type ����ע����������</param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// Registers a type with it's implementation.
        /// ע������ʵ��
        /// </summary>
        /// <typeparam name="TType">Registering type ע������</typeparam>
        /// <typeparam name="TImpl">The type that implements <see cref="TType"/> ʵ������</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type ����ע����������</param>
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;

        /// <summary>
        /// Registers a type with it's implementation.
        /// ע������
        /// </summary>
        /// <param name="type">Type of the class �������</param>
        /// <param name="impl">The type that implements <paramref name="type"/> ʵ������</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type ����ע����������</param>
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// Checks whether given type is registered before.
        /// �Ƿ��Ѿ�ע�ᣬ��ע��ǰ�������������Ƿ��Ѿ�ע��
        /// </summary>
        /// <param name="type">Type to check ���ͼ��</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// Checks whether given type is registered before.
        /// �Ƿ��Ѿ�ע�ᣬ��ע��ǰ�������������Ƿ��Ѿ�ע��
        /// </summary>
        /// <typeparam name="TType">Type to check ���ͼ��</typeparam>
        bool IsRegistered<TType>();
    }
}
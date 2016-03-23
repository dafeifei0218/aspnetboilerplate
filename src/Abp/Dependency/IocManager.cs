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
    /// IOC���Ʒ�ת������
    /// </summary>
    public class IocManager : IIocManager
    {
        /// <summary>
        /// The Singleton instance.
        /// ����ʵ��
        /// </summary>
        public static IocManager Instance { get; private set; }

        /// <summary>
        /// Reference to the Castle Windsor Container.
        /// IOC����
        /// </summary>
        public IWindsorContainer IocContainer { get; private set; }

        /// <summary>
        /// List of all registered conventional registrars.
        /// ��������ע���б�
        /// </summary>
        private readonly List<IConventionalDependencyRegistrar> _conventionalRegistrars;

        /// <summary>
        /// ���캯��
        /// </summary>
        static IocManager()
        {
            Instance = new IocManager();
        }

        /// <summary>
        /// Creates a new <see cref="IocManager"/> object.
        /// Normally, you don't directly instantiate an <see cref="IocManager"/>.
        /// This may be useful for test purposes.
        /// ���캯����
        /// ͨ������£��㲻��ֱ��ʵ����IocManager
        /// ����Ŀ�Ŀ��������õġ�
        /// </summary>
        public IocManager()
        {
            IocContainer = new WindsorContainer();
            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();

            //Register self!
            //ע���Լ�
            IocContainer.Register(
                Component.For<IocManager, IIocManager, IIocRegistrar, IIocResolver>().UsingFactoryMethod(() => this)
                );
        }

        /// <summary>
        /// Adds a dependency registrar for conventional registration.
        /// ��ӳ���ע��
        /// </summary>
        /// <param name="registrar">dependency registrar ��������ע��</param>
        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar)
        {
            _conventionalRegistrars.Add(registrar);
        }

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="AddConventionalRegistrar"/> method.
        /// ���ݳ��淽ʽע����򼯣�
        /// ͨ�����г����ע�ᣬָ����������͡�
        /// </summary>
        /// <param name="assembly">Assembly to register ע�����</param>
        public void RegisterAssemblyByConvention(Assembly assembly)
        {
            RegisterAssemblyByConvention(assembly, new ConventionalRegistrationConfig());
        }

        /// <summary>
        /// Registers types of given assembly by all conventional registrars. See <see cref="AddConventionalRegistrar"/> method.
        /// ���ݳ��淽ʽע�����
        /// </summary>
        /// <param name="assembly">Assembly to register ע�����</param>
        /// <param name="config">Additional configuration ����ע������</param>
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
        /// ע�ᣬĬ��ע������Ϊ��ע��
        /// </summary>
        /// <typeparam name="TType">Type of the class ����Ϊclass</typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type ע����������ע����������</param>
        public void Register<TType>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where TType : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType>(), lifeStyle));
        }

        /// <summary>
        /// Registers a type as self registration.
        /// ע�ᣬĬ��ע������Ϊ��ע��
        /// </summary>
        /// <param name="type">Type of the class ����Ϊclass</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type ע����������ע����������</param>
        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        /// <summary>
        /// Registers a type with it's implementation.
        /// ע�ᣬע������������ʵ��
        /// </summary>
        /// <typeparam name="TType">Registering type ע������</typeparam>
        /// <typeparam name="TImpl">The type that implements <see cref="TType"/> </typeparam>
        /// <param name="lifeStyle">Lifestyle of the objects of this type ע����������ע����������</param>
        public void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }

        /// <summary>
        /// Registers a class as self registration.
        /// Registers a type with it's implementation.
        /// ע�ᣬĬ��ע������Ϊ��ע��
        /// </summary>
        /// <param name="type">Type of the class ����Ϊclass</param>
        /// <param name="impl">The type that implements <paramref name="type"/> ʵ�ֵ�����</param>
        /// <param name="lifeStyle">Lifestyle of the objects of this type ע����������ע����������</param>
        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type, impl).ImplementedBy(impl), lifeStyle));
        }

        /// <summary>
        /// Checks whether given type is registered before.
        /// �Ƿ��Ѿ�ע�ᣬ��ע��ǰ�����������Ƿ�
        /// </summary>
        /// <param name="type">Type to check �������</param>
        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        /// <summary>
        /// Checks whether given type is registered before.
        /// �Ƿ��Ѿ�ע�ᣬ��ע��ǰ�����������Ƿ�
        /// </summary>
        /// <typeparam name="TType">Type to check �������</typeparam>
        public bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// ��������ȡ����
        /// ���ض�����뱻�ͷţ���IIocResolver.Release��
        /// </summary> 
        /// <typeparam name="T">Type of the object to get ��ȡ��������</typeparam>
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
        /// ��������ȡ����
        /// ���ض�����뱻�ͷţ���IIocResolver.Release��
        /// </summary> 
        /// <typeparam name="T">Type of the object to get ��ȡ��������</typeparam>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The instance object</returns>
        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<T>(argumentsAsAnonymousType);
        }

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// ��������ȡ����
        /// ���ض�����뱻�ͷţ���IIocResolver.Release��
        /// </summary> 
        /// <param name="type">Type of the object to get ��ȡ��������</param>
        /// <returns>The instance object ʵ������</returns>
        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// ��������ȡ����
        /// ���ض�����뱻�ͷţ���IIocResolver.Release��
        /// </summary> 
        /// <param name="type">Type of the object to get ��ȡ��������</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments ���캯������</param>
        /// <returns>The instance object ʵ������</returns>
        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentsAsAnonymousType);
        }

        /// <summary>
        /// Releases a pre-resolved object. See Resolve methods.
        /// �ͷţ��ͷ�Ԥ������󡣼�Resolve����
        /// </summary>
        /// <param name="obj">Object to be released �ͷŶ���</param>
        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <inheritdoc/>
        public void Dispose()
        {
            IocContainer.Dispose();
        }

        /// <summary>
        /// Ӧ����������
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="registration">ע��</param>
        /// <param name="lifeStyle">��������</param>
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
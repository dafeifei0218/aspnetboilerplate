using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Configuration.Startup;
using Abp.Dependency;

namespace Abp.Modules
{
    /// <summary>
    /// This class must be implemented by all module definition classes.
    /// Abp模块
    /// </summary>
    /// <remarks>
    /// A module definition class is generally located in it's own assembly
    /// and implements some action in module events on application startup and shotdown.
    /// It also defines depended modules.
    /// </remarks>
    public abstract class AbpModule
    {
        /// <summary>
        /// Gets a reference to the IOC manager.
        /// IOC管理类
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

        /// <summary>
        /// Gets a reference to the ABP configuration.
        /// Abp启动配置
        /// </summary>
        protected internal IAbpStartupConfiguration Configuration { get; internal set; }

        /// <summary>
        /// This is the first event called on application startup. 
        /// Codes can be placed here to run before dependency injection registrations.
        /// 预初始化之方法，
        /// 这是第一个在应用程序启动时调用的事件。
        /// 代码可以放置在这里，在运行依赖注入注册之前执行。
        /// </summary>
        public virtual void PreInitialize()
        {

        }

        /// <summary>
        /// This method is used to register dependencies for this module.
        /// 初始化方法，
        /// 此方法用于此模块的依赖关系。
        /// </summary>
        public virtual void Initialize()
        {

        }

        /// <summary>
        /// This method is called lastly on application startup.
        /// 初始化方法，在应用程序启动后执行
        /// </summary>
        public virtual void PostInitialize()
        {
            
        }

        /// <summary>
        /// This method is called when the application is being shutdown.
        /// 关闭，此方法被调用时，该应用程序正在关闭。
        /// </summary>
        public virtual void Shutdown()
        {
            
        }

        /// <summary>
        /// Checks if given type is an Abp module class.
        /// 是否是Abp模块
        /// </summary>
        /// <param name="type">Type to check 被检查的类型</param>
        public static bool IsAbpModule(Type type)
        {
            return
                type.IsClass &&
                !type.IsAbstract &&
                typeof(AbpModule).IsAssignableFrom(type);
        }

        /// <summary>
        /// Finds depended modules of a module.
        /// 查找依赖模块的模块。
        /// </summary>
        /// <param name="moduleType">模块类型</param>
        /// <returns></returns>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsAbpModule(moduleType))
            {
                throw new AbpInitializationException("This type is not an ABP module: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }
    }
}

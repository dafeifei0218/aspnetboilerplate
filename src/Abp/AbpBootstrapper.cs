using System;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Dependency.Installers;
using Abp.Modules;

namespace Abp
{
    /// <summary>
    /// This is the main class that is responsible to start entire ABP system.
    /// Prepares dependency injection and registers core components needed for startup.
    /// It must be instantiated and initialized (see <see cref="Initialize"/>) first in an application.
    /// Abp系统启动类，
    /// 这是一个主要的类，它负责启动整个Abp系统。
    /// 在启动中准备依赖注入和核心组件注册。
    /// 在一个应用中，他必须最先实例化（查看<see cref="Initialize"/>）。
    /// </summary>
    public class AbpBootstrapper : IDisposable
    {
        /// <summary>
        /// Gets IIocManager object used by this class.
        /// 获取IOC管理类
        /// </summary>
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// Is this object disposed before?
        /// 对象是否释放资源
        /// </summary>
        protected bool IsDisposed;

        /// <summary>
        /// Abp模块管理类
        /// </summary>
        private IAbpModuleManager _moduleManager;

        /// <summary>
        /// Creates a new <see cref="AbpBootstrapper"/> instance.
        /// 构造函数
        /// </summary>
        public AbpBootstrapper()
            : this(Dependency.IocManager.Instance)
        {

        }

        /// <summary>
        /// Creates a new <see cref="AbpBootstrapper"/> instance.
        /// 构造函数
        /// </summary>
        /// <param name="iocManager">IIocManager that is used to bootstrap the ABP system. IOC管理类，启动ABP系统使用的IIocManager。</param>
        public AbpBootstrapper(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        /// <summary>
        /// Initializes the ABP system.
        /// 初始化ABP系统
        /// </summary>
        public virtual void Initialize()
        {
            //Abp核心安装程序
            IocManager.IocContainer.Install(new AbpCoreInstaller());

            //abp启动配置初始化
            IocManager.Resolve<AbpStartupConfiguration>().Initialize();

            //解析abp模块
            _moduleManager = IocManager.Resolve<IAbpModuleManager>();
            _moduleManager.InitializeModules();
        }

        /// <summary>
        /// Disposes the ABP system.
        /// 销毁ABP系统
        /// </summary>
        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (_moduleManager != null)
            {
                _moduleManager.ShutdownModules();
            }
        }
    }
}

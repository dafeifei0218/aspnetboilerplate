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
    /// Abp系统启动类，准备依赖注入和启动所需的核心组件
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
        /// 是否释放资源
        /// </summary>
        protected bool IsDisposed;

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
        /// <param name="iocManager">IIocManager that is used to bootstrap the ABP system. IOC管理类</param>
        public AbpBootstrapper(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        /// <summary>
        /// Initializes the ABP system.
        /// ABP系统初始化
        /// </summary>
        public virtual void Initialize()
        {
            IocManager.IocContainer.Install(new AbpCoreInstaller());

            IocManager.Resolve<AbpStartupConfiguration>().Initialize();

            _moduleManager = IocManager.Resolve<IAbpModuleManager>();
            _moduleManager.InitializeModules();
        }

        /// <summary>
        /// Disposes the ABP system.
        /// ABP系统销毁
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

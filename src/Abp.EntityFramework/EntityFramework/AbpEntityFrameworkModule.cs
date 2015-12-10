using System.Reflection;
using Abp.Collections.Extensions;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Abp.EntityFramework.Uow;
using Abp.Modules;
using Abp.Reflection;
using Castle.Core.Logging;
using Castle.MicroKernel.Registration;

namespace Abp.EntityFramework
{
    /// <summary>
    /// This module is used to implement "Data Access Layer" in EntityFramework.
    /// AbpEntityFramework模块
    /// </summary>
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpEntityFrameworkModule : AbpModule
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 类型过滤器
        /// </summary>
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="typeFinder">类型过滤器</param>
        public AbpEntityFrameworkModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 预先初始化
        /// </summary>
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new EntityFrameworkConventionalRegisterer());
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(
                Component.For(typeof (IDbContextProvider<>))
                    .ImplementedBy(typeof (UnitOfWorkDbContextProvider<>))
                    .LifestyleTransient()
                );

            //注册通用仓储
            RegisterGenericRepositories();
        }

        /// <summary>
        /// 注册通用仓储
        /// </summary>
        private void RegisterGenericRepositories()
        {
            var dbContextTypes =
                _typeFinder.Find(type =>
                    type.IsPublic &&
                    !type.IsAbstract &&
                    type.IsClass &&
                    typeof(AbpDbContext).IsAssignableFrom(type)
                    );

            //如果AbpDbContext未找到，则抛出异常
            if (dbContextTypes.IsNullOrEmpty())
            {
                Logger.Warn("No class found derived from AbpDbContext.");
                return;
            }

            foreach (var dbContextType in dbContextTypes)
            {
                EntityFrameworkGenericRepositoryRegistrar.RegisterForDbContext(dbContextType, IocManager);
            }
        }
    }
}

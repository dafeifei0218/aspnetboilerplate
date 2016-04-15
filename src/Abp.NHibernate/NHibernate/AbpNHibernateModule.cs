using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.NHibernate.Filters;
using Abp.NHibernate.Interceptors;
using Abp.NHibernate.Repositories;
using Castle.Components.DictionaryAdapter.Xml;
using NHibernate;

namespace Abp.NHibernate
{
    /// <summary>
    /// This module is used to implement "Data Access Layer" in NHibernate.
    /// Abp Nhibernate模块，这个模块使用Nhibernate来实现“数据层”
    /// </summary>
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpNHibernateModule : AbpModule
    {
        /// <summary>
        /// NHibernate session factory object.
        /// NHibernate会话工厂对象。
        /// </summary>
        private ISessionFactory _sessionFactory;

        /// <summary>
        ///  初始化
        /// </summary>
        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.Register<AbpNHibernateInterceptor>(DependencyLifeStyle.Transient);

            _sessionFactory = Configuration.Modules.AbpNHibernate().FluentConfiguration
                .Mappings(m => m.FluentMappings.Add(typeof(MayHaveTenantFilter)))
                .Mappings(m => m.FluentMappings.Add(typeof(MustHaveTenantFilter)))
                .ExposeConfiguration(config => config.SetInterceptor(IocManager.Resolve<AbpNHibernateInterceptor>()))
                .BuildSessionFactory();

            IocManager.IocContainer.Install(new NhRepositoryInstaller(_sessionFactory));
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
        
        /// <summary>
        /// 关闭
        /// </summary>
        /// <inheritdoc/>
        public override void Shutdown()
        {
            _sessionFactory.Dispose();
        }
    }
}

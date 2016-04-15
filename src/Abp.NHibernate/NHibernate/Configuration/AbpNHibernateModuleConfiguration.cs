using FluentNHibernate.Cfg;

namespace Abp.NHibernate.Configuration
{
    /// <summary>
    /// Abp NHibernate模块配置
    /// </summary>
    internal class AbpNHibernateModuleConfiguration : IAbpNHibernateModuleConfiguration
    {
        /// <summary>
        /// Fluent配置
        /// </summary>
        public FluentConfiguration FluentConfiguration { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AbpNHibernateModuleConfiguration()
        {
            FluentConfiguration = Fluently.Configure();
        }
    }
}
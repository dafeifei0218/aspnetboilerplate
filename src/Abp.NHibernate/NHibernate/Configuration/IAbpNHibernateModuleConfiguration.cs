using FluentNHibernate.Cfg;

namespace Abp.NHibernate.Configuration
{
    /// <summary>
    /// Used to configure ABP NHibernate module.
    /// Abp NHibernate模块配置接口
    /// </summary>
    public interface IAbpNHibernateModuleConfiguration
    {
        /// <summary>
        /// Used to get and modify NHibernate fluent configuration.
        /// You can add mappings to this object.
        /// Do not call BuildSessionFactory on it.
        /// Fluent配置，用于获取或修改NHibernate fluent配置。
        /// 你可以想这个对象添加映射。
        /// 不要叫建造会话工厂。
        /// </summary>
        FluentConfiguration FluentConfiguration { get; }
    }
}
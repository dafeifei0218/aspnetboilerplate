namespace Abp.Configuration.Startup
{
    /// <summary>
    /// Used to configure multi-tenancy.
    /// 多租户配置
    /// </summary>
    public interface IMultiTenancyConfig
    {
        /// <summary>
        /// Is multi-tenancy enabled?
        /// Default value: false.
        /// 是否启用多租户，
        /// 默认值：false
        /// </summary>
        bool IsEnabled { get; set; }
    }
}
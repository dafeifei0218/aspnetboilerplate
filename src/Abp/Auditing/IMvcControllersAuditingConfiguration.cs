namespace Abp.Auditing
{
    /// <summary>
    /// Defines MVC Controller auditing configurations
    /// MVC控制器审计配置
    /// </summary>
    /// <remarks>
    /// 用于配置是否启用对MVC Controller及其Action的Auditing功能。
    /// </remarks>
    public interface IMvcControllersAuditingConfiguration
    {
        /// <summary>
        /// Used to enable/disable auditing for MVC controllers.
        /// Default: true.
        /// 是否启用，
        /// 默认：true
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Used to enable/disable auditing for child MVC actions.
        /// Default: false.
        /// 是否启用子Action，
        /// 默认：false
        /// </summary>
        bool IsEnabledForChildActions { get; set; }
    }
}
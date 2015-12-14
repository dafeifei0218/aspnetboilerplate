namespace Abp.Auditing
{
    /// <summary>
    /// Used to configure auditing.
    /// 审计配置接口
    /// </summary>
    public interface IAuditingConfiguration
    {
        /// <summary>
        /// Used to enable/disable auditing system.
        /// Default: true. Set false to completely disable it.
        /// 是否启用，用于启用/禁用审计系统
        /// 默认：true，设置false为要完全禁用它。
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Set true to enable saving audit logs if current user is not logged in.
        /// Default: false.
        /// 是否启用匿名用户，如果当前用户没有登录，则设置为启用保存审计日志。
        /// 默认值：false
        /// </summary>
        bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// Used to configure auditing for MVC Controllers.
        /// MVC控制器，使用MVC控制器配置审计。
        /// </summary>
        IMvcControllersAuditingConfiguration MvcControllers { get; }

        /// <summary>
        /// List of selectors to select classes/interfaces which should be audited as default.
        /// 审计选择列表，选择类/接口应该是审计作为默认选择列表。
        /// </summary>
        IAuditingSelectorList Selectors { get; }
    }
}
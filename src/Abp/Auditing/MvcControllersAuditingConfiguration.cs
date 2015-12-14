namespace Abp.Auditing
{
    /// <summary>
    /// MVC控制器审计配置
    /// </summary>
    internal class MvcControllersAuditingConfiguration : IMvcControllersAuditingConfiguration
    {
        /// <summary>
        /// 是否启用，
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否启用子Action，默认：false
        /// </summary>
        public bool IsEnabledForChildActions { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public MvcControllersAuditingConfiguration()
        {
            IsEnabled = true;
        }
    }
}
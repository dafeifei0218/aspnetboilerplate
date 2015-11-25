namespace Abp.Auditing
{
    /// <summary>
    /// 审计配置
    /// </summary>
    internal class AuditingConfiguration : IAuditingConfiguration
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否启用匿名用户
        /// </summary>
        public bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// MVC控制器审计配置
        /// </summary>
        public IMvcControllersAuditingConfiguration MvcControllers { get; private set; }

        /// <summary>
        /// 审计选择器列表
        /// </summary>
        public IAuditingSelectorList Selectors { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuditingConfiguration()
        {
            IsEnabled = true;
            Selectors = new AuditingSelectorList();
            MvcControllers = new MvcControllersAuditingConfiguration();
        }
    }
}
namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Names of standard caches used in ABP.
    /// Abp��������
    /// </summary>
    public static class AbpCacheNames
    {
        /// <summary>
        /// Application settings cache: AbpApplicationSettingsCache.
        /// Ӧ�ó������û���
        /// </summary>
        public const string ApplicationSettings = "AbpApplicationSettingsCache";

        /// <summary>
        /// Tenant settings cache: AbpTenantSettingsCache.
        /// �⻧���û���
        /// </summary>
        public const string TenantSettings = "AbpTenantSettingsCache";

        /// <summary>
        /// User settings cache: AbpUserSettingsCache.
        /// �û����û���
        /// </summary>
        public const string UserSettings = "AbpUserSettingsCache";

        /// <summary>
        /// Localization scripts cache: AbpLocalizationScripts.
        /// ���ػ��ű�
        /// </summary>
        public const string LocalizationScripts = "AbpLocalizationScripts";
    }
}
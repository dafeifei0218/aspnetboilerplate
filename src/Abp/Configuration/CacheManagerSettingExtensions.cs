﻿using System.Collections.Generic;
using Abp.Runtime.Caching;

namespace Abp.Configuration
{
    /// <summary>
    /// Extension methods for <see cref="ICacheManager"/> to get setting caches.
    /// ICacheManager缓存管理设置扩展类
    /// </summary>
    public static class CacheManagerSettingExtensions
    {
        /// <summary>
        /// Gets application settings cache.
        /// 获取应用程序设置缓存
        /// </summary>
        public static ITypedCache<string, Dictionary<string, SettingInfo>> GetApplicationSettingsCache(this ICacheManager cacheManager)
        {
            return cacheManager
                .GetCache<string, Dictionary<string, SettingInfo>>(AbpCacheNames.ApplicationSettings);
        }

        /// <summary>
        /// Gets tenant settings cache.
        /// 获取租户设置缓存
        /// </summary>
        public static ITypedCache<int, Dictionary<string, SettingInfo>> GetTenantSettingsCache(this ICacheManager cacheManager)
        {
            return cacheManager
                .GetCache<int, Dictionary<string, SettingInfo>>(AbpCacheNames.TenantSettings);
        }

        /// <summary>
        /// Gets user settings cache.
        /// 获取用户设置缓存
        /// </summary>
        public static ITypedCache<long, Dictionary<string, SettingInfo>> GetUserSettingsCache(this ICacheManager cacheManager)
        {
            return cacheManager
                .GetCache<long, Dictionary<string, SettingInfo>>(AbpCacheNames.UserSettings);
        }
    }
}

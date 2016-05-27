using System;
using System.Collections.Generic;
using Abp.Configuration.Startup;

namespace Abp.Runtime.Caching.Configuration
{
    /// <summary>
    /// Used to configure caching system.
    /// 缓存配置接口，用于配置缓存系统
    /// </summary>
    public interface ICachingConfiguration
    {
        /// <summary>
        /// Gets the ABP configuration object.
        /// 获取ABP启动配置
        /// </summary>
        IAbpStartupConfiguration AbpConfiguration { get; }

        /// <summary>
        /// List of all registered configurators.
        /// 缓存配置列表
        /// </summary>
        IReadOnlyList<ICacheConfigurator> Configurators { get; }

        /// <summary>
        /// Used to configure all caches.
        /// 全部缓存
        /// </summary>
        /// <param name="initAction">
        /// An action to configure caches
        /// This action is called for each cache just after created.
        /// 初始化动作，
        /// 配置缓存操作，这个动作在缓存刚刚创建时调用
        /// </param>
        void ConfigureAll(Action<ICache> initAction);

        /// <summary>
        /// Used to configure a specific cache. 
        /// 用于配置特定的缓存
        /// </summary>
        /// <param name="cacheName">Cache name 缓存名称</param>
        /// <param name="initAction">
        /// An action to configure the cache.
        /// This action is called just after the cache is created.
        /// 初始化动作，
        /// 配置缓存操作，这个动作在缓存刚刚创建时调用
        /// </param>
        void Configure(string cacheName, Action<ICache> initAction);
    }
}

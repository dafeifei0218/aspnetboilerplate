using System;

namespace Abp.Runtime.Caching.Configuration
{
    /// <summary>
    /// A registered cache configurator.
    /// 缓存配置器接口
    /// </summary>
    public interface ICacheConfigurator
    {
        /// <summary>
        /// Name of the cache.
        /// It will be null if this configurator configures all caches.
        /// 缓存名称，如果配置器配置所有缓存，它会是空的
        /// </summary>
        string CacheName { get; }

        /// <summary>
        /// Configuration action. Called just after the cache is created.
        /// 初始化动作。在缓存创建后调用配置动作
        /// </summary>
        Action<ICache> InitAction { get; }
    }
}
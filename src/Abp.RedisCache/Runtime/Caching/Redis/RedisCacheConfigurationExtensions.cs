using Abp.Dependency;
using Abp.Runtime.Caching.Configuration;

namespace Abp.Runtime.Caching.Redis
{
    /// <summary>
    /// Extension methods for <see cref="ICachingConfiguration"/>.
    /// Redis缓存配置扩展类
    /// </summary>
    public static class RedisCacheConfigurationExtensions
    {
        /// <summary>
        /// Configures caching to use Redis as cache server.
        /// 配置缓存用于Redis缓存服务器
        /// </summary>
        /// <param name="cachingConfiguration">The caching configuration. 缓存配置</param>
        public static void UseRedis(this ICachingConfiguration cachingConfiguration)
        {
            cachingConfiguration.AbpConfiguration.IocManager.RegisterIfNot<ICacheManager, AbpRedisCacheManager>();
        }
    }
}

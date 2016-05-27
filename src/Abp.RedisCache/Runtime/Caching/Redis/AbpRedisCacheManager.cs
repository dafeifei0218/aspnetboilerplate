using Abp.Dependency;
using Abp.Runtime.Caching.Configuration;

namespace Abp.Runtime.Caching.Redis
{
    /// <summary>
    /// Used to create <see cref="AbpRedisCache"/> instances.
    /// Abp Redis管理类
    /// </summary>
    /// <remarks>
    /// 重写了CacheManagerBase的CreateCacheImplementation方法，该方法用于创建真实的Icache对象。 
    /// 具体到AbpRedisCacheManager就是创建AbpRedisCache。
    /// </remarks>
    public class AbpRedisCacheManager : CacheManagerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AbpRedisCacheManager"/> class.
        /// 初始化一个新的<see cref="AbpRedisCacheManager"/>类
        /// </summary>
        /// <param name="iocManager">Ioc管理类</param>
        /// <param name="configuration">缓存配置类</param>
        public AbpRedisCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {
            IocManager.RegisterIfNot<AbpRedisCache>(DependencyLifeStyle.Transient);
        }

        /// <summary>
        /// 获取缓存实现
        /// </summary>
        /// <param name="name">键</param>
        /// <returns></returns>
        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.Resolve<AbpRedisCache>(new { name });
        }
    }
}

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Abp.Dependency;
using Abp.Runtime.Caching.Configuration;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Base class for cache managers.
    /// 缓存管理基类
    /// </summary>
    public abstract class CacheManagerBase : ICacheManager, ISingletonDependency
    {
        protected readonly IIocManager IocManager;

        protected readonly ICachingConfiguration Configuration;

        protected readonly ConcurrentDictionary<string, ICache> Caches;

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="iocManager">Ioc管理类</param>
        /// <param name="configuration">缓存配置</param>
        protected CacheManagerBase(IIocManager iocManager, ICachingConfiguration configuration)
        {
            IocManager = iocManager;
            Configuration = configuration;
            Caches = new ConcurrentDictionary<string, ICache>();
        }

        /// <summary>
        /// 获取全部缓存
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<ICache> GetAllCaches()
        {
            return Caches.Values.ToImmutableList();
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="name">缓存名称</param>
        /// <returns></returns>
        public virtual ICache GetCache(string name)
        {
            return Caches.GetOrAdd(name, (cacheName) =>
            {
                var cache = CreateCacheImplementation(cacheName);

                var configurators = Configuration.Configurators.Where(c => c.CacheName == null || c.CacheName == cacheName);

                foreach (var configurator in configurators)
                {
                    if (configurator.InitAction != null)
                    {
                        configurator.InitAction(cache);
                    }
                }

                return cache;
            });
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public virtual void Dispose()
        {
            foreach (var cache in Caches)
            {
                IocManager.Release(cache.Value);
            }

            Caches.Clear();
        }

        /// <summary>
        /// Used to create actual cache implementation.
        /// 创建缓存实现
        /// </summary>
        /// <param name="name">Name of the cache 缓存名称</param>
        /// <returns>Cache object 缓存对象</returns>
        protected abstract ICache CreateCacheImplementation(string name);
    }
}
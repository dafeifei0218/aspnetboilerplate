using System.Runtime.Caching;
using Abp.Dependency;
using Abp.Runtime.Caching.Configuration;

namespace Abp.Runtime.Caching.Memory
{
    /// <summary>
    /// Implements <see cref="ICacheManager"/> to work with <see cref="MemoryCache"/>.
    /// Abp内存缓存管理类
    /// </summary>
    public class AbpMemoryCacheManager : CacheManagerBase
    {
        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="iocManager">Ioc管理类</param>
        /// <param name="configuration">缓存配置</param>
        public AbpMemoryCacheManager(IIocManager iocManager, ICachingConfiguration configuration)
            : base(iocManager, configuration)
        {
            IocManager.RegisterIfNot<AbpMemoryCache>(DependencyLifeStyle.Transient);
        }

        /// <summary>
        /// 创建缓存实现
        /// </summary>
        /// <param name="name">键</param>
        /// <returns></returns>
        protected override ICache CreateCacheImplementation(string name)
        {
            return IocManager.Resolve<AbpMemoryCache>(new { name });
        }
    }
}

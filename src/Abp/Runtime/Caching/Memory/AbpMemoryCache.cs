using System;
using System.Runtime.Caching;

namespace Abp.Runtime.Caching.Memory
{
    /// <summary>
    /// Implements <see cref="ICache"/> to work with <see cref="MemoryCache"/>.
    /// Abp内存缓存
    /// </summary>
    public class AbpMemoryCache : CacheBase
    {
        private MemoryCache _memoryCache;

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="name">Unique name of the cache 缓存的唯一名称</param>
        public AbpMemoryCache(string name)
            : base(name)
        {
            _memoryCache = new MemoryCache(Name);
        }

        /// <summary>
        /// 获取默认值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public override object GetOrDefault(string key)
        {
            return _memoryCache.Get(key);
        }
        
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="slidingExpireTime">过期时间</param>
        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            //缓存不能插入null值
            if (value == null)
            {
                throw new AbpException("Can not insert null values to the cache!");
            }

            //TODO: Optimize by using a default CacheItemPolicy?
            //通过使用默认CacheItemPolicy优化？
            _memoryCache.Set(
                key,
                value,
                new CacheItemPolicy
                {
                    SlidingExpiration = slidingExpireTime ?? DefaultSlidingExpireTime
                });
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">键</param>
        public override void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public override void Clear()
        {
            _memoryCache.Dispose();
            _memoryCache = new MemoryCache(Name);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public override void Dispose()
        {
            _memoryCache.Dispose();
            base.Dispose();
        }
    }
}
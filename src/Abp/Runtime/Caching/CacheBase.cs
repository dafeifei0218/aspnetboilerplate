using System;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Base class for caches.
    /// It's used to simplify implementing <see cref="ICache"/>.
    /// 缓存基类
    /// </summary>
    public abstract class CacheBase : ICache
    {
        /// <summary>
        /// 缓存名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 默认失效时间
        /// </summary>
        public TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// 同步对象
        /// </summary>
        protected readonly object SyncObj = new object();

        //异步锁
        private readonly AsyncLock _asyncLock = new AsyncLock();

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="name">缓存名称</param>
        protected CacheBase(string name)
        {
            Name = name;
            DefaultSlidingExpireTime = TimeSpan.FromHours(1);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="factory">工厂</param>
        /// <returns></returns>
        public virtual object Get(string key, Func<string, object> factory)
        {
            var cacheKey = key;
            var item = GetOrDefault(key);
            if (item == null)
            {
                lock (SyncObj)
                {
                    item = GetOrDefault(key);
                    if (item == null)
                    {
                        item = factory(key);
                        if (item == null)
                        {
                            return null;
                        }

                        Set(cacheKey, item);
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// 获取缓存-异步
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="factory">工厂</param>
        /// <returns></returns>
        public virtual async Task<object> GetAsync(string key, Func<string, Task<object>> factory)
        {
            var cacheKey = key;
            var item = await GetOrDefaultAsync(key);
            if (item == null)
            {
                using (await _asyncLock.LockAsync())
                {
                    item = await GetOrDefaultAsync(key);
                    if (item == null)
                    {
                        item = await factory(key);
                        if (item == null)
                        {
                            return null;
                        }

                        await SetAsync(cacheKey, item);
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// 获取缓存或者默认值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public abstract object GetOrDefault(string key);

        /// <summary>
        /// 获取缓存或者默认值-异步
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public virtual Task<object> GetOrDefaultAsync(string key)
        {
            return Task.FromResult(GetOrDefault(key));
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="slidingExpireTime">失效时间</param>
        public abstract void Set(string key, object value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="slidingExpireTime">失效时间</param>
        /// <returns></returns>
        public virtual Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            Set(key, value, slidingExpireTime);
            return Task.FromResult(0);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">键</param>
        public abstract void Remove(string key);

        /// <summary>
        /// 删除缓存-异步
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public virtual Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.FromResult(0);
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// 清除缓存-异步
        /// </summary>
        /// <returns></returns>
        public virtual Task ClearAsync()
        {
            Clear();
            return Task.FromResult(0);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public virtual void Dispose()
        {

        }
    }
}
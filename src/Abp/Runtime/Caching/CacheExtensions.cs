using System;
using System.Threading.Tasks;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Extension methods for <see cref="ICache"/>.
    /// 缓存扩展类
    /// </summary>
    public static class CacheExtensions
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <param name="key">键</param>
        /// <param name="factory">工厂</param>
        /// <returns></returns>
        public static object Get(this ICache cache, string key, Func<object> factory)
        {
            return cache.Get(key, k => factory());
        }

        /// <summary>
        /// 获取缓存-异步
        /// </summary>
        /// <param name="cache">缓存</param>
        /// <param name="key">键</param>
        /// <param name="factory">工厂</param>
        /// <returns></returns>
        public static Task<object> GetAsync(this ICache cache, string key, Func<Task<object>> factory)
        {
            return cache.GetAsync(key, k => factory());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey">泛型的键</typeparam>
        /// <typeparam name="TValue">泛型的值</typeparam>
        /// <param name="cache">缓存</param>
        /// <returns></returns>
        public static ITypedCache<TKey, TValue> AsTyped<TKey, TValue>(this ICache cache)
        {
            return new TypedCacheWrapper<TKey, TValue>(cache);
        }
        
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="TKey">泛型的键</typeparam>
        /// <typeparam name="TValue">泛型的值</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">键</param>
        /// <param name="factory">工厂</param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this ICache cache, TKey key, Func<TKey, TValue> factory)
        {
            return (TValue)cache.Get(key.ToString(), (k) => (object)factory(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey">泛型的键</typeparam>
        /// <typeparam name="TValue">泛型的值</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">键</param>
        /// <param name="factory">工厂</param>
        /// <returns></returns>
        public static TValue Get<TKey, TValue>(this ICache cache, TKey key, Func<TValue> factory)
        {
            return cache.Get(key, (k) => factory());
        }

        /// <summary>
        /// 获取缓存-异步
        /// </summary>
        /// <typeparam name="TKey">泛型的键</typeparam>
        /// <typeparam name="TValue">泛型的值</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">键</param>
        /// <param name="factory">工厂</param>
        /// <returns></returns>
        public static async Task<TValue> GetAsync<TKey, TValue>(this ICache cache, TKey key, Func<TKey, Task<TValue>> factory)
        {
            var value = await cache.GetAsync(key.ToString(), async (keyAsString) =>
            {
                var v = await factory(key);
                return (object)v;
            });

            return (TValue)value;
        }

        /// <summary>
        /// 获取缓存-异步
        /// </summary>
        /// <typeparam name="TKey">泛型的键</typeparam>
        /// <typeparam name="TValue">泛型的值</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">键</param>
        /// <param name="factory">工厂</param>
        /// <returns></returns>
        public static Task<TValue> GetAsync<TKey, TValue>(this ICache cache, TKey key, Func<Task<TValue>> factory)
        {
            return cache.GetAsync(key, (k) => factory());
        }

        /// <summary>
        /// 获取缓存，如果值为null，则不报错
        /// </summary>
        /// <typeparam name="TKey">泛型的键</typeparam>
        /// <typeparam name="TValue">泛型的值</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static TValue GetOrDefault<TKey, TValue>(this ICache cache, TKey key)
        {
            var value = cache.GetOrDefault(key.ToString());
            if (value == null)
            {
                return default(TValue);
            }

            return (TValue) value;
        }

        /// <summary>
        /// 获取缓存-异步，如果值为null，则不报错
        /// </summary>
        /// <typeparam name="TKey">泛型的键</typeparam>
        /// <typeparam name="TValue">泛型的值</typeparam>
        /// <param name="cache">缓存</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static async Task<TValue> GetOrDefaultAsync<TKey, TValue>(this ICache cache, TKey key)
        {
            var value = await cache.GetOrDefaultAsync(key.ToString());
            if (value == null)
            {
                return default(TValue);
            }

            return (TValue)value;
        }
    }
}
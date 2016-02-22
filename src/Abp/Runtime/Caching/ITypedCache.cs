using System;
using System.Threading.Tasks;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// An interface to work with cache in a typed manner.
    /// Use <see cref="CacheExtensions.AsTyped{TKey,TValue}"/> method
    /// to convert a <see cref="ICache"/> to this interface.
    /// ���ͻ���ӿ�
    /// </summary>
    /// <typeparam name="TKey">Key type for cache items ������Ŀ������</typeparam>
    /// <typeparam name="TValue">Value type for cache items</typeparam>
    public interface ITypedCache<TKey, TValue> : IDisposable
    {
        /// <summary>
        /// Unique name of the cache.
        /// ����Ψһ����
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Default sliding expire time of cache items.
        /// Ĭ�Ϲ���ʱ��
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// Gets the internal cache.
        /// ��ȡ�ڲ�����
        /// </summary>
        ICache InternalCache { get; }

        /// <summary>
        /// Gets an item from the cache.
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="key">Key ��</param>
        /// <param name="factory">Factory method to create cache item if not exists ��������������������������</param>
        /// <returns>Cached item ����ֵ</returns>
        TValue Get(TKey key, Func<TKey, TValue> factory);

        /// <summary>
        /// Gets an item from the cache.
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="key">Key ��</param>
        /// <param name="factory">Factory method to create cache item if not exists ��������������������������</param>
        /// <returns>Cached item ����ֵ</returns>
        Task<TValue> GetAsync(TKey key, Func<TKey, Task<TValue>> factory);

        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// ��ȡ���������δ�ҵ��򷵻�null
        /// </summary>
        /// <param name="key">Key ��</param>
        /// <returns>Cached item or null if not found ���������δ�ҵ��򷵻�null</returns>
        TValue GetOrDefault(TKey key);

        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// ��ȡ���������δ�ҵ��򷵻�null-�첽
        /// </summary>
        /// <param name="key">Key ��</param>
        /// <returns>Cached item or null if not found ���������δ�ҵ��򷵻�null</returns>
        Task<TValue> GetOrDefaultAsync(TKey key);

        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// ���ݼ�����/��д�������
        /// </summary>
        /// <param name="key">Key ��</param>
        /// <param name="value">Value ֵ</param>
        /// <param name="slidingExpireTime">Sliding expire time ����ʱ��</param>
        void Set(TKey key, TValue value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// ���ݼ�����/��д�������-�첽
        /// </summary>
        /// <param name="key">Key ��</param>
        /// <param name="value">Value ֵ</param>
        /// <param name="slidingExpireTime">Sliding expire time ����ʱ��</param>
        Task SetAsync(TKey key, TValue value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// Removes a cache item by it's key (does nothing if given key does not exists in the cache).
        /// ���ݼ�ɾ������
        /// </summary>
        /// <param name="key">Key ��</param>
        void Remove(TKey key);

        /// <summary>
        /// Removes a cache item by it's key.
        /// ���ݼ�ɾ������
        /// </summary>
        /// <param name="key">Key ��</param>
        Task RemoveAsync(TKey key);

        /// <summary>
        /// Clears all items in this cache.
        /// �������������Ŀ
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears all items in this cache.
        /// �������������Ŀ-�첽
        /// </summary>
        Task ClearAsync();
    }
}
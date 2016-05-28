using System;
using System.Threading.Tasks;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Defines a cache that can be store and get items by keys.
    /// ����ӿڣ�
    /// ����һ�����棬����ͨ������洢�ͻ�ȡ��Ŀ��
    /// </summary>
    public interface ICache : IDisposable
    {
        /// <summary>
        /// Unique name of the cache.
        /// ����Ψһ����
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Default sliding expire time of cache items.
        /// Default value: 60 minutes. Can be changed by configuration.
        /// Ĭ�ϻ������ʱ��
        /// </summary>
        TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// Gets an item from the cache.
        /// ��ȡ�������
        /// </summary>
        /// <param name="key">Key ����������һ����Ŀ�ļ����ַ������ͣ�</param>
        /// <param name="factory">Factory method to create cache item if not exists ��������������������������</param>
        /// <returns>Cached item �������</returns>
        object Get(string key, Func<string, object> factory);

        /// <summary>
        /// Gets an item from the cache.
        /// ��ȡ�������
        /// </summary>
        /// <param name="key">Key ��</param>
        /// <param name="factory">Factory method to create cache item if not exists ��������</param>
        /// <returns>Cached item �������</returns>
        Task<object> GetAsync(string key, Func<string, Task<object>> factory);

        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// ��ȡ���������δ�ҵ��򷵻�null
        /// </summary>
        /// <param name="key">Key ��</param>
        /// <returns>Cached item or null if not found ���������δ�ҵ��򷵻�null</returns>
        object GetOrDefault(string key);

        /// <summary>
        /// Gets an item from the cache or null if not found.
        /// ��ȡ���������δ�ҵ��򷵻�null
        /// </summary>
        /// <param name="key">Key ��</param>
        /// <returns>Cached item or null if not found ���������δ�ҵ��򷵻�null</returns>
        Task<object> GetOrDefaultAsync(string key);

        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// ���ݼ�����/��д�������
        /// </summary>
        /// <param name="key">Key ��</param>
        /// <param name="value">Value ֵ</param>
        /// <param name="slidingExpireTime">Sliding expire time</param>
        void Set(string key, object value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// Saves/Overrides an item in the cache by a key.
        /// ���ݼ�����/��д�������
        /// </summary>
        /// <param name="key">Key ��</param>
        /// <param name="value">Value ֵ</param>
        /// <param name="slidingExpireTime">Sliding expire time </param>
        Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// Removes a cache item by it's key.
        /// ɾ������
        /// </summary>
        /// <param name="key">Key ��</param>
        void Remove(string key);

        /// <summary>
        /// Removes a cache item by it's key (does nothing if given key does not exists in the cache).
        /// ɾ������-�첽
        /// </summary>
        /// <param name="key">Key ��</param>
        Task RemoveAsync(string key);

        /// <summary>
        /// Clears all items in this cache.
        /// ���ȫ��������
        /// </summary>
        void Clear();

        /// <summary>
        /// Clears all items in this cache.
        /// ���ȫ��������-�첽
        /// </summary>
        Task ClearAsync();
    }
}
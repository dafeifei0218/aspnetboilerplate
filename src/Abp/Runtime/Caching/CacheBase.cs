using System;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Abp.Runtime.Caching
{
    /// <summary>
    /// Base class for caches.
    /// It's used to simplify implementing <see cref="ICache"/>.
    /// �������
    /// </summary>
    public abstract class CacheBase : ICache
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Ĭ��ʧЧʱ��
        /// </summary>
        public TimeSpan DefaultSlidingExpireTime { get; set; }

        /// <summary>
        /// ͬ������
        /// </summary>
        protected readonly object SyncObj = new object();

        //�첽��
        private readonly AsyncLock _asyncLock = new AsyncLock();

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        /// <param name="name">��������</param>
        protected CacheBase(string name)
        {
            Name = name;
            DefaultSlidingExpireTime = TimeSpan.FromHours(1);
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="factory">����</param>
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
        /// ��ȡ����-�첽
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="factory">����</param>
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
        /// ��ȡ�������Ĭ��ֵ
        /// </summary>
        /// <param name="key">��</param>
        /// <returns></returns>
        public abstract object GetOrDefault(string key);

        /// <summary>
        /// ��ȡ�������Ĭ��ֵ-�첽
        /// </summary>
        /// <param name="key">��</param>
        /// <returns></returns>
        public virtual Task<object> GetOrDefaultAsync(string key)
        {
            return Task.FromResult(GetOrDefault(key));
        }

        /// <summary>
        /// ���û���
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
        /// <param name="slidingExpireTime">ʧЧʱ��</param>
        public abstract void Set(string key, object value, TimeSpan? slidingExpireTime = null);

        /// <summary>
        /// ���û���
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
        /// <param name="slidingExpireTime">ʧЧʱ��</param>
        /// <returns></returns>
        public virtual Task SetAsync(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            Set(key, value, slidingExpireTime);
            return Task.FromResult(0);
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="key">��</param>
        public abstract void Remove(string key);

        /// <summary>
        /// ɾ������-�첽
        /// </summary>
        /// <param name="key">��</param>
        /// <returns></returns>
        public virtual Task RemoveAsync(string key)
        {
            Remove(key);
            return Task.FromResult(0);
        }

        /// <summary>
        /// �������
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// �������-�첽
        /// </summary>
        /// <returns></returns>
        public virtual Task ClearAsync()
        {
            Clear();
            return Task.FromResult(0);
        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual void Dispose()
        {

        }
    }
}
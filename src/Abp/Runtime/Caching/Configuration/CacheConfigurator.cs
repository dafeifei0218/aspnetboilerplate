using System;

namespace Abp.Runtime.Caching.Configuration
{
    /// <summary>
    /// ����������
    /// </summary>
    internal class CacheConfigurator : ICacheConfigurator
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string CacheName { get; private set; }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public Action<ICache> InitAction { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="initAction">��ʼ������</param>
        public CacheConfigurator(Action<ICache> initAction)
        {
            InitAction = initAction;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="cacheName">��������</param>
        /// <param name="initAction">��ʼ������</param>
        public CacheConfigurator(string cacheName, Action<ICache> initAction)
        {
            CacheName = cacheName;
            InitAction = initAction;
        }
    }
}
using System;

namespace Abp.Runtime.Caching.Configuration
{
    /// <summary>
    /// A registered cache configurator.
    /// �����������ӿ�
    /// </summary>
    public interface ICacheConfigurator
    {
        /// <summary>
        /// Name of the cache.
        /// It will be null if this configurator configures all caches.
        /// �������ƣ�����������������л��棬�����ǿյ�
        /// </summary>
        string CacheName { get; }

        /// <summary>
        /// Configuration action. Called just after the cache is created.
        /// ��ʼ���������ڻ��洴����������ö���
        /// </summary>
        Action<ICache> InitAction { get; }
    }
}
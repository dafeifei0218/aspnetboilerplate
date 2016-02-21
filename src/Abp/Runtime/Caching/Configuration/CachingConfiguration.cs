using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Abp.Runtime.Caching.Configuration
{
    /// <summary>
    /// ��������
    /// </summary>
    internal class CachingConfiguration : ICachingConfiguration
    {
        /// <summary>
        /// ���������б�
        /// </summary>
        public IReadOnlyList<ICacheConfigurator> Configurators
        {
            get { return _configurators.ToImmutableList(); }
        }

        //���������б�
        private readonly List<ICacheConfigurator> _configurators;

        /// <summary>
        /// ���캯��
        /// </summary>
        public CachingConfiguration()
        {
            _configurators = new List<ICacheConfigurator>();
        }

        /// <summary>
        /// ȫ������
        /// </summary>
        /// <param name="initAction">��ʼ������</param>
        public void ConfigureAll(Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(initAction));
        }

        /// <summary>
        /// ���������ض��Ļ���
        /// </summary>
        /// <param name="cacheName">��������</param>
        /// <param name="initAction">��ʼ������</param>
        public void Configure(string cacheName, Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(cacheName, initAction));
        }
    }
}
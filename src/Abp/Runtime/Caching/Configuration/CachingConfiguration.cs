using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Configuration.Startup;

namespace Abp.Runtime.Caching.Configuration
{
    /// <summary>
    /// 缓存配置
    /// </summary>
    internal class CachingConfiguration : ICachingConfiguration
    {
        /// <summary>
        /// 获取ABP启动配置
        /// </summary>
        public IAbpStartupConfiguration AbpConfiguration { get; private set; }

        /// <summary>
        /// 缓存配置列表
        /// </summary>
        public IReadOnlyList<ICacheConfigurator> Configurators
        {
            get { return _configurators.ToImmutableList(); }
        }

        /// <summary>
        /// 缓存配置列表
        /// </summary>
        private readonly List<ICacheConfigurator> _configurators;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public CachingConfiguration(IAbpStartupConfiguration abpConfiguration)
        {
            AbpConfiguration = abpConfiguration;

            _configurators = new List<ICacheConfigurator>();
        }

        /// <summary>
        /// 全部缓存
        /// </summary>
        /// <param name="initAction">初始化动作</param>
        public void ConfigureAll(Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(initAction));
        }

        /// <summary>
        /// 用于配置特定的缓存
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <param name="initAction">初始化动作</param>
        public void Configure(string cacheName, Action<ICache> initAction)
        {
            _configurators.Add(new CacheConfigurator(cacheName, initAction));
        }
    }
}
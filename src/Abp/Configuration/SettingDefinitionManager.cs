﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Configuration.Startup;
using Abp.Dependency;

namespace Abp.Configuration
{
    /// <summary>
    /// Implements <see cref="ISettingDefinitionManager"/>.
    /// 设置定义管理类
    /// </summary>
    /// <remarks>
    /// 主要完成注册到ABP中的SettingDefinition初始化。
    /// 首先通过ISettingsConfiguration实例获取SettingProviders集合，然后在Initialize方法中通过SettingProviders获取SettingDefinition的数组。
    /// 并将其保存在Dictionary中，其key就是SettingDefinition的Name
    /// </remarks>
    internal class SettingDefinitionManager : ISettingDefinitionManager, ISingletonDependency
    {
        private readonly IIocManager _iocManager;
        private readonly ISettingsConfiguration _settingsConfiguration;
        private readonly IDictionary<string, SettingDefinition> _settings;

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        public SettingDefinitionManager(IIocManager iocManager, ISettingsConfiguration settingsConfiguration)
        {
            _iocManager = iocManager;
            _settingsConfiguration = settingsConfiguration;
            _settings = new Dictionary<string, SettingDefinition>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            //此方法是在AbpKernelModule的PostInitialize方法执行的时候被调用。

            var context = new SettingDefinitionProviderContext();

            foreach (var providerType in _settingsConfiguration.Providers)
            {
                var provider = CreateProvider(providerType);
                foreach (var settings in provider.GetSettingDefinitions(context))
                {
                    _settings[settings.Name] = settings;
                }
            }
        }

        /// <summary>
        /// 获取设置定义
        /// </summary>
        /// <param name="name">设置名称</param>
        /// <returns></returns>
        public SettingDefinition GetSettingDefinition(string name)
        {
            SettingDefinition settingDefinition;
            if (!_settings.TryGetValue(name, out settingDefinition))
            {
                throw new AbpException("There is no setting defined with name: " + name);
            }

            return settingDefinition;
        }

        /// <summary>
        /// 获取全部设置定义
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<SettingDefinition> GetAllSettingDefinitions()
        {
            return _settings.Values.ToImmutableList();
        }

        /// <summary>
        /// 创建设置提供者
        /// </summary>
        /// <param name="providerType">提供者类型</param>
        /// <returns></returns>
        private SettingProvider CreateProvider(Type providerType)
        {
            if (!_iocManager.IsRegistered(providerType))
            {
                _iocManager.Register(providerType);
            }

            return (SettingProvider)_iocManager.Resolve(providerType);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Localization.Dictionaries;
using Abp.Localization.Sources;
using Castle.Core.Logging;

namespace Abp.Localization
{
    /// <summary>
    /// 本地化管理类
    /// </summary>
    internal class LocalizationManager : ILocalizationManager
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets current language for the application.
        /// 当前语言
        /// </summary>
        [Obsolete("Inject ILanguageManager and use ILanguageManager.CurrentLanguage.")]
        public LanguageInfo CurrentLanguage { get { return _languageManager.CurrentLanguage; } }

        private readonly ILanguageManager _languageManager;
        private readonly ILocalizationConfiguration _configuration;
        private readonly IIocResolver _iocResolver;
        private readonly IDictionary<string, ILocalizationSource> _sources;

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        public LocalizationManager(
            ILanguageManager languageManager,
            ILocalizationConfiguration configuration, 
            IIocResolver iocResolver)
        {
            Logger = NullLogger.Instance;
            _languageManager = languageManager;
            _configuration = configuration;
            _iocResolver = iocResolver;
            _sources = new Dictionary<string, ILocalizationSource>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            InitializeSources();
        }

        /// <summary>
        /// 获取全部语言信息
        /// </summary>
        /// <returns></returns>
        [Obsolete("Inject ILanguageManager and use ILanguageManager.GetLanguages().")]
        public IReadOnlyList<LanguageInfo> GetAllLanguages()
        {
            return _languageManager.GetLanguages();
        }

        /// <summary>
        /// 初始化源
        /// </summary>
        /// <remarks>
        /// LocalizationManager通过调用InitializeSources初始化和load本地化资源文件中的内容到IDictionary实例对象_sources中
        /// </remarks>
        private void InitializeSources()
        {
            if (!_configuration.IsEnabled)
            {
                Logger.Debug("Localization disabled.");
                return;
            }

            //遍历LocalizationConfiguration中的ILocalizationSourceList实例，
            //通过其ILocalizationSource的ILocalizationDictionaryProvider实例完成本地化资源的初始化。
            //提供GetString方法返回本地化的string。
            //LocalizationManager维护了一个ILocalizationSource对象的字典用于维护所有的本地化资源。
            Logger.Debug(string.Format("Initializing {0} localization sources.", _configuration.Sources.Count));
            foreach (var source in _configuration.Sources)
            {
                if (_sources.ContainsKey(source.Name))
                {
                    throw new AbpException("There are more than one source with name: " + source.Name + "! Source name must be unique!");
                }

                _sources[source.Name] = source;
                source.Initialize(_configuration, _iocResolver);

                //Extending dictionaries
                //扩展信息字典
                if (source is IDictionaryBasedLocalizationSource)
                {
                    var dictionaryBasedSource = source as IDictionaryBasedLocalizationSource;
                    var extensions = _configuration.Sources.Extensions.Where(e => e.SourceName == source.Name).ToList();
                    foreach (var extension in extensions)
                    {
                        extension.DictionaryProvider.Initialize(source.Name);
                        foreach (var extensionDictionary in extension.DictionaryProvider.Dictionaries.Values)
                        {
                            dictionaryBasedSource.Extend(extensionDictionary);
                        }
                    }
                }

                Logger.Debug("Initialized localization source: " + source.Name);
            }
        }

        /// <summary>
        /// Gets a localization source with name.
        /// 根据名称获取本地化资源
        /// </summary>
        /// <param name="name">Unique name of the localization source 本地化资源名称</param>
        /// <returns>The localization source 本地化资源</returns>
        public ILocalizationSource GetSource(string name)
        {
            // 如果未启用，则初始化空本地化源
            if (!_configuration.IsEnabled)
            {
                return NullLocalizationSource.Instance;
            }

            // 如果本地化名称为空，则抛出异常
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            ILocalizationSource source;
            if (!_sources.TryGetValue(name, out source))
            {
                throw new AbpException("Can not find a source with name: " + name);
            }

            return source;
        }

        /// <summary>
        /// Gets all registered localization sources.
        /// 获取全部注册的本地化资源
        /// </summary>
        /// <returns>List of sources 本地化源列表</returns>
        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _sources.Values.ToImmutableList();
        }
    }
}
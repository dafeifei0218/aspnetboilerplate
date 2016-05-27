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
    /// ���ػ�������
    /// </summary>
    internal class LocalizationManager : ILocalizationManager
    {
        /// <summary>
        /// ��־
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets current language for the application.
        /// ��ǰ����
        /// </summary>
        [Obsolete("Inject ILanguageManager and use ILanguageManager.CurrentLanguage.")]
        public LanguageInfo CurrentLanguage { get { return _languageManager.CurrentLanguage; } }

        private readonly ILanguageManager _languageManager;
        private readonly ILocalizationConfiguration _configuration;
        private readonly IIocResolver _iocResolver;
        private readonly IDictionary<string, ILocalizationSource> _sources;

        /// <summary>
        /// Constructor.
        /// ���캯��
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
        /// ��ʼ��
        /// </summary>
        public void Initialize()
        {
            InitializeSources();
        }

        /// <summary>
        /// ��ȡȫ��������Ϣ
        /// </summary>
        /// <returns></returns>
        [Obsolete("Inject ILanguageManager and use ILanguageManager.GetLanguages().")]
        public IReadOnlyList<LanguageInfo> GetAllLanguages()
        {
            return _languageManager.GetLanguages();
        }

        /// <summary>
        /// ��ʼ��Դ
        /// </summary>
        /// <remarks>
        /// LocalizationManagerͨ������InitializeSources��ʼ����load���ػ���Դ�ļ��е����ݵ�IDictionaryʵ������_sources��
        /// </remarks>
        private void InitializeSources()
        {
            if (!_configuration.IsEnabled)
            {
                Logger.Debug("Localization disabled.");
                return;
            }

            //����LocalizationConfiguration�е�ILocalizationSourceListʵ����
            //ͨ����ILocalizationSource��ILocalizationDictionaryProviderʵ����ɱ��ػ���Դ�ĳ�ʼ����
            //�ṩGetString�������ر��ػ���string��
            //LocalizationManagerά����һ��ILocalizationSource������ֵ�����ά�����еı��ػ���Դ��
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
                //��չ��Ϣ�ֵ�
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
        /// �������ƻ�ȡ���ػ���Դ
        /// </summary>
        /// <param name="name">Unique name of the localization source ���ػ���Դ����</param>
        /// <returns>The localization source ���ػ���Դ</returns>
        public ILocalizationSource GetSource(string name)
        {
            // ���δ���ã����ʼ���ձ��ػ�Դ
            if (!_configuration.IsEnabled)
            {
                return NullLocalizationSource.Instance;
            }

            // ������ػ�����Ϊ�գ����׳��쳣
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
        /// ��ȡȫ��ע��ı��ػ���Դ
        /// </summary>
        /// <returns>List of sources ���ػ�Դ�б�</returns>
        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _sources.Values.ToImmutableList();
        }
    }
}
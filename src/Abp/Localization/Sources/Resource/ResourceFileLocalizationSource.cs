using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;
using Abp.Configuration.Startup;
using Abp.Dependency;

namespace Abp.Localization.Sources.Resource
{
    /// <summary>
    /// This class is used to simplify to create a localization source that
    /// uses resource a file.
    /// 资源文件本地化源
    /// </summary>
    public class ResourceFileLocalizationSource : ILocalizationSource, ISingletonDependency
    {
        /// <summary>
        /// Unique Name of the source.
        /// 本地化源名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Reference to the <see cref="ResourceManager"/> object related to this localization source.
        /// 资源文件管理类
        /// </summary>
        public ResourceManager ResourceManager { get; private set; }

        /// <summary>
        /// 本地化配置
        /// </summary>
        private ILocalizationConfiguration _configuration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">Unique Name of the source 本地化源名称</param>
        /// <param name="resourceManager">Reference to the <see cref="ResourceManager"/> object related to this localization source</param>
        public ResourceFileLocalizationSource(string name, ResourceManager resourceManager)
        {
            Name = name;
            ResourceManager = resourceManager;
        }

        /// <summary>
        /// This method is called by ABP before first usage.
        /// 初始化
        /// </summary>
        public virtual void Initialize(ILocalizationConfiguration configuration, IIocResolver iocResolver)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual string GetString(string name)
        {
            var value = GetStringOrNull(name);
            if (value == null)
            {
                return ReturnGivenNameOrThrowException(name);
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public virtual string GetString(string name, CultureInfo culture)
        {
            var value = GetStringOrNull(name, culture);
            if (value == null)
            {
                return ReturnGivenNameOrThrowException(name);
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tryDefaults"></param>
        /// <returns></returns>
        public string GetStringOrNull(string name, bool tryDefaults = true)
        {
            //WARN: tryDefaults is not implemented!
            return ResourceManager.GetString(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="culture"></param>
        /// <param name="tryDefaults"></param>
        /// <returns></returns>
        public string GetStringOrNull(string name, CultureInfo culture, bool tryDefaults = true)
        {
            //WARN: tryDefaults is not implemented!
            return ResourceManager.GetString(name, culture);
        }

        /// <summary>
        /// Gets all strings in current language.
        /// </summary>
        public virtual IReadOnlyList<LocalizedString> GetAllStrings(bool includeDefaults = true)
        {
            return GetAllStrings(Thread.CurrentThread.CurrentUICulture, includeDefaults);
        }

        /// <summary>
        /// Gets all strings in specified culture.
        /// </summary>
        public virtual IReadOnlyList<LocalizedString> GetAllStrings(CultureInfo culture, bool includeDefaults = true)
        {
            return ResourceManager
                .GetResourceSet(culture, true, includeDefaults)
                .Cast<DictionaryEntry>()
                .Select(entry => new LocalizedString(entry.Key.ToString(), entry.Value.ToString(), culture))
                .ToImmutableList();
        }

        protected virtual string ReturnGivenNameOrThrowException(string name)
        {
            return LocalizationSourceHelper.ReturnGivenNameOrThrowException(_configuration, Name, name);
        }
    }
}

using System.Globalization;
using Abp.Configuration;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Localization.Sources;
using Castle.Core.Logging;

namespace Abp.BackgroundJobs
{
    /// <summary>
    /// Base class that can be used to implement <see cref="IBackgroundJob{TArgs}"/>.
    /// 后台工作，实现<see cref="IBackgroundJob{TArgs}"/>基类
    /// </summary>
    public abstract class BackgroundJob<TArgs> : IBackgroundJob<TArgs>
    {
        /// <summary>
        /// Reference to the setting manager.
        /// 设置管理
        /// </summary>
        public ISettingManager SettingManager { protected get; set; }

        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager"/>.
        /// 工作单元管理
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                {
                    throw new AbpException("Must set UnitOfWorkManager before use it.");
                }

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }
        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Gets current unit of work.
        /// 获取当前工作单元
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        /// <summary>
        /// Reference to the localization manager.
        /// 本地化管理
        /// </summary>
        public ILocalizationManager LocalizationManager { protected get; set; }

        /// <summary>
        /// Gets/sets name of the localization source that is used in this application service.
        /// It must be set in order to use <see cref="L(string)"/> and <see cref="L(string,CultureInfo)"/> methods.
        /// 获取/设置应用程序服务中使用的本地化源名称。
        /// 必须使用<see cref="L(string)"/>和<see cref="L(string,CultureInfo)"/>方法设置。
        /// </summary>
        protected string LocalizationSourceName { get; set; }

        /// <summary>
        /// Gets localization source.
        /// It's valid if <see cref="LocalizationSourceName"/> is set.
        /// 获取本地化源。
        /// 如果设置<see cref="LocalizationSourceName"/>本地化源名称，则有效。
        /// </summary>
        protected ILocalizationSource LocalizationSource
        {
            get
            {
                if (LocalizationSourceName == null)
                {
                    throw new AbpException("Must set LocalizationSourceName before, in order to get LocalizationSource");
                }

                if (_localizationSource == null || _localizationSource.Name != LocalizationSourceName)
                {
                    _localizationSource = LocalizationManager.GetSource(LocalizationSourceName);
                }

                return _localizationSource;
            }
        }
        private ILocalizationSource _localizationSource;

        /// <summary>
        /// Reference to the logger to write logs.
        /// 日志
        /// </summary>
        public ILogger Logger { protected get; set; }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        protected BackgroundJob()
        {
            Logger = NullLogger.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="args"></param>
        public abstract void Execute(TArgs args);

        /// <summary>
        /// Gets localized string for given key name and current language.
        /// 获取给定键名称的当前语言的本地化字符串。
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <returns>Localized string 本地化字符串</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// 获取给定键名称的当前语言的格式化参数的本地化字符串。
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <param name="args">Format arguments 格式化参数</param>
        /// <returns>Localized string 本地化字符串</returns>
        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        /// Gets localized string for given key name and specified culture information.
        /// 获取给定键名称的有关特定区域性信息的本地化字符串。
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <param name="culture">culture information 提供有关特定区域性的信息</param>
        /// <returns>Localized string 本地化字符串</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// 获取给定键名称和当前语言的有关特定区域性信息和格式化参数的本地化字符串。
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <param name="culture">culture information 提供有关特定区域性的信息</param>
        /// <param name="args">Format arguments 格式化参数</param>
        /// <returns>Localized string 本地化字符串</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }
    }
}
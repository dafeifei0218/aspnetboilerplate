using System.Globalization;
using Abp.Configuration;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.Localization.Sources;
using Castle.Core.Logging;

namespace Abp.Threading.BackgroundWorkers
{
    /// <summary>
    /// Base class that can be used to implement <see cref="IBackgroundWorker"/>.
    /// 后台工作基类，
    /// 可以用于实现<see cref="IBackgroundWorker"/>。
    /// </summary> 
    /// 同事添加了uow，Setting和本地化的一些辅助方法。
    public abstract class BackgroundWorkerBase : RunnableBase, IBackgroundWorker
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
        /// 本地化源名称
        /// </summary>
        protected string LocalizationSourceName { get; set; }

        /// <summary>
        /// Gets localization source.
        /// It's valid if <see cref="LocalizationSourceName"/> is set.
        /// 本地化源，
        /// 获取本地化源。
        /// 
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
        protected BackgroundWorkerBase()
        {
            Logger = NullLogger.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        /// <summary>
        /// 启动
        /// </summary>
        public override void Start()
        {
            base.Start();
            Logger.Debug("Start background worker: " + ToString());
        }

        /// <summary>
        /// 停止
        /// </summary>
        public override void Stop()
        {
            base.Stop();
            Logger.Debug("Stop background worker: " + ToString());
        }

        /// <summary>
        /// 等待停止
        /// </summary>
        public override void WaitToStop()
        {
            base.WaitToStop();
            Logger.Debug("WaitToStop background worker: " + ToString());
        }

        /// <summary>
        /// Gets localized string for given key name and current language.
        /// 获取给定的键名称和当前语言的本地化字符串
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <returns>Localized string 本地化字符串</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// 获取给定的键名称与格式化字符串的本地化字符串。
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
        /// 获取给定的键名称和有关特定区域性的信息的本地化字符串
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <param name="culture">culture information 有关特定区域性的信息</param>
        /// <returns>Localized string 本地化字符串</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// 获取给定的键名称和有关特定区域性的信息与格式化字符串的本地化字符串。
        /// </summary>
        /// <param name="name">Key name 键名称</param>
        /// <param name="culture">culture information 有关特定区域性的信息</param>
        /// <param name="args">Format arguments 格式化参数</param>
        /// <returns>Localized string 本地化字符串</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }

        public override string ToString()
        {
            return GetType().FullName;
        }
    }
}
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
    /// ��̨������ʵ��<see cref="IBackgroundJob{TArgs}"/>����
    /// </summary>
    public abstract class BackgroundJob<TArgs> : IBackgroundJob<TArgs>
    {
        /// <summary>
        /// Reference to the setting manager.
        /// ���ù���
        /// </summary>
        public ISettingManager SettingManager { protected get; set; }

        /// <summary>
        /// Reference to <see cref="IUnitOfWorkManager"/>.
        /// ������Ԫ����
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
        /// ��ȡ��ǰ������Ԫ
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork { get { return UnitOfWorkManager.Current; } }

        /// <summary>
        /// Reference to the localization manager.
        /// ���ػ�����
        /// </summary>
        public ILocalizationManager LocalizationManager { protected get; set; }

        /// <summary>
        /// Gets/sets name of the localization source that is used in this application service.
        /// It must be set in order to use <see cref="L(string)"/> and <see cref="L(string,CultureInfo)"/> methods.
        /// ��ȡ/����Ӧ�ó��������ʹ�õı��ػ�Դ���ơ�
        /// ����ʹ��<see cref="L(string)"/>��<see cref="L(string,CultureInfo)"/>�������á�
        /// </summary>
        protected string LocalizationSourceName { get; set; }

        /// <summary>
        /// Gets localization source.
        /// It's valid if <see cref="LocalizationSourceName"/> is set.
        /// ��ȡ���ػ�Դ��
        /// �������<see cref="LocalizationSourceName"/>���ػ�Դ���ƣ�����Ч��
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
        /// ��־
        /// </summary>
        public ILogger Logger { protected get; set; }

        /// <summary>
        /// Constructor.
        /// ���캯��
        /// </summary>
        protected BackgroundJob()
        {
            Logger = NullLogger.Instance;
            LocalizationManager = NullLocalizationManager.Instance;
        }

        /// <summary>
        /// ִ��
        /// </summary>
        /// <param name="args"></param>
        public abstract void Execute(TArgs args);

        /// <summary>
        /// Gets localized string for given key name and current language.
        /// ��ȡ���������Ƶĵ�ǰ���Եı��ػ��ַ�����
        /// </summary>
        /// <param name="name">Key name ������</param>
        /// <returns>Localized string ���ػ��ַ���</returns>
        protected virtual string L(string name)
        {
            return LocalizationSource.GetString(name);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// ��ȡ���������Ƶĵ�ǰ���Եĸ�ʽ�������ı��ػ��ַ�����
        /// </summary>
        /// <param name="name">Key name ������</param>
        /// <param name="args">Format arguments ��ʽ������</param>
        /// <returns>Localized string ���ػ��ַ���</returns>
        protected string L(string name, params object[] args)
        {
            return LocalizationSource.GetString(name, args);
        }

        /// <summary>
        /// Gets localized string for given key name and specified culture information.
        /// ��ȡ���������Ƶ��й��ض���������Ϣ�ı��ػ��ַ�����
        /// </summary>
        /// <param name="name">Key name ������</param>
        /// <param name="culture">culture information �ṩ�й��ض������Ե���Ϣ</param>
        /// <returns>Localized string ���ػ��ַ���</returns>
        protected virtual string L(string name, CultureInfo culture)
        {
            return LocalizationSource.GetString(name, culture);
        }

        /// <summary>
        /// Gets localized string for given key name and current language with formatting strings.
        /// ��ȡ���������ƺ͵�ǰ���Ե��й��ض���������Ϣ�͸�ʽ�������ı��ػ��ַ�����
        /// </summary>
        /// <param name="name">Key name ������</param>
        /// <param name="culture">culture information �ṩ�й��ض������Ե���Ϣ</param>
        /// <param name="args">Format arguments ��ʽ������</param>
        /// <returns>Localized string ���ػ��ַ���</returns>
        protected string L(string name, CultureInfo culture, params object[] args)
        {
            return LocalizationSource.GetString(name, culture, args);
        }
    }
}
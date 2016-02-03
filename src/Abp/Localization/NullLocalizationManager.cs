using System.Collections.Generic;
using System.Threading;
using Abp.Localization.Sources;

namespace Abp.Localization
{
    /// <summary>
    /// �ձ��ػ�������
    /// </summary>
    public class NullLocalizationManager : ILocalizationManager
    {
        /// <summary>
        /// Singleton instance.
        /// ����ʵ��
        /// </summary>
        public static NullLocalizationManager Instance { get { return SingletonInstance; } }
        private static readonly NullLocalizationManager SingletonInstance = new NullLocalizationManager();

        /// <summary>
        /// ��ǰ����
        /// </summary>
        public LanguageInfo CurrentLanguage { get { return new LanguageInfo(Thread.CurrentThread.CurrentUICulture.Name, Thread.CurrentThread.CurrentUICulture.DisplayName); } }
        
        //��������Ϣ�б�
        private readonly IReadOnlyList<LanguageInfo> _emptyLanguageArray = new LanguageInfo[0];

        //�ձ��ػ�Դ�б�
        private readonly IReadOnlyList<ILocalizationSource> _emptyLocalizationSourceArray = new ILocalizationSource[0];

        /// <summary>
        /// ���캯��
        /// </summary>
        private NullLocalizationManager()
        {
            
        }

        /// <summary>
        /// ��ȡȫ��������Ϣ�б�
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<LanguageInfo> GetAllLanguages()
        {
            return _emptyLanguageArray;
        }

        /// <summary>
        /// ��ȡ���ػ�Դ
        /// </summary>
        /// <param name="name">���ػ�Դ����</param>
        /// <returns></returns>
        public ILocalizationSource GetSource(string name)
        {
            return NullLocalizationSource.Instance;
        }

        /// <summary>
        /// ��ȡȫ�����ػ�Դ�б�
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _emptyLocalizationSourceArray;
        }
    }
}
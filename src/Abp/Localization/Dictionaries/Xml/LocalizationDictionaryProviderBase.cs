using System.Collections.Generic;

namespace Abp.Localization.Dictionaries.Xml
{
    /// <summary>
    /// ���ػ��ֵ��ṩ�߻���
    /// </summary>
    public abstract class LocalizationDictionaryProviderBase : ILocalizationDictionaryProvider
    {
        /// <summary>
        /// ���ػ�Դ����
        /// </summary>
        public string SourceName { get; private set; }

        /// <summary>
        /// Ĭ�ϱ��ػ��ֵ�
        /// </summary>
        public ILocalizationDictionary DefaultDictionary { get; protected set; }

        /// <summary>
        /// �ֵ�
        /// </summary>
        public IDictionary<string, ILocalizationDictionary> Dictionaries { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        protected LocalizationDictionaryProviderBase()
        {
            Dictionaries = new Dictionary<string, ILocalizationDictionary>();
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="sourceName">���ػ�Դ����</param>
        public virtual void Initialize(string sourceName)
        {
            SourceName = sourceName;
        }

        /// <summary>
        /// ��չ
        /// </summary>
        /// <param name="dictionary">���ػ��ֵ�</param>
        public void Extend(ILocalizationDictionary dictionary)
        {
            //Add
            //���
            ILocalizationDictionary existingDictionary;
            if (!Dictionaries.TryGetValue(dictionary.CultureInfo.Name, out existingDictionary))
            {
                Dictionaries[dictionary.CultureInfo.Name] = dictionary;
                return;
            }

            //Override
            //��д
            var localizedStrings = dictionary.GetAllStrings();
            foreach (var localizedString in localizedStrings)
            {
                existingDictionary[localizedString.Name] = localizedString.Value;
            }
        }
    }
}
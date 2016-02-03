using System.Collections.Generic;
using System.Globalization;

namespace Abp.Localization.Dictionaries
{
    /// <summary>
    /// Represents a dictionary that is used to find a localized string.
    /// ���ػ��ֵ�
    /// </summary>
    public interface ILocalizationDictionary
    {
        /// <summary>
        /// Culture of the dictionary.
        /// �ֵ��ض�������Ϣ
        /// </summary>
        CultureInfo CultureInfo { get; }

        /// <summary>
        /// Gets/sets a string for this dictionary with given name (key).
        /// ������
        /// </summary>
        /// <param name="name">Name to get/set ���ƵĻ�ȡ/����</param>
        string this[string name] { get; set; }

        /// <summary>
        /// Gets a <see cref="LocalizedString"/> for given <paramref name="name"/>.
        /// ���ݸ����ļ����ƣ���ȡ���ػ��ַ���
        /// </summary>
        /// <param name="name">Name (key) to get localized string ���ػ��ַ���������</param>
        /// <returns>The localized string or null if not found in this dictionary ���ػ��ַ���</returns>
        LocalizedString GetOrNull(string name);

        /// <summary>
        /// Gets a list of all strings in this dictionary.
        /// ��ȡȫ���ֵ��ַ���
        /// </summary>
        /// <returns>List of all <see cref="LocalizedString"/> object ���ػ��ַ����б�</returns>
        IReadOnlyList<LocalizedString> GetAllStrings();
    }
}
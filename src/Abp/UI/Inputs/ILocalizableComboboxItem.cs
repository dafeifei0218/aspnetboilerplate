using Abp.Localization;
using Newtonsoft.Json;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// ���ػ���������Ŀ�ӿ�
    /// </summary>
    public interface ILocalizableComboboxItem
    {
        /// <summary>
        /// ֵ
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// ��ʾ�ı�
        /// </summary>
        [JsonConverter(typeof(LocalizableStringToStringJsonConverter))]
        ILocalizableString DisplayText { get; set; }
    }
}
using Abp.Localization;
using Newtonsoft.Json;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// 本地化下拉框项目接口
    /// </summary>
    public interface ILocalizableComboboxItem
    {
        /// <summary>
        /// 值
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// 显示文本
        /// </summary>
        [JsonConverter(typeof(LocalizableStringToStringJsonConverter))]
        ILocalizableString DisplayText { get; set; }
    }
}
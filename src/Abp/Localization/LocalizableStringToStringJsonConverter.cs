using System;
using Newtonsoft.Json;

namespace Abp.Localization
{
    /// <summary>
    /// This class can be used to serialize <see cref="ILocalizableString"/> to <see cref="string"/> during serialization.
    /// It does not work for deserialization.
    /// 本地化字符串转换为Json字符串转换器
    /// </summary>
    public class LocalizableStringToStringJsonConverter : JsonConverter
    {
        /// <summary>
        /// 写Json
        /// </summary>
        /// <param name="writer">写</param>
        /// <param name="value">值</param>
        /// <param name="serializer">Json序列化</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var localizableString = (ILocalizableString) value;
            writer.WriteValue(localizableString.Localize(new LocalizationContext(LocalizationHelper.Manager)));
        }

        /// <summary>
        /// 读取Json
        /// </summary>
        /// <param name="reader">读</param>
        /// <param name="objectType">对象类型</param>
        /// <param name="existingValue">现存值</param>
        /// <param name="serializer">Json序列化</param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 是否可以转换
        /// </summary>
        /// <param name="objectType">对象类型</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof (ILocalizableString).IsAssignableFrom(objectType);
        }
    }
}
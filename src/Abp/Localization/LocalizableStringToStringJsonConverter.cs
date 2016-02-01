using System;
using Newtonsoft.Json;

namespace Abp.Localization
{
    /// <summary>
    /// This class can be used to serialize <see cref="ILocalizableString"/> to <see cref="string"/> during serialization.
    /// It does not work for deserialization.
    /// 
    /// </summary>
    public class LocalizableStringToStringJsonConverter : JsonConverter
    {
        /// <summary>
        /// дJson
        /// </summary>
        /// <param name="writer">д</param>
        /// <param name="value">ֵ</param>
        /// <param name="serializer">Json���л�</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var localizableString = (ILocalizableString) value;
            writer.WriteValue(localizableString.Localize());
        }

        /// <summary>
        /// ��ȡJson
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// �Ƿ����ת��
        /// </summary>
        /// <param name="objectType">��������</param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof (ILocalizableString).IsAssignableFrom(objectType);
        }
    }
}
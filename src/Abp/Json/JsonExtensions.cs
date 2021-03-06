﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Abp.Json
{
    /// <summary>
    /// Json扩展类
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Converts given object to JSON string.
        /// object转换为JSON字符串
        /// </summary>
        /// <param name="obj">object</param>
        /// <param name="camelCase">是否骆驼命名，默认为false</param>
        /// <param name="indented">是否缩进，默认为false</param>
        /// <returns></returns>
        public static string ToJsonString(this object obj, bool camelCase = false, bool indented = false)
        {
            var options = new JsonSerializerSettings();

            if (camelCase)
            {
                options.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }

            if (indented)
            {
                options.Formatting = Formatting.Indented;
            }

            return JsonConvert.SerializeObject(obj, options);
        }
    }
}

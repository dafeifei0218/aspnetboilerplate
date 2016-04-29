using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

/* This class is inspired from http://www.matskarlsson.se/blog/serialize-net-objects-as-camelcase-json */

namespace Abp.Web.Mvc.Controllers.Results
{
    /// <summary>
    /// This class is used to override returning Json results from MVC controllers.
    /// Abp Json结果
    /// </summary>
    public class AbpJsonResult : JsonResult
    {
        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        public AbpJsonResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        /// <summary>
        /// Constructor with JSON data.
        /// JSON数据的构造函数
        /// </summary>
        /// <param name="data">JSON data</param>
        public AbpJsonResult(object data)
            : this()
        {
            Data = data;
        }

        /// <summary>
        /// 执行结果
        /// </summary>
        /// <param name="context"></param>
        /// <inheritdoc/>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                //TODO: Make this static for performance reason?
                var jsonSerializerSettings = new JsonSerializerSettings
                                                 {
                                                     ContractResolver = new CamelCasePropertyNamesContractResolver()
                                                 };

                response.Write(JsonConvert.SerializeObject(Data, Formatting.Indented, jsonSerializerSettings));
            }
        }
    }
}
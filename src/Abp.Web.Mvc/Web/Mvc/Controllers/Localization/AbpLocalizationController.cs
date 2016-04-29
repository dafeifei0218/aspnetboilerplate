using System;
using System.Web;
using System.Web.Mvc;
using Abp.Auditing;
using Abp.Localization;
using Abp.Timing;
using Abp.Web.Mvc.Models;

namespace Abp.Web.Mvc.Controllers.Localization
{
    /// <summary>
    /// Abp本地化控制器
    /// </summary>
    public class AbpLocalizationController : AbpController
    {
        /// <summary>
        /// 变更区域信息
        /// </summary>
        /// <param name="cultureName">区域性名称</param>
        /// <param name="returnUrl">返回Url</param>
        /// <returns></returns>
        [DisableAuditing]
        public virtual ActionResult ChangeCulture(string cultureName, string returnUrl = "")
        {
            if (!GlobalizationHelper.IsValidCultureCode(cultureName))
            {
                throw new AbpException("Unknown language: " + cultureName + ". It must be a valid culture!");
            }

            Response.Cookies.Add(new HttpCookie("Abp.Localization.CultureName", cultureName) { Expires = Clock.Now.AddYears(2) });

            //如果是Ajax请求
            if (Request.IsAjaxRequest())
            {
                return Json(new MvcAjaxResponse(), JsonRequestBehavior.AllowGet);
            }

            //如果返回地址不为空
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect(Request.ApplicationPath);
        }
    }
}

using System.Web.Mvc;
using Abp.Auditing;

namespace Abp.Web.Mvc.Controllers
{
    /// <summary>
    /// Abp 视图控制器
    /// </summary>
    //TODO: Maybe it's better to write an HTTP handler for that instead of controller (since it's more light)
    public class AbpAppViewController : AbpController
    {
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="viewUrl">视图Url</param>
        /// <returns></returns>
        [DisableAuditing]
        public ActionResult Load(string viewUrl)
        {
            if (!viewUrl.StartsWith("~"))
            {
                viewUrl = "~" + viewUrl;
            }

            return View(viewUrl);
        }
    }
}

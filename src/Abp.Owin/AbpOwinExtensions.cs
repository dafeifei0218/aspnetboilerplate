using Abp.Threading;
using Owin;

namespace Abp.Owin
{
    /// <summary>
    /// OWIN extension methods for ABP.
    /// Abp Owin扩展类。
    /// </summary>
    public static class AbpOwinExtensions
    {
        /// <summary>
        /// Uses ABP.
        /// 使用Abp。
        /// </summary>
        public static void UseAbp(this IAppBuilder app)
        {
            ThreadCultureSanitizer.Sanitize();
        }
    }
}
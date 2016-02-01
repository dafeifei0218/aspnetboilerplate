using System.Globalization;

namespace Abp.Localization
{
    /// <summary>
    /// 全局化帮助类
    /// </summary>
    internal static class GlobalizationHelper
    {
        /// <summary>
        /// 是否有效的区域代码
        /// </summary>
        /// <param name="cultureCode">区域代码</param>
        /// <returns></returns>
        public static bool IsValidCultureCode(string cultureCode)
        {
            try
            {
                CultureInfo.GetCultureInfo(cultureCode);
                return true;
            }
            catch (CultureNotFoundException)
            {
                return false;
            }
        }
    }
}

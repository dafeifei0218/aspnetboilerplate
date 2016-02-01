using Abp.Configuration.Startup;
using Abp.Logging;

namespace Abp.Localization
{
    /// <summary>
    /// 本地化源帮助类
    /// </summary>
    public static class LocalizationSourceHelper
    {
        /// <summary>
        /// 返回给定名称或抛出异常
        /// </summary>
        /// <param name="configuration">本地化配置</param>
        /// <param name="sourceName">本地化源名称</param>
        /// <param name="name">本地化的键名称</param>
        /// <returns></returns>
        public static string ReturnGivenNameOrThrowException(ILocalizationConfiguration configuration, string sourceName, string name)
        {
            var exceptionMessage = string.Format(
                "Can not find '{0}' in localization source '{1}'!",
                name, sourceName
                );

            if (!configuration.ReturnGivenTextIfNotFound)
            {
                throw new AbpException(exceptionMessage);
            }

            LogHelper.Logger.Warn(exceptionMessage);

            return configuration.WrapGivenTextIfNotFound
                ? string.Format("[{0}]", name)
                : name;
        }
    }
}

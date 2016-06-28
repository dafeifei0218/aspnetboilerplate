using System.Collections.Generic;
using Abp.Configuration;
using Abp.Localization;

namespace Abp.Notifications
{
    /// <summary>
    /// 通知设置提供者
    /// </summary>
    /// <remarks>
    /// 用于初始化Notification功能模块的Setting，
    /// Notification功能模块的唯一一项Setting就是设置是否接收Notification。
    /// </remarks>
    public class NotificationSettingProvider : SettingProvider
    {
        /// <summary>
        /// 获取设置定义
        /// </summary>
        /// <param name="context">设置定义上下文</param>
        /// <returns></returns>
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(
                    NotificationSettingNames.ReceiveNotifications,
                    "true",
                    L("ReceiveNotifications"),
                    scopes: SettingScopes.User,
                    isVisibleToClients: true)
            };
        }

        /// <summary>
        /// 获取本地化字符串
        /// </summary>
        /// <param name="name">本地化字符串键名称</param>
        /// <returns></returns>
        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, AbpConsts.LocalizationSourceName);
        }
    }
}

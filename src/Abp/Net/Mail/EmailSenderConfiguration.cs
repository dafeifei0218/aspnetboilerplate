using System;
using Abp.Configuration;
using Abp.Extensions;

namespace Abp.Net.Mail
{
    /// <summary>
    /// Implementation of <see cref="IEmailSenderConfiguration"/> that reads settings
    /// from <see cref="ISettingManager"/>.
    /// 邮件发送配置
    /// </summary>
    public abstract class EmailSenderConfiguration : IEmailSenderConfiguration
    {
        /// <summary>
        /// 默认发送地址
        /// </summary>
        public string DefaultFromAddress
        {
            get { return GetNotEmptySettingValue(EmailSettingNames.DefaultFromAddress); }
        }

        /// <summary>
        /// 默认发送显示名
        /// </summary>
        public string DefaultFromDisplayName
        {
            get { return SettingManager.GetSettingValue(EmailSettingNames.DefaultFromDisplayName); }
        }

        //设置管理类
        protected readonly ISettingManager SettingManager;

        /// <summary>
        /// Creates a new <see cref="EmailSenderConfiguration"/>.
        /// 构造函数
        /// </summary>
        /// <param name="settingManager">设置管理类</param>
        protected EmailSenderConfiguration(ISettingManager settingManager)
        {
            SettingManager = settingManager;
        }

        /// <summary>
        /// Gets a setting value by checking. Throws <see cref="AbpException"/> if it's null or empty.
        /// 获取非空的设置值，如果值为null或空字符串，则抛出AbpException异常
        /// </summary>
        /// <param name="name">Name of the setting 设置名称</param>
        /// <returns>Value of the setting 设置值</returns>
        protected string GetNotEmptySettingValue(string name)
        {
            var value = SettingManager.GetSettingValue(name);
            if (value.IsNullOrEmpty())
            {
                throw new AbpException(String.Format("Setting value for '{0}' is null or empty!", name));
            }

            return value;
        }
    }
}
using Abp.Configuration;
using Abp.Dependency;

namespace Abp.Net.Mail.Smtp
{
    /// <summary>
    /// Implementation of <see cref="ISmtpEmailSenderConfiguration"/> that reads settings
    /// from <see cref="ISettingManager"/>.
    /// SMTP邮件发送配置
    /// </summary>
    public class SmtpEmailSenderConfiguration : EmailSenderConfiguration, ISmtpEmailSenderConfiguration, ITransientDependency
    {
        /// <summary>
        /// SMTP Host name/IP.
        /// SMTP的主机名/IP.
        /// </summary>
        public string Host
        {
            get { return GetNotEmptySettingValue(EmailSettingNames.Smtp.Host); }
        }

        /// <summary>
        /// SMTP Port.
        /// SMTP端口
        /// </summary>
        public int Port
        {
            get { return SettingManager.GetSettingValue<int>(EmailSettingNames.Smtp.Port); }
        }

        /// <summary>
        /// User name to login to SMTP server.
        /// SMTP服务器登录名
        /// </summary>
        public string UserName
        {
            get { return GetNotEmptySettingValue(EmailSettingNames.Smtp.UserName); }
        }

        /// <summary>
        /// Password to login to SMTP server.
        /// SMTP服务器密码
        /// </summary>
        public string Password
        {
            get { return GetNotEmptySettingValue(EmailSettingNames.Smtp.Password); }
        }

        /// <summary>
        /// Domain name to login to SMTP server.
        /// SMTP服务器登录域名
        /// </summary>
        public string Domain
        {
            get { return SettingManager.GetSettingValue(EmailSettingNames.Smtp.Domain); }
        }

        /// <summary>
        /// Is SSL enabled?
        /// 是否启用SSL安全套接字层加密链接
        /// </summary>
        public bool EnableSsl
        {
            get { return SettingManager.GetSettingValue<bool>(EmailSettingNames.Smtp.EnableSsl); }
        }

        /// <summary>
        /// Use default credentials?
        /// 是否使用默认凭据
        /// </summary>
        public bool UseDefaultCredentials
        {
            get { return SettingManager.GetSettingValue<bool>(EmailSettingNames.Smtp.UseDefaultCredentials); }
        }

        /// <summary>
        /// Creates a new <see cref="SmtpEmailSenderConfiguration"/>.
        /// 构造函数
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        public SmtpEmailSenderConfiguration(ISettingManager settingManager)
            : base(settingManager)
        {

        }
    }
}
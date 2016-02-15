using System.Net.Mail;

namespace Abp.Net.Mail.Smtp
{
    /// <summary>
    /// Defines configurations to used by <see cref="SmtpClient"/> object.
    /// SMTP邮件发送配置接口
    /// </summary>
    public interface ISmtpEmailSenderConfiguration : IEmailSenderConfiguration
    {
        /// <summary>
        /// SMTP Host name/IP.
        /// SMTP的主机名/IP.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// SMTP Port.
        /// SMTP端口.
        /// </summary>
        int Port { get; }

        /// <summary>
        /// User name to login to SMTP server.
        /// SMTP服务器登录名
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Password to login to SMTP server.
        /// SMTP服务器密码
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Domain name to login to SMTP server.
        /// SMTP服务器登录域名
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Is SSL enabled?
        /// 是否启用SSL安全套接字层加密链接
        /// </summary>
        bool EnableSsl { get; }

        /// <summary>
        /// Use default credentials?
        /// 是否使用默认凭据
        /// </summary>
        bool UseDefaultCredentials { get; }
    }
}
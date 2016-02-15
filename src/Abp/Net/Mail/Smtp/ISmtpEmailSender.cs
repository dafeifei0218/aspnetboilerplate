using System.Net.Mail;

namespace Abp.Net.Mail.Smtp
{
    /// <summary>
    /// Used to send emails over SMTP.
    /// 通过SMTP发送邮件接口。
    /// </summary>
    public interface ISmtpEmailSender : IEmailSender
    {
        /// <summary>
        /// Creates and configures new <see cref="SmtpClient"/> object to send emails. 
        /// 创建和配置SMTPClient对象发送电子邮件
        /// </summary>
        /// <returns>
        /// An <see cref="SmtpClient"/> object that is ready to send emails.
        /// 一个SmtpClient对象准备发送电子邮件
        /// </returns>
        SmtpClient BuildClient();
    }
}
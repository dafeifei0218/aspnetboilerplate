using System.Net.Mail;
using System.Threading.Tasks;
using Castle.Core.Logging;

namespace Abp.Net.Mail
{
    //TODO: Move this to Abp.TestBase?
    /// <summary>
    /// This class is an implementation of <see cref="IEmailSender"/> as similar to null pattern.
    /// It does not send emails but logs them.
    /// 邮件发送空实现
    /// </summary>
    public class NullEmailSender : EmailSenderBase
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Creates a new <see cref="NullEmailSender"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="configuration">Configuration 邮件发送配置</param>
        public NullEmailSender(IEmailSenderConfiguration configuration)
            : base(configuration)
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 发送邮件-异步
        /// </summary>
        /// <param name="mail">邮件消息</param>
        /// <returns></returns>
        protected override Task SendEmailAsync(MailMessage mail)
        {
            Logger.Debug("SendEmailAsync:");
            LogEmail(mail);
            return Task.FromResult(0);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mail">邮件消息</param>
        protected override void SendEmail(MailMessage mail)
        {
            Logger.Debug("SendEmail:");
            LogEmail(mail);
        }

        /// <summary>
        /// 邮件日志
        /// </summary>
        /// <param name="mail">邮件消息</param>
        private void LogEmail(MailMessage mail)
        {
            Logger.Debug(mail.To.ToString());
            Logger.Debug(mail.CC.ToString());
            Logger.Debug(mail.Subject);
            Logger.Debug(mail.Body);
        }
    }
}
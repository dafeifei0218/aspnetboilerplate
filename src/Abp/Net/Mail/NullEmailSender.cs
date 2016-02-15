using System.Net.Mail;
using System.Threading.Tasks;
using Castle.Core.Logging;

namespace Abp.Net.Mail
{
    //TODO: Move this to Abp.TestBase?
    /// <summary>
    /// This class is an implementation of <see cref="IEmailSender"/> as similar to null pattern.
    /// It does not send emails but logs them.
    /// �ʼ����Ϳ�ʵ��
    /// </summary>
    public class NullEmailSender : EmailSenderBase
    {
        /// <summary>
        /// ��־
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Creates a new <see cref="NullEmailSender"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="configuration">Configuration �ʼ���������</param>
        public NullEmailSender(IEmailSenderConfiguration configuration)
            : base(configuration)
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// �����ʼ�-�첽
        /// </summary>
        /// <param name="mail">�ʼ���Ϣ</param>
        /// <returns></returns>
        protected override Task SendEmailAsync(MailMessage mail)
        {
            Logger.Debug("SendEmailAsync:");
            LogEmail(mail);
            return Task.FromResult(0);
        }

        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mail">�ʼ���Ϣ</param>
        protected override void SendEmail(MailMessage mail)
        {
            Logger.Debug("SendEmail:");
            LogEmail(mail);
        }

        /// <summary>
        /// �ʼ���־
        /// </summary>
        /// <param name="mail">�ʼ���Ϣ</param>
        private void LogEmail(MailMessage mail)
        {
            Logger.Debug(mail.To.ToString());
            Logger.Debug(mail.CC.ToString());
            Logger.Debug(mail.Subject);
            Logger.Debug(mail.Body);
        }
    }
}
using System.Net.Mail;
using System.Threading.Tasks;

namespace Abp.Net.Mail
{
    /// <summary>
    /// This service can be used simply sending emails.
    /// Email发送接口
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email.
        /// 发送Email
        /// </summary>
        /// <param name="to">发送到</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">内容是否Html</param>
        Task SendAsync(string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// Sends an email.
        /// 发送Email
        /// </summary>
        /// <param name="to">发送到</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">内容是否Html</param>
        void Send(string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// Sends an email.
        /// 发送Email
        /// </summary>
        /// <param name="from">从哪个邮箱发送</param>
        /// <param name="to">发送到</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">内容是否Html</param>
        Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// Sends an email.
        /// 发送Email
        /// </summary>
        /// <param name="from">从哪个邮箱发送</param>
        /// <param name="to">发送到</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="isBodyHtml">内容是否Html</param>
        void Send(string from, string to, string subject, string body, bool isBodyHtml = true);

        /// <summary>
        /// Sends an email.
        /// 发送Email
        /// </summary>
        /// <param name="mail">Mail to be sent 发送邮件</param>
        /// <param name="normalize">
        /// Should normalize email?
        /// If true, it sets sender address/name if it's not set before and makes mail encoding UTF-8. 
        /// 是否标准邮件，true：UTF-8
        /// </param>
        void Send(MailMessage mail, bool normalize = true);

        /// <summary>
        /// Sends an email.
        /// 发送Email
        /// </summary>
        /// <param name="mail">Mail to be sent 发送邮件</param>
        /// <param name="normalize">
        /// Should normalize email?
        /// If true, it sets sender address/name if it's not set before and makes mail encoding UTF-8. 
        /// 是否标准邮件，true：UTF-8
        /// </param>
        Task SendAsync(MailMessage mail, bool normalize = true);
    }
}

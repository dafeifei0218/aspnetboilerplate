namespace Abp.Net.Mail
{
    /// <summary>
    /// Defines configurations used while sending emails.
    /// 邮件发送配置接口
    /// </summary>
    public interface IEmailSenderConfiguration
    {
        /// <summary>
        /// Default from address.
        /// 默认发送地址
        /// </summary>
        string DefaultFromAddress { get; }
        
        /// <summary>
        /// Default display name.
        /// 默认发送显示名
        /// </summary>
        string DefaultFromDisplayName { get; }
    }
}
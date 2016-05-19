namespace Abp.Net.Mail
{
    /// <summary>
    /// Declares names of the settings defined by <see cref="EmailSettingProvider"/>.
    /// 定义邮件设置名称
    /// </summary>
    public static class EmailSettingNames
    {
        /// <summary>
        /// Abp.Net.Mail.DefaultFromAddress
        /// 默认发送邮件地址
        /// </summary>
        public const string DefaultFromAddress = "Abp.Net.Mail.DefaultFromAddress";

        /// <summary>
        /// Abp.Net.Mail.DefaultFromDisplayName
        /// 默认发送邮件显示名称
        /// </summary>
        public const string DefaultFromDisplayName = "Abp.Net.Mail.DefaultFromDisplayName";

        /// <summary>
        /// SMTP related email settings.
        /// SMTP相关电子邮件设置
        /// </summary>
        /// 封装SMTP设置的信息。也就是说定义了一些常量用作Setting的Name。
        /// 比如Host就是“Abp.Net.Mail.Smtp.Host”，所以在web.config就是配置一项key是“Abp.Net.Mail.Smtp.Host”的配置项。
        public static class Smtp
        {
            /// <summary>
            /// Abp.Net.Mail.Smtp.Host
            /// SMTP的主机名/IP.
            /// </summary>
            public const string Host = "Abp.Net.Mail.Smtp.Host";

            /// <summary>
            /// Abp.Net.Mail.Smtp.Port
            /// SMTP端口
            /// </summary>
            public const string Port = "Abp.Net.Mail.Smtp.Port";

            /// <summary>
            /// Abp.Net.Mail.Smtp.UserName
            /// SMTP服务器登录名
            /// </summary>
            public const string UserName = "Abp.Net.Mail.Smtp.UserName";

            /// <summary>
            /// Abp.Net.Mail.Smtp.Password
            /// SMTP服务器密码
            /// </summary>
            public const string Password = "Abp.Net.Mail.Smtp.Password";

            /// <summary>
            /// Abp.Net.Mail.Smtp.Domain
            /// SMTP服务器登录域名
            /// </summary>
            public const string Domain = "Abp.Net.Mail.Smtp.Domain";

            /// <summary>
            /// Abp.Net.Mail.Smtp.EnableSsl
            /// 是否启用SSL安全套接字层加密链接
            /// </summary>
            public const string EnableSsl = "Abp.Net.Mail.Smtp.EnableSsl";

            /// <summary>
            /// Abp.Net.Mail.Smtp.UseDefaultCredentials
            /// 是否使用默认凭据
            /// </summary>
            public const string UseDefaultCredentials = "Abp.Net.Mail.Smtp.UseDefaultCredentials";
        }
    }
}
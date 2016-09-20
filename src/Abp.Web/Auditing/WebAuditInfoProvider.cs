using System;
using System.Net;
using System.Net.Sockets;
using System.Web;
using Abp.Dependency;
using Castle.Core.Logging;

namespace Abp.Auditing
{
    /// <summary>
    /// Implements <see cref="IAuditInfoProvider"/> to fill web specific audit informations.
    /// Web审计信息提供者，
    /// 实现<see cref="IAuditInfoProvider"/>审计信息提供者接口，填充网站特定的审计信息。
    /// </summary>
    /// <remarks>
    /// 这个IAuditInfoProvider对象就是上面所说的上层的IAuditInfoProvider实现。
    /// 这个类就是在Abp.Web模块中实现的。
    /// （ 注意：整个项目中除了NullAuditInfoProvider只能有一个自定义的IAuditInfoProvider实现。
    /// 也就是说实际项目中无法直接创建自定义的IAuditInfoProvider，因为Abp.Web模块中已经有一个了。）
    /// </remarks>
    public class WebAuditInfoProvider : IAuditInfoProvider, ITransientDependency
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Http上下文
        /// </summary>
        private readonly HttpContext _httpContext;

        /// <summary>
        /// Creates a new <see cref="WebAuditInfoProvider"/>.
        /// 创建一个新的<see cref="WebAuditInfoProvider"/>。
        /// </summary>
        public WebAuditInfoProvider()
        {
            _httpContext = HttpContext.Current;
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditInfo">审计信息</param>
        public void Fill(AuditInfo auditInfo)
        {
            var httpContext = HttpContext.Current ?? _httpContext;
            if (httpContext == null)
            {
                return;
            }

            try
            {
                auditInfo.BrowserInfo = GetBrowserInfo(httpContext);
                auditInfo.ClientIpAddress = GetClientIpAddress(httpContext);
                auditInfo.ClientName = GetComputerName(httpContext);
            }
            catch (Exception ex)
            {
                Logger.Warn("Could not obtain web parameters for audit info.");
                Logger.Warn(ex.ToString(), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static string GetBrowserInfo(HttpContext httpContext)
        {
            return httpContext.Request.Browser.Browser + " / " +
                   httpContext.Request.Browser.Version + " / " +
                   httpContext.Request.Browser.Platform;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static string GetClientIpAddress(HttpContext httpContext)
        {
            var clientIp = httpContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                           httpContext.Request.ServerVariables["REMOTE_ADDR"];

            foreach (var hostAddress in Dns.GetHostAddresses(clientIp))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }

            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return hostAddress.ToString();
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static string GetComputerName(HttpContext httpContext)
        {
            if (!httpContext.Request.IsLocal)
            {
                return null;
            }

            try
            {
                var clientIp = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ??
                               HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                return Dns.GetHostEntry(IPAddress.Parse(clientIp)).HostName;
            }
            catch
            {
                return null;
            }
        }
    }
}

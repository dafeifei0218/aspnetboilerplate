using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Abp.Configuration.Startup;
using Abp.MultiTenancy;
using Abp.Runtime.Security;

namespace Abp.Runtime.Session
{
    /// <summary>
    /// Implements <see cref="IAbpSession"/> to get session properties from claims of <see cref="Thread.CurrentPrincipal"/>.
    /// 声明Abp会话
    /// </summary>
    public class ClaimsAbpSession : IAbpSession
    {
        private const int DefaultTenantId = 1;

        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual long? UserId
        {
            get
            {
                var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
                if (claimsPrincipal == null)
                {
                    return null;
                }

                var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;
                if (claimsIdentity == null)
                {
                    return null;
                }

                var userIdClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                {
                    return null;
                }

                long userId;
                if (!long.TryParse(userIdClaim.Value, out userId))
                {
                    return null;
                }

                return userId;
            }
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public virtual int? TenantId
        {
            get
            {
                //如果未启用
                if (!_multiTenancy.IsEnabled)
                {
                    return DefaultTenantId;
                }

                var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
                if (claimsPrincipal == null)
                {
                    return null;
                }

                var tenantIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.TenantId);
                if (tenantIdClaim == null || string.IsNullOrEmpty(tenantIdClaim.Value))
                {
                    return null;
                }

                return Convert.ToInt32(tenantIdClaim.Value);
            }
        }

        /// <summary>
        /// 多租户双方
        /// </summary>
        public virtual MultiTenancySides MultiTenancySide
        {
            get
            {
                return _multiTenancy.IsEnabled && !TenantId.HasValue
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant;
            }
        }

        /// <summary>
        /// 模拟用户Id
        /// </summary>
        public virtual long? ImpersonatorUserId
        {
            get
            {
                var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
                if (claimsPrincipal == null)
                {
                    return null;
                }

                var impersonatorUserIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorUserId);
                if (impersonatorUserIdClaim == null || string.IsNullOrEmpty(impersonatorUserIdClaim.Value))
                {
                    return null;
                }

                return Convert.ToInt64(impersonatorUserIdClaim.Value);
            }
        }

        /// <summary>
        /// 模拟租户Id
        /// </summary>
        public virtual int? ImpersonatorTenantId
        {
            get
            {
                if (!_multiTenancy.IsEnabled)
                {
                    return DefaultTenantId;
                }

                var claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
                if (claimsPrincipal == null)
                {
                    return null;
                }

                var impersonatorTenantIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == AbpClaimTypes.ImpersonatorTenantId);
                if (impersonatorTenantIdClaim == null || string.IsNullOrEmpty(impersonatorTenantIdClaim.Value))
                {
                    return null;
                }

                return Convert.ToInt32(impersonatorTenantIdClaim.Value);
            }
        }

        private readonly IMultiTenancyConfig _multiTenancy;

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="multiTenancy">多租户配置</param>
        public ClaimsAbpSession(IMultiTenancyConfig multiTenancy)
        {
            _multiTenancy = multiTenancy;
        }
    }
}
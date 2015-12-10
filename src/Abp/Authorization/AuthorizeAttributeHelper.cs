using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Runtime.Session;
using Abp.Threading;

namespace Abp.Authorization
{
    /// <summary>
    /// 授权自定义属性帮助类
    /// </summary>
    internal class AuthorizeAttributeHelper : IAuthorizeAttributeHelper, ITransientDependency
    {
        /// <summary>
        /// ABP会话
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// 权限检查
        /// </summary>
        public IPermissionChecker PermissionChecker { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuthorizeAttributeHelper()
        {
            AbpSession = NullAbpSession.Instance;
            PermissionChecker = NullPermissionChecker.Instance;
        }

        /// <summary>
        /// 授权-异步
        /// </summary>
        /// <param name="authorizeAttributes">授权自定义属性集合</param>
        /// <returns></returns>
        public async Task AuthorizeAsync(IEnumerable<IAbpAuthorizeAttribute> authorizeAttributes)
        {
            if (!AbpSession.UserId.HasValue)
            {
                throw new AbpAuthorizationException("No user logged in!");
            }

            foreach (var authorizeAttribute in authorizeAttributes)
            {
                await PermissionChecker.AuthorizeAsync(authorizeAttribute.RequireAllPermissions, authorizeAttribute.Permissions);
            }
        }
        
        /// <summary>
        /// 授权-异步
        /// </summary>
        /// <param name="authorizeAttribute">授权自定义属性</param>
        /// <returns></returns>
        public async Task AuthorizeAsync(IAbpAuthorizeAttribute authorizeAttribute)
        {
            await AuthorizeAsync(new[] { authorizeAttribute });
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="authorizeAttributes">授权自定义属性集合</param>
        public void Authorize(IEnumerable<IAbpAuthorizeAttribute> authorizeAttributes)
        {
            AsyncHelper.RunSync(() => AuthorizeAsync(authorizeAttributes));
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="authorizeAttribute">授权自定义属性</param>
        public void Authorize(IAbpAuthorizeAttribute authorizeAttribute)
        {
            Authorize(new[] { authorizeAttribute });
        }
    }
}
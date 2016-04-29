using System.Web.Mvc;
using Abp.Authorization;
using Abp.Dependency;
using Abp.Logging;

namespace Abp.Web.Mvc.Authorization
{
    /// <summary>
    /// This attribute is used on an action of an MVC <see cref="Controller"/>
    /// to make that action usable only by authorized users. 
    /// Abp Mvc授权自定义属性
    /// </summary>
    public class AbpMvcAuthorizeAttribute : AuthorizeAttribute, IAbpAuthorizeAttribute
    {
        /// <summary>
        /// 权限数组
        /// </summary>
        /// <inheritdoc/>
        public string[] Permissions { get; set; }

        /// <summary>
        /// 要求所有权限
        /// </summary>
        /// <inheritdoc/>
        public bool RequireAllPermissions { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="AbpMvcAuthorizeAttribute"/> class.
        /// 创建一个新的实例<see cref="AbpMvcAuthorizeAttribute"/>类
        /// </summary>
        /// <param name="permissions">A list of permissions to authorize 一个授权权限列表</param>
        public AbpMvcAuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }

        /// <summary>
        /// 授权代码
        /// </summary>
        /// <param name="httpContext">Http上下文</param>
        /// <inheritdoc/>
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext))
            {
                return false;
            }

            try
            {
                using (var authorizationAttributeHelper = IocManager.Instance.ResolveAsDisposable<IAuthorizeAttributeHelper>())
                {
                    authorizationAttributeHelper.Object.Authorize(this);
                }

                return true;
            }
            catch (AbpAuthorizationException ex)
            {
                LogHelper.Logger.Warn(ex.ToString(), ex);
                return false;
            }
        }
    }
}

using System;
using Abp.Application.Services;

namespace Abp.Authorization
{
    /// <summary>
    /// This attribute is used on a method of an Application Service (A class that implements <see cref="IApplicationService"/>)
    /// to make that method usable only by authorized users.
    /// ABP授权自定义属性，这个属性用在应用服务成的方法上，使得只有授权的用户可用的方法。
    /// </summary>
    public class AbpAuthorizeAttribute : Attribute, IAbpAuthorizeAttribute
    {
        /// <summary>
        /// A list of permissions to authorize.
        /// 权限，授权的权限列表
        /// </summary>
        public string[] Permissions { get; private set; }

        /// <summary>
        /// If this property is set to true, all of the <see cref="Permissions"/> must be granted.
        /// If it's false, at least one of the <see cref="Permissions"/> must be granted.
        /// Default: false.
        /// 是否全部权限必须授权
        /// 如果为true：所有权限Permissions必须授权。
        /// 如果为false：至少有一个权限Permissions必须授权
        /// 默认：false
        /// </summary>
        public bool RequireAllPermissions { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="AbpAuthorizeAttribute"/> class.
        /// 创建一个AbpAuthorizeAttribute类
        /// </summary>
        /// <param name="permissions">A list of permissions to authorize 授权的权限列表</param>
        public AbpAuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }
    }
}

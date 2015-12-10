namespace Abp.Authorization
{
    /// <summary>
    /// Defines standard interface for authorization attributes.
    /// 授权自定义属性接口
    /// </summary>
    public interface IAbpAuthorizeAttribute
    {
        /// <summary>
        /// A list of permissions to authorize.
        /// 权限，授权的权限列表
        /// </summary>
        string[] Permissions { get; }

        /// <summary>
        /// If this property is set to true, all of the <see cref="Permissions"/> must be granted.
        /// If it's false, at least one of the <see cref="Permissions"/> must be granted.
        /// Default: false.
        /// 是否全部权限必须授权
        /// 如果为true：所有权限Permissions必须授权。
        /// 如果为false：至少有一个权限Permissions必须授权
        /// 默认：false
        /// </summary>
        bool RequireAllPermissions { get; set; }
    }
}
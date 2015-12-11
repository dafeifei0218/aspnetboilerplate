using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Abp.Application.Features;
using Abp.Collections.Extensions;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.MultiTenancy;
using Abp.Runtime.Session;

namespace Abp.Authorization
{
    /// <summary>
    /// Permission manager.
    /// 权限管理类
    /// </summary>
    internal class PermissionManager : PermissionDefinitionContextBase, IPermissionManager, ISingletonDependency
    {
        /// <summary>
        /// ABP会话
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        private readonly IIocManager _iocManager;
        private readonly IAuthorizationConfiguration _authorizationConfiguration;
        private readonly FeatureDependencyContext _featureDependencyContext;

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        public PermissionManager(
            IIocManager iocManager, 
            IAuthorizationConfiguration authorizationConfiguration,
            FeatureDependencyContext featureDependencyContext
            )
        {
            _iocManager = iocManager;
            _authorizationConfiguration = authorizationConfiguration;
            _featureDependencyContext = featureDependencyContext;

            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            foreach (var providerType in _authorizationConfiguration.Providers)
            {
                CreateAuthorizationProvider(providerType).SetPermissions(this);
            }

            Permissions.AddAllPermissions();
        }

        /// <summary>
        /// 根据权限名获取权限
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <returns></returns>
        public Permission GetPermission(string name)
        {
            var permission = Permissions.GetOrDefault(name);
            if (permission == null)
            {
                throw new AbpException("There is no permission with name: " + name);
            }

            return permission;
        }

        /// <summary>
        /// 获取全部权限
        /// </summary>
        /// <param name="tenancyFilter">是否过滤租户信息</param>
        /// <returns></returns>
        public IReadOnlyList<Permission> GetAllPermissions(bool tenancyFilter = true)
        {
            return Permissions.Values
                .WhereIf(tenancyFilter, p => p.MultiTenancySides.HasFlag(AbpSession.MultiTenancySide))
                .Where(p =>
                    p.FeatureDependency == null ||
                    AbpSession.MultiTenancySide == MultiTenancySides.Host ||
                    p.FeatureDependency.IsSatisfied(_featureDependencyContext)
                ).ToImmutableList();
        }

        /// <summary>
        /// 获取全部权限
        /// </summary>
        /// <param name="multiTenancySides">多租户双方</param>
        /// <returns></returns>
        public IReadOnlyList<Permission> GetAllPermissions(MultiTenancySides multiTenancySides)
        {
            return Permissions.Values
                .Where(p => p.MultiTenancySides.HasFlag(multiTenancySides))
                .Where(p =>
                    p.FeatureDependency == null ||
                    (p.MultiTenancySides.HasFlag(MultiTenancySides.Host) && multiTenancySides.HasFlag(MultiTenancySides.Host)) ||
                    p.FeatureDependency.IsSatisfied(_featureDependencyContext)
                ).ToImmutableList();
        }
        
        /// <summary>
        /// 创建授权提供者
        /// </summary>
        /// <param name="providerType">提供者类型</param>
        /// <returns></returns>
        private AuthorizationProvider CreateAuthorizationProvider(Type providerType)
        {
            if (!_iocManager.IsRegistered(providerType))
            {
                _iocManager.Register(providerType);
            }

            return (AuthorizationProvider)_iocManager.Resolve(providerType);
        }
    }
}
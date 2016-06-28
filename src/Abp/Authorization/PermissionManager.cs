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
    /// <remarks>
    /// 在ABP这是一个单例实例，继承了PermissionDefinitionContextBase类，实现了IPermissionManager的四个方法。
    /// PermissionManager在Initialize方法中会实例化系统的Permissio并存入PermissionDictionary中。
    /// 具体是通过调用AuthorizationProvider的SetPermissions的方法实现的。
    /// 这边的做法和FeatureManager通过FeatureProvider初始化FeatureDictionary一致，也和NavigationManager通过NavigationProvider初始化menus一致的。
    /// </remarks>
    internal class PermissionManager : PermissionDefinitionContextBase, IPermissionManager, ISingletonDependency
    {
        /// <summary>
        /// ABP会话
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// IOC管理
        /// </summary>
        private readonly IIocManager _iocManager;
        /// <summary>
        /// 授权配置
        /// </summary>
        private readonly IAuthorizationConfiguration _authorizationConfiguration;
        //private readonly FeatureDependencyContext _featureDependencyContext;

        ///// <summary>
        ///// Constructor.
        ///// 构造函数
        ///// </summary>
        //public PermissionManager(
        //    IIocManager iocManager,
        //    IAuthorizationConfiguration authorizationConfiguration,
        //    FeatureDependencyContext featureDependencyContext)
        //{
        //    _iocManager = iocManager;
        //    _authorizationConfiguration = authorizationConfiguration;
        //    _featureDependencyContext = featureDependencyContext;

        //    AbpSession = NullAbpSession.Instance;
        //}

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        public PermissionManager(
            IIocManager iocManager,
            IAuthorizationConfiguration authorizationConfiguration)
        {
            _iocManager = iocManager;
            _authorizationConfiguration = authorizationConfiguration;

            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <remarks>
        /// PermissionManager的Initialize方法 是在AbpKernelModule的PostInitialize的方法中被调用的。
        /// </remarks>
        public void Initialize()
        {
            foreach (var providerType in _authorizationConfiguration.Providers)
            {
                //CreateAuthorizationProvider(providerType).SetPermissions(this);

                _iocManager.RegisterIfNot(providerType, DependencyLifeStyle.Transient);
                using (var provider = _iocManager.ResolveAsDisposable<AuthorizationProvider>(providerType))
                {
                    provider.Object.SetPermissions(this);
                }
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

        ///// <summary>
        ///// 获取全部权限
        ///// </summary>
        ///// <param name="tenancyFilter">是否过滤租户信息</param>
        ///// <returns></returns>
        //public IReadOnlyList<Permission> GetAllPermissions(bool tenancyFilter = true)
        //{
            //return Permissions.Values
            //    .WhereIf(tenancyFilter, p => p.MultiTenancySides.HasFlag(AbpSession.MultiTenancySide))
            //    .Where(p =>
            //        p.FeatureDependency == null ||
            //        AbpSession.MultiTenancySide == MultiTenancySides.Host ||
            //        p.FeatureDependency.IsSatisfied(_featureDependencyContext)
            //    ).ToImmutableList();
        //}
        
        public IReadOnlyList<Permission> GetAllPermissions(bool tenancyFilter = true)
        {
            using (var featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext>())
            {
                var featureDependencyContextObject = featureDependencyContext.Object;
                return Permissions.Values
                    .WhereIf(tenancyFilter, p => p.MultiTenancySides.HasFlag(AbpSession.MultiTenancySide))
                    .Where(p =>
                        p.FeatureDependency == null ||
                        AbpSession.MultiTenancySide == MultiTenancySides.Host ||
                        p.FeatureDependency.IsSatisfied(featureDependencyContextObject)
                    ).ToImmutableList();
            }
        }

        ///// <summary>
        ///// 获取全部权限
        ///// </summary>
        ///// <param name="multiTenancySides">多租户双方</param>
        ///// <returns></returns>
        //public IReadOnlyList<Permission> GetAllPermissions(MultiTenancySides multiTenancySides)
        //{
            //return Permissions.Values
            //    .Where(p => p.MultiTenancySides.HasFlag(multiTenancySides))
            //    .Where(p =>
            //        p.FeatureDependency == null ||
            //        (p.MultiTenancySides.HasFlag(MultiTenancySides.Host) && multiTenancySides.HasFlag(MultiTenancySides.Host)) ||
            //        p.FeatureDependency.IsSatisfied(_featureDependencyContext)
            //    ).ToImmutableList();
        //}

        public IReadOnlyList<Permission> GetAllPermissions(MultiTenancySides multiTenancySides)
        {
            using (var featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext>())
            {
                var featureDependencyContextObject = featureDependencyContext.Object;
                return Permissions.Values
                    .Where(p => p.MultiTenancySides.HasFlag(multiTenancySides))
                    .Where(p =>
                        p.FeatureDependency == null ||
                        AbpSession.MultiTenancySide == MultiTenancySides.Host ||
                        (p.MultiTenancySides.HasFlag(MultiTenancySides.Host) &&
                         multiTenancySides.HasFlag(MultiTenancySides.Host)) ||
                        p.FeatureDependency.IsSatisfied(featureDependencyContextObject)
                    ).ToImmutableList();
            }
        }
        
        /// <summary>
        /// 创建授权提供者
        /// </summary>
        /// <param name="providerType">提供者类型</param>
        /// <returns></returns>
        private AuthorizationProvider CreateAuthorizationProvider(Type providerType)
        {
            //如果providerType未注册，则注册该providerType
            if (!_iocManager.IsRegistered(providerType))
            {
                _iocManager.Register(providerType);
            }

            return (AuthorizationProvider)_iocManager.Resolve(providerType);
        }
    }
}
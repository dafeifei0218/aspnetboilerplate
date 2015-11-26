﻿using System;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Authorization;
using Abp.Runtime.Session;

namespace Abp.Application.Services
{
    /// <summary>
    /// This class can be used as a base class for application services. 
    /// 应用服务抽象类，这个类可以用作应用程序服务的基类。
    /// </summary>
    public abstract class ApplicationService : AbpServiceBase, IApplicationService
    {
        /// <summary>
        /// Gets current session information.
        /// 获取当前Session信息
        /// </summary>
        public IAbpSession AbpSession { get; set; }
        
        /// <summary>
        /// Reference to the permission manager.
        /// 权限管理器
        /// </summary>
        public IPermissionManager PermissionManager { protected get; set; }

        /// <summary>
        /// Reference to the permission checker.
        /// 权限检查
        /// </summary>
        public IPermissionChecker PermissionChecker { protected get; set; }

        /// <summary>
        /// Reference to the feature manager.
        /// 功能管理
        /// </summary>
        public IFeatureManager FeatureManager { protected get; set; }

        /// <summary>
        /// Reference to the permission checker.
        /// 功能检查
        /// </summary>
        public IFeatureChecker FeatureChecker { protected get; set; }

        /// <summary>
        /// Gets current session information.
        /// 获取当前Session信息
        /// </summary>
        [Obsolete("Use AbpSession property instead. CurrentSetting will be removed in future releases.")]
        protected IAbpSession CurrentSession { get { return AbpSession; } }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        protected ApplicationService()
        {
            AbpSession = NullAbpSession.Instance;
            PermissionChecker = NullPermissionChecker.Instance;
        }

        /// <summary>
        /// Checks if current user is granted for a permission.
        /// 检查当前用户是否赋予权限。
        /// </summary>
        /// <param name="permissionName">Name of the permission 权限名</param>
        protected virtual Task<bool> IsGrantedAsync(string permissionName)
        {
            return PermissionChecker.IsGrantedAsync(permissionName);
        }

        /// <summary>
        /// Checks if current user is granted for a permission.
        /// 检查当前用户是否赋予权限。
        /// </summary>
        /// <param name="permissionName">Name of the permission 权限名</param>
        protected virtual bool IsGranted(string permissionName)
        {
            return PermissionChecker.IsGranted(permissionName);
        }

        /// <summary>
        /// Checks if given feature is enabled for current tenant.
        /// 检查给定的功能是否为当前租户启用。
        /// </summary>
        /// <param name="featureName">Name of the feature 功能名称</param>
        /// <returns></returns>
        protected virtual Task<bool> IsEnabledAsync(string featureName)
        {
            return FeatureChecker.IsEnabledAsync(featureName);
        }

        /// <summary>
        /// Checks if given feature is enabled for current tenant.
        /// 检查给定的功能是否为当前租户启用。
        /// </summary>
        /// <param name="featureName">Name of the feature  功能名称</param>
        /// <returns></returns>
        protected virtual bool IsEnabled(string featureName)
        {
            return FeatureChecker.IsEnabled(featureName);
        }
    }
}

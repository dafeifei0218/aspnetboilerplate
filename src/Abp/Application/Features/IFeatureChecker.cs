﻿using System.Threading.Tasks;
using Abp.Runtime.Session;

namespace Abp.Application.Features
{
    /// <summary>
    /// This interface should be used to get value of
    /// 功能检查接口
    /// </summary> 
    /// <remarks>
    /// 提供检查特定的Feture对于特定的Tenant是否可用。
    /// 首先FeatureChecker 根据Feature Name通过FeatureManager获取Featue,然后通过从IFeatureValueStore对象根据Feature的Name和tenantId获取Feature的value值。
    /// 然后判断value是否为“true”。
    /// </remarks>
    public interface IFeatureChecker
    {
        /// <summary>
        /// Gets value of a feature by it's name.
        /// This is a shortcut for <see cref="GetValueAsync(int, string)"/> that uses <see cref="IAbpSession.TenantId"/> as tenantId.
        /// So, this method should be used only if TenantId can be obtained from the session.
        /// 根据名称获取功能点
        /// </summary>
        /// <param name="name">Unique feature name 功能名称</param>
        /// <returns>Feature's current value 功能值</returns>
        Task<string> GetValueAsync(string name);

        /// <summary>
        /// Gets value of a feature for a tenant by the feature name.
        /// 根据租户Id和租户名称，获取一个功能名称
        /// </summary>
        /// <param name="tenantId">Tenant's Id 租户Id</param>
        /// <param name="name">Unique feature name 功能名称</param>
        /// <returns>Feature's current value 功能值</returns>
        Task<string> GetValueAsync(int tenantId, string name);
    }
}
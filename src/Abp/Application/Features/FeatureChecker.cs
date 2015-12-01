﻿using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Runtime.Session;

namespace Abp.Application.Features
{
    /// <summary>
    /// Default implementation for <see cref="IFeatureChecker"/>.
    /// 功能检查
    /// </summary>
    public class FeatureChecker : IFeatureChecker, ITransientDependency
    {
        /// <summary>
        /// Reference to current session.
        /// 当前Session会话
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// Reference to the store used to get feature values.
        /// 获取功能值的储存
        /// </summary>
        public IFeatureValueStore FeatureValueStore { get; set; }

        private readonly IFeatureManager _featureManager;

        /// <summary>
        /// Creates a new <see cref="FeatureChecker"/> object.
        /// 构造函数
        /// </summary>
        public FeatureChecker(IFeatureManager featureManager)
        {
            _featureManager = featureManager;

            FeatureValueStore = NullFeatureValueStore.Instance;
            AbpSession = NullAbpSession.Instance;
        }

        /// <inheritdoc/>
        public Task<string> GetValueAsync(string name)
        {
            if (!AbpSession.TenantId.HasValue)
            {
                throw new AbpException("FeatureChecker can not get a feature value by name. TenantId is not set in the IAbpSession!");
            }

            return GetValueAsync(AbpSession.TenantId.Value, name);
        }

        /// <inheritdoc/>
        public async Task<string> GetValueAsync(int tenantId, string name)
        {
            var feature = _featureManager.Get(name);

            var value = await FeatureValueStore.GetValueOrNullAsync(tenantId, feature);
            if (value == null)
            {
                return feature.DefaultValue;
            }

            return value;
        }
    }
}
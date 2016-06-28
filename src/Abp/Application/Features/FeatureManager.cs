using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Abp.Dependency;

namespace Abp.Application.Features
{
    /// <summary>
    /// Implements <see cref="IFeatureManager"/>.
    /// 功能管理类。
    /// </summary>
    /// <remarks>
    /// 通过调用继承自FeatureDefinitionContextBase中的方法来实现IFeatureManager中定义的方法。
    /// 这个FeatureManager起到了一个类似适配器的作用，把IFeatureDefinitionContext适配成IFeatureManager。
    /// FeatureManager的另一个作用是初始化FeatureDictionary（其Features属性）。
    /// </remarks>
    internal class FeatureManager : FeatureDefinitionContextBase, IFeatureManager, ISingletonDependency
    {
        private readonly IIocManager _iocManager;
        private readonly IFeatureConfiguration _featureConfiguration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocManager">IOC控制反转管理类</param>
        /// <param name="featureConfiguration">功能配置</param>
        public FeatureManager(IIocManager iocManager, IFeatureConfiguration featureConfiguration)
        {
            _iocManager = iocManager;
            _featureConfiguration = featureConfiguration;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            foreach (var providerType in _featureConfiguration.Providers)
            {
                CreateProvider(providerType).SetFeatures(this);
            }

            Features.AddAllFeatures();
        }

        /// <summary>
        /// 获取指定名称的功能。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Feature Get(string name)
        {
            var feature = GetOrNull(name);
            if (feature == null)
            {
                throw new AbpException("There is no feature with name: " + name);
            }

            return feature;
        }

        /// <summary>
        /// 获取全部功能。
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<Feature> GetAll()
        {
            return Features.Values.ToImmutableList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerType"></param>
        /// <returns></returns>
        private FeatureProvider CreateProvider(Type providerType)
        {
            _iocManager.RegisterIfNot(providerType);
            return (FeatureProvider)_iocManager.Resolve(providerType);
        }
    }
}

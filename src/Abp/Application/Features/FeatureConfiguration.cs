using Abp.Collections;

namespace Abp.Application.Features
{
    /// <summary>
    /// 功能配置
    /// </summary>
    internal class FeatureConfiguration : IFeatureConfiguration
    {
        public ITypeList<FeatureProvider> Providers { get; private set; }

        public FeatureConfiguration()
        {
            Providers = new TypeList<FeatureProvider>();
        }
    }
}
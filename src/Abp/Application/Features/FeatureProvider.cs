namespace Abp.Application.Features
{
    /// <summary>
    /// This class should be inherited in order to provide <see cref="Feature"/>s.
    /// 功能提供者
    /// </summary>
    public abstract class FeatureProvider
    {
        /// <summary>
        /// Used to set <see cref="Feature"/>s.
        /// 设置功能
        /// </summary>
        /// <param name="context">Feature definition context 功能定义上下文</param>
        public abstract void SetFeatures(IFeatureDefinitionContext context);
    }
}
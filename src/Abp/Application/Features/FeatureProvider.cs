namespace Abp.Application.Features
{
    /// <summary>
    /// This class should be inherited in order to provide <see cref="Feature"/>s.
    /// 功能提供者
    /// </summary>
    /// <remarks>
    /// 抽象基类，用于向IFeatureDefinitionContext对象（FeatureManager）中添加Feature。
    /// Abp框架只提供了抽象类，SampleFeatureProvider下面代码是一个简单的示例。
    /// 实际项目中可以创建自定义FeatureProvider来从数据库中读取Feature来填充到FeatureManager对象中。
    /// </remarks>
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
namespace Abp.Application.Features
{
    /// <summary>
    /// This class should be inherited in order to provide <see cref="Feature"/>s.
    /// �����ṩ��
    /// </summary>
    public abstract class FeatureProvider
    {
        /// <summary>
        /// Used to set <see cref="Feature"/>s.
        /// ���ù���
        /// </summary>
        /// <param name="context">Feature definition context ���ܶ���������</param>
        public abstract void SetFeatures(IFeatureDefinitionContext context);
    }
}
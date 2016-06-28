namespace Abp.Application.Features
{
    /// <summary>
    /// This class should be inherited in order to provide <see cref="Feature"/>s.
    /// �����ṩ��
    /// </summary>
    /// <remarks>
    /// ������࣬������IFeatureDefinitionContext����FeatureManager�������Feature��
    /// Abp���ֻ�ṩ�˳����࣬SampleFeatureProvider���������һ���򵥵�ʾ����
    /// ʵ����Ŀ�п��Դ����Զ���FeatureProvider�������ݿ��ж�ȡFeature����䵽FeatureManager�����С�
    /// </remarks>
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
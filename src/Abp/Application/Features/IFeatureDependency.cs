using System.Threading.Tasks;

namespace Abp.Application.Features
{
    /// <summary>
    /// Defines a feature dependency.
    /// ��������
    /// </summary>
    /// <remarks>
    /// IFeatureDependency/SimpleFeatureDependency��
    /// ���ĳ���Ҫ�Ƚ���Feature��飬���Լ���һ��IFeatureDependency���ԡ�
    /// IFeatureDependency����ͨ������IFeatureChecker������������ļ�顣
    /// �����������ɲ鿴MenuItemDefinition��UserNavigationManager���÷���
    /// </remarks>
    public interface IFeatureDependency
    {
        /// <summary>
        /// Checks depended features and returns true if dependencies are satisfied.
        /// ���������ϵ����鹦�ܺͷ����Ƿ�Ϊtrue
        /// </summary>
        Task<bool> IsSatisfiedAsync(IFeatureDependencyContext context);
    }
}
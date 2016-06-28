using System.Threading.Tasks;

namespace Abp.Application.Features
{
    /// <summary>
    /// Defines a store to get feature values.
    /// ����ֵ�洢
    /// </summary>
    /// <remarks>
    /// �ӿڶ����˻�ȡFeatureֵ�ķ�����
    /// FeatureValueStore��Ҫ����ģ����ʵ�֡�
    /// ��Ϊfeature�����Ǵ�������ݿ��еġ�
    /// ����Abp�ײ����ǲ�����������ݿ����������߼�.�ýӿ��Ѿ���ȫʵ������ module-zero��Ŀ�С�
    /// ���û��ʵ�ָýӿڣ���ôĬ�ϻ�ʹ��NullFeatureValueStore�����еĹ��ܷ���null����ʱʹ��Ĭ�ϵĹ���ֵ����
    /// </remarks>
    public interface IFeatureValueStore
    {
        /// <summary>
        /// Gets the feature value or null.
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="tenantId">The tenant id. �⻧Id</param>
        /// <param name="feature">The feature. ����</param>
        Task<string> GetValueOrNullAsync(int tenantId, Feature feature);
    }
}
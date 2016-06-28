using Abp.Dependency;

namespace Abp.Application.Features
{
    /// <summary>
    /// Used in <see cref="IFeatureDependency.IsSatisfiedAsync"/> method.
    /// ��������������
    /// </summary>
    /// <remarks>
    /// ������������װ��IFeatureChecker �� IResolver���󡣱����ڷ������βΡ�
    /// </remarks>
    public interface IFeatureDependencyContext
    {
        /// <summary>
        /// Tenant id which required the feature.
        /// Null for current tenant.
        /// �⻧Id
        /// ���ܵ��⻧Id��
        /// ��ǰ�⻧��Ϊ�ա�
        /// </summary>
        int? TenantId { get; }

        /// <summary>
        /// Gets the <see cref="IIocResolver"/>.
        /// ��ȡIOC���Ʒ�ת������
        /// </summary>
        /// <value>
        /// The ioc resolver.
        /// </value>
        IIocResolver IocResolver { get; }

        /// <summary>
        /// Gets the <see cref="IFeatureChecker"/>.
        /// ��ȡ���ܼ����
        /// </summary>
        /// <value>
        /// The feature checker.
        /// </value>
        IFeatureChecker FeatureChecker { get; }
    }
}
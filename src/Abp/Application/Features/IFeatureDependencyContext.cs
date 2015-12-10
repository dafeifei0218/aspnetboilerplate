using Abp.Dependency;

namespace Abp.Application.Features
{
    /// <summary>
    /// Used in <see cref="IFeatureDependency.IsSatisfiedAsync"/> method.
    /// ��������������
    /// </summary>
    public interface IFeatureDependencyContext
    {
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
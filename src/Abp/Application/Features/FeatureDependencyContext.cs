using Abp.Dependency;

namespace Abp.Application.Features
{
    /// <summary>
    /// Implementation of <see cref="IFeatureDependencyContext"/>.
    /// ��������������
    /// </summary>
    public class FeatureDependencyContext : IFeatureDependencyContext, ITransientDependency
    {
        /// <summary>
        /// IOC���Ʒ�ת������
        /// </summary>
        /// <inheritdoc/>
        public IIocResolver IocResolver { get; private set; }

        /// <summary>
        /// ���ܼ��
        /// </summary>
        /// <inheritdoc/>
        public IFeatureChecker FeatureChecker { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureDependencyContext"/> class.
        /// ���캯������ʼ����������������
        /// </summary>
        /// <param name="iocResolver">The ioc resolver. IOC���Ʒ�ת������</param>
        /// <param name="featureChecker">The feature checker. ���ܼ��</param>
        public FeatureDependencyContext(IIocResolver iocResolver, IFeatureChecker featureChecker)
        {
            IocResolver = iocResolver;
            FeatureChecker = featureChecker;
        }
    }
}
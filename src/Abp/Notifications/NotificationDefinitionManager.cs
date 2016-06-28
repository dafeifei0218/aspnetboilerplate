using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Dependency;

namespace Abp.Notifications
{
    /// <summary>
    /// Implements <see cref="INotificationDefinitionManager"/>.
    /// ֪ͨ���������
    /// </summary>
    /// <remarks>
    /// ��������ʵ����INotificationDefinitionManager�ӿڡ�
    /// NotificationDefinitionManager��װ��һ��Dictionary<string, NotificationDefinition>�ֵ�������ڴ��NotificationDefinition����Initialize�����������NotificationDefinition�ĳ�ʼ����װ�ء�Initialize������NotificationConfiguration��ȡNotificationProvider��װ��NotificationDefinition������˽�е�IDictionary�����С�
    /// ��ʵ�ֵ��ַ���Feature��Navigation�Լ�Authorization��һ�µ�
    /// </remarks>
    internal class NotificationDefinitionManager : INotificationDefinitionManager, ISingletonDependency
    {
        /// <summary>
        /// ֪ͨ���ýӿ�
        /// </summary>
        private readonly INotificationConfiguration _configuration;
        /// <summary>
        /// IOC���Ʒ�ת������
        /// </summary>
        private readonly IocManager _iocManager;

        /// <summary>
        /// ֪ͨ�����ֵ�
        /// </summary>
        private readonly IDictionary<string, NotificationDefinition> _notificationDefinitions;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="iocManager">Ioc������</param>
        /// <param name="configuration">֪ͨ����</param>
        public NotificationDefinitionManager(
            IocManager iocManager,
            INotificationConfiguration configuration)
        {
            _configuration = configuration;
            _iocManager = iocManager;

            _notificationDefinitions = new Dictionary<string, NotificationDefinition>();
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Initialize()
        {
            var context = new NotificationDefinitionContext(this);

            foreach (var providerType in _configuration.Providers)
            {
                _iocManager.RegisterIfNot(providerType, DependencyLifeStyle.Transient);
                using (var provider = _iocManager.ResolveAsDisposable<NotificationProvider>(providerType))
                {
                    provider.Object.SetNotifications(context);
                }
            }
        }

        /// <summary>
        /// ���֪ͨ����
        /// </summary>
        /// <param name="notificationDefinition">֪ͨ����</param>
        public void Add(NotificationDefinition notificationDefinition)
        {
            if (_notificationDefinitions.ContainsKey(notificationDefinition.Name))
            {
                throw new AbpInitializationException("There is already a notification definition with given name: " + notificationDefinition.Name + ". Notification names must be unique!");
            }

            _notificationDefinitions[notificationDefinition.Name] = notificationDefinition;
        }

        /// <summary>
        /// ��ȡ֪ͨ����
        /// </summary>
        /// <param name="name">֪ͨ����</param>
        /// <returns></returns>
        public NotificationDefinition Get(string name)
        {
            var definition = GetOrNull(name);
            if (definition == null)
            {
                throw new AbpException("There is no notification definition with given name: " + name);
            }

            return definition;
        }

        /// <summary>
        /// ��ȡ֪ͨ���壬���Ϊ�շ���null
        /// </summary>
        /// <param name="name">֪ͨ����</param>
        /// <returns></returns>
        public NotificationDefinition GetOrNull(string name)
        {
            return _notificationDefinitions.GetOrDefault(name);
        }

        /// <summary>
        /// ��ȡȫ��֪ͨ����
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<NotificationDefinition> GetAll()
        {
            return _notificationDefinitions.Values.ToImmutableList();
        }

        /// <summary>
        /// �Ƿ����-�첽
        /// </summary>
        /// <param name="name">֪ͨ����</param>
        /// <param name="tenantId">�⻧Id</param>
        /// <param name="userId">�û�Id</param>
        /// <returns></returns>
        public async Task<bool> IsAvailableAsync(string name, int? tenantId, long userId)
        {
            var notificationDefinition = Get(name);

            if (notificationDefinition.FeatureDependency != null)
            {
                using (var featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext>())
                {
                    featureDependencyContext.Object.TenantId = tenantId;

                    if (!await notificationDefinition.FeatureDependency.IsSatisfiedAsync(featureDependencyContext.Object))
                    {
                        return false;
                    }
                }
            }

            if (notificationDefinition.PermissionDependency != null)
            {
                using (var permissionDependencyContext = _iocManager.ResolveAsDisposable<PermissionDependencyContext>())
                {
                    permissionDependencyContext.Object.UserId = userId;

                    if (!await notificationDefinition.PermissionDependency.IsSatisfiedAsync(permissionDependencyContext.Object))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// ��ȡȫ������֪ͨ����
        /// </summary>
        /// <param name="tenantId">�⻧Id</param>
        /// <param name="userId">�û�Id</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(int? tenantId, long userId)
        {
            var availableDefinitions = new List<NotificationDefinition>();

            using (var permissionDependencyContext = _iocManager.ResolveAsDisposable<PermissionDependencyContext>())
            {
                permissionDependencyContext.Object.UserId = userId;

                using (var featureDependencyContext = _iocManager.ResolveAsDisposable<FeatureDependencyContext>())
                {
                    featureDependencyContext.Object.TenantId = tenantId;

                    foreach (var notificationDefinition in GetAll())
                    {
                        if (notificationDefinition.PermissionDependency != null &&
                            !await notificationDefinition.PermissionDependency.IsSatisfiedAsync(permissionDependencyContext.Object))
                        {
                            continue;
                        }

                        if (tenantId.HasValue &&
                            notificationDefinition.FeatureDependency != null &&
                            !await notificationDefinition.FeatureDependency.IsSatisfiedAsync(featureDependencyContext.Object))
                        {
                            continue;
                        }

                        availableDefinitions.Add(notificationDefinition);
                    }
                }
            }

            return availableDefinitions.ToImmutableList();
        }
    }
}
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
    /// 通知定义管理类
    /// </summary>
    /// <remarks>
    /// 单例对象，实现了INotificationDefinitionManager接口。
    /// NotificationDefinitionManager封装了一个Dictionary<string, NotificationDefinition>字典对象用于存放NotificationDefinition。其Initialize方法完成所有NotificationDefinition的初始化和装载。Initialize方法从NotificationConfiguration读取NotificationProvider以装载NotificationDefinition到他的私有的IDictionary容器中。
    /// 其实现的手法和Feature，Navigation以及Authorization是一致的
    /// </remarks>
    internal class NotificationDefinitionManager : INotificationDefinitionManager, ISingletonDependency
    {
        /// <summary>
        /// 通知配置接口
        /// </summary>
        private readonly INotificationConfiguration _configuration;
        /// <summary>
        /// IOC控制反转管理类
        /// </summary>
        private readonly IocManager _iocManager;

        /// <summary>
        /// 通知定义字典
        /// </summary>
        private readonly IDictionary<string, NotificationDefinition> _notificationDefinitions;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iocManager">Ioc管理类</param>
        /// <param name="configuration">通知配置</param>
        public NotificationDefinitionManager(
            IocManager iocManager,
            INotificationConfiguration configuration)
        {
            _configuration = configuration;
            _iocManager = iocManager;

            _notificationDefinitions = new Dictionary<string, NotificationDefinition>();
        }

        /// <summary>
        /// 初始化
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
        /// 添加通知定义
        /// </summary>
        /// <param name="notificationDefinition">通知定义</param>
        public void Add(NotificationDefinition notificationDefinition)
        {
            if (_notificationDefinitions.ContainsKey(notificationDefinition.Name))
            {
                throw new AbpInitializationException("There is already a notification definition with given name: " + notificationDefinition.Name + ". Notification names must be unique!");
            }

            _notificationDefinitions[notificationDefinition.Name] = notificationDefinition;
        }

        /// <summary>
        /// 获取通知定义
        /// </summary>
        /// <param name="name">通知名称</param>
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
        /// 获取通知定义，如果为空返回null
        /// </summary>
        /// <param name="name">通知名称</param>
        /// <returns></returns>
        public NotificationDefinition GetOrNull(string name)
        {
            return _notificationDefinitions.GetOrDefault(name);
        }

        /// <summary>
        /// 获取全部通知定义
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<NotificationDefinition> GetAll()
        {
            return _notificationDefinitions.Values.ToImmutableList();
        }

        /// <summary>
        /// 是否可用-异步
        /// </summary>
        /// <param name="name">通知名称</param>
        /// <param name="tenantId">租户Id</param>
        /// <param name="userId">用户Id</param>
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
        /// 获取全部可用通知定义
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <param name="userId">用户Id</param>
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
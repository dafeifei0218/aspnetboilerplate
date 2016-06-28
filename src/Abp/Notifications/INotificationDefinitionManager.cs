using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to manage notification definitions.
    /// 通知定义管理接口
    /// </summary>
    /// <remarks>
    /// 该接口定义根据Name返回NotificationDefinition的一些方法
    /// </remarks>
    public interface INotificationDefinitionManager
    {
        /// <summary>
        /// Adds the specified notification definition.
        /// 添加指定的通知定义
        /// </summary>
        /// <param name="notificationDefinition">通知定义</param>
        void Add(NotificationDefinition notificationDefinition);

        /// <summary>
        /// Gets a notification definition by name.
        /// Throws exception if there is no notification definition with given name.
        /// 获取名称的通知定义。
        /// 如果没有给定名称的通知定义，则抛出异常。
        /// </summary>
        /// <param name="name">名称</param>
        NotificationDefinition Get(string name);

        /// <summary>
        /// Gets a notification definition by name.
        /// Returns null if there is no notification definition with given name.
        /// 获取名称的通知定义。
        /// 如果没有给定名称的通知定义，则返回null。
        /// </summary>
        /// <param name="name">名称</param>
        NotificationDefinition GetOrNull(string name);

        /// <summary>
        /// Gets all notification definitions.
        /// 获取全部通知定义
        /// </summary>
        IReadOnlyList<NotificationDefinition> GetAll();

        /// <summary>
        /// Checks if given notification (<see cref="name"/>) is available for given user and given tenant.
        /// 是否可用，如果给定的通知（名称）、给定的用户和给定的租户是否可用的。
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="tenantId">租户Id</param>
        /// <param name="userId">用户Id</param>
        Task<bool> IsAvailableAsync(string name, int? tenantId, long userId);

        /// <summary>
        /// Gets all available notification definitions for given <see cref="tenantId"/> and <see cref="userId"/>.
        /// 给定的租户和用户，获取全部可用异步。
        /// </summary>
        /// <param name="tenantId">Tenant id 租户Id</param>
        /// <param name="userId">User id 用户Id</param>
        Task<IReadOnlyList<NotificationDefinition>> GetAllAvailableAsync(int? tenantId, long userId);
    }
}
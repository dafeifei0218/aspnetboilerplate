using System.Collections.Generic;

namespace Abp.RealTime
{
    /// <summary>
    /// Used to manage online clients those are connected to the application.
    /// 在线客户端管理接口
    /// </summary>
    public interface IOnlineClientManager
    {
        /// <summary>
        /// Adds a client.
        /// 添加客户端
        /// </summary>
        /// <param name="client">The client. 客户端</param>
        void Add(IOnlineClient client);

        /// <summary>
        /// Removes the specified client.
        /// 删除指定的客户端
        /// </summary>
        /// <param name="client">The client. 客户端</param>
        /// <returns>True, if a client is removed. true：如果客户端移除成功</returns>
        bool Remove(IOnlineClient client);

        /// <summary>
        /// Removes a client by connection id.
        /// 根据链接Id删除客户端
        /// </summary>
        /// <param name="connectionId">The connection id. 链接Id</param>
        /// <returns>True, if a client is removed. true：如果客户端移除成功</returns>
        bool Remove(string connectionId);

        /// <summary>
        /// Tries to find a client by connection id.
        /// Returns null if not found.
        /// 根据链接Id获取客户端，如果未找到返回null
        /// </summary>
        /// <param name="connectionId">connection id. 链接Id</param>
        IOnlineClient GetByConnectionIdOrNull(string connectionId);

        /// <summary>
        /// Tries to find a client by userId.
        /// Returns null if not found.
        /// 根据用户Id获取客户端，如果未找到返回null
        /// </summary>
        /// <param name="userId">UserId. 用户Id</param>
        IOnlineClient GetByUserIdOrNull(long userId);

        /// <summary>
        /// Gets all online clients.
        /// 获取全部客户端
        /// </summary>
        IReadOnlyList<IOnlineClient> GetAllClients();
    }
}
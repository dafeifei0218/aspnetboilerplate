using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Dependency;

namespace Abp.RealTime
{
    /// <summary>
    /// Implements <see cref="IOnlineClientManager"/>.
    /// 在线客户端管理类
    /// </summary>
    public class OnlineClientManager : IOnlineClientManager, ISingletonDependency
    {
        /// <summary>
        /// Online clients.
        /// 线程安全的在线客户端字典
        /// </summary>
        private readonly ConcurrentDictionary<string, IOnlineClient> _clients;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlineClientManager"/> class.
        /// 构造函数
        /// </summary>
        public OnlineClientManager()
        {
            _clients = new ConcurrentDictionary<string, IOnlineClient>();
        }

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="client">/// </summary></param>
        public void Add(IOnlineClient client)
        {
            _clients[client.ConnectionId] = client;
        }

        /// <summary>
        /// 删除指定的客户端
        /// </summary>
        /// <param name="client">/// </summary></param>
        /// <returns></returns>
        public bool Remove(IOnlineClient client)
        {
            return Remove(client.ConnectionId);
        }

        /// <summary>
        /// 根据链接Id删除客户端
        /// </summary>
        /// <param name="connectionId">链接Id</param>
        /// <returns></returns>
        public bool Remove(string connectionId)
        {
            IOnlineClient client;
            return _clients.TryRemove(connectionId, out client);
        }

        /// <summary>
        /// 根据链接Id获取客户端，如果未找到返回null
        /// </summary>
        /// <param name="connectionId">链接Id</param>
        /// <returns></returns>
        public IOnlineClient GetByConnectionIdOrNull(string connectionId)
        {
            return _clients.GetOrDefault(connectionId);
        }

        /// <summary>
        /// 根据用户Id获取客户端，如果未找到返回null
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IOnlineClient GetByUserIdOrNull(long userId)
        {
            //TODO: We can create a dictionary for a faster lookup.
            return GetAllClients().FirstOrDefault(c => c.UserId == userId);
        }

        /// <summary>
        /// 获取全部客户端
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<IOnlineClient> GetAllClients()
        {
            return _clients.Values.ToImmutableList();
        }
    }
}
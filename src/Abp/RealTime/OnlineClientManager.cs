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
    /// ���߿ͻ��˹�����
    /// </summary>
    public class OnlineClientManager : IOnlineClientManager, ISingletonDependency
    {
        /// <summary>
        /// Online clients.
        /// �̰߳�ȫ�����߿ͻ����ֵ�
        /// </summary>
        private readonly ConcurrentDictionary<string, IOnlineClient> _clients;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlineClientManager"/> class.
        /// ���캯��
        /// </summary>
        public OnlineClientManager()
        {
            _clients = new ConcurrentDictionary<string, IOnlineClient>();
        }

        /// <summary>
        /// ��ӿͻ���
        /// </summary>
        /// <param name="client">/// </summary></param>
        public void Add(IOnlineClient client)
        {
            _clients[client.ConnectionId] = client;
        }

        /// <summary>
        /// ɾ��ָ���Ŀͻ���
        /// </summary>
        /// <param name="client">/// </summary></param>
        /// <returns></returns>
        public bool Remove(IOnlineClient client)
        {
            return Remove(client.ConnectionId);
        }

        /// <summary>
        /// ��������Idɾ���ͻ���
        /// </summary>
        /// <param name="connectionId">����Id</param>
        /// <returns></returns>
        public bool Remove(string connectionId)
        {
            IOnlineClient client;
            return _clients.TryRemove(connectionId, out client);
        }

        /// <summary>
        /// ��������Id��ȡ�ͻ��ˣ����δ�ҵ�����null
        /// </summary>
        /// <param name="connectionId">����Id</param>
        /// <returns></returns>
        public IOnlineClient GetByConnectionIdOrNull(string connectionId)
        {
            return _clients.GetOrDefault(connectionId);
        }

        /// <summary>
        /// �����û�Id��ȡ�ͻ��ˣ����δ�ҵ�����null
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <returns></returns>
        public IOnlineClient GetByUserIdOrNull(long userId)
        {
            //TODO: We can create a dictionary for a faster lookup.
            return GetAllClients().FirstOrDefault(c => c.UserId == userId);
        }

        /// <summary>
        /// ��ȡȫ���ͻ���
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<IOnlineClient> GetAllClients()
        {
            return _clients.Values.ToImmutableList();
        }
    }
}
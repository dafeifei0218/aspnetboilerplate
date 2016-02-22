using System;
using System.Collections.Generic;
using Abp.Json;
using Abp.Timing;

namespace Abp.RealTime
{
    /// <summary>
    /// Implements <see cref="IOnlineClient"/>.
    /// 在线客户端
    /// </summary>
    [Serializable]
    public class OnlineClient : IOnlineClient
    {
        /// <summary>
        /// Unique connection Id for this client.
        /// 链接Id，此客户端的唯一链接标示
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// IP address of this client.
        /// IP地址，此客户端的IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Tenant Id.
        /// 租户Id
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// User Id.
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Connection establishment time for this client.
        /// 链接时间，此客户端链接建立时间
        /// </summary>
        public DateTime ConnectTime { get; set; }

        /// <summary>
        /// Shortcut to set/get <see cref="Properties"/>.
        /// 获取/设置属性
        /// </summary>
        public object this[string key]
        {
            get { return Properties[key]; }
            set { Properties[key] = value; }
        }

        /// <summary>
        /// Can be used to add custom properties for this client.
        /// 属性字典，用于此客户端添加自定义属性
        /// </summary>
        public Dictionary<string, object> Properties
        {
            get { return _properties; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _properties = value;
            }
        }
        private Dictionary<string, object> _properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlineClient"/> class.
        /// 构造函数，初始化OnlineClient类
        /// </summary>
        public OnlineClient()
        {
            ConnectTime = Clock.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OnlineClient"/> class.
        /// 构造函数，初始化OnlineClient类
        /// </summary>
        /// <param name="connectionId">The connection identifier. 链接Id</param>
        /// <param name="ipAddress">The ip address. IP地址</param>
        /// <param name="tenantId">The tenant identifier. 租户Id</param>
        /// <param name="userId">The user identifier. 用户Id</param>
        public OnlineClient(string connectionId, string ipAddress, int? tenantId, long? userId)
            : this()
        {
            ConnectionId = connectionId;
            IpAddress = ipAddress;
            TenantId = tenantId;
            UserId = userId;

            Properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToJsonString();
        }
    }
}
using System;
using System.Collections.Generic;

namespace Abp.RealTime
{
    /// <summary>
    /// Represents an online client connected to the application.
    /// 在线客户端接口，表示连接到应用程序的联机客户端。
    /// </summary>
    public interface IOnlineClient
    {
        /// <summary>
        /// Unique connection Id for this client.
        /// 链接Id，此客户端的唯一链接标示
        /// </summary>
        string ConnectionId { get; }

        /// <summary>
        /// IP address of this client.
        /// IP地址，此客户端的IP地址
        /// </summary>
        string IpAddress { get; }

        /// <summary>
        /// Tenant Id.
        /// 租户Id
        /// </summary>
        int? TenantId { get; }

        /// <summary>
        /// User Id.
        /// 用户Id
        /// </summary>
        long? UserId { get; }

        /// <summary>
        /// Connection establishment time for this client.
        /// 链接时间，此客户端链接建立时间
        /// </summary>
        DateTime ConnectTime { get; }

        /// <summary>
        /// Shortcut to set/get <see cref="Properties"/>.
        /// 获取/设置属性
        /// </summary>
        object this[string key] { get; set; }

        /// <summary>
        /// Can be used to add custom properties for this client.
        /// 属性字典，用于此客户端添加自定义属性
        /// </summary>
        Dictionary<string, object> Properties { get; }
    }
}
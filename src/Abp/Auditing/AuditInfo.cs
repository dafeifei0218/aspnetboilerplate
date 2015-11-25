using System;

namespace Abp.Auditing
{
    /// <summary>
    /// This informations are collected for an <see cref="AuditedAttribute"/> method.
    /// 审计信息
    /// </summary>
    public class AuditInfo
    {
        /// <summary>
        /// TenantId.
        /// 租户Id
        /// </summary>
        public int? TenantId { get; set; }
        
        /// <summary>
        /// UserId.
        /// 用户Id
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// ImpersonatorUserId.
        /// 模拟用户Id
        /// </summary>
        public long? ImpersonatorUserId { get; set; }

        /// <summary>
        /// ImpersonatorTenantId.
        /// 模拟租户Id
        /// </summary>
        public int? ImpersonatorTenantId { get; set; }

        /// <summary>
        /// Service (class/interface) name.
        /// 服务器名
        /// </summary>
        public string ServiceName { get; set; }
        
        /// <summary>
        /// Executed method name.
        /// 执行方法名
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// Calling parameters.
        /// 调用参数
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// Start time of the method execution.
        /// 执行时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// Total duration of the method call.
        /// 方法调用总时间
        /// </summary>
        public int ExecutionDuration { get; set; }

        /// <summary>
        /// IP address of the client.
        /// 客户端IP地址
        /// </summary>
        public string ClientIpAddress { get; set; }
        
        /// <summary>
        /// Name (generally computer name) of the client.
        /// 客户端的名称（通常是计算机名称）
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Browser information if this method is called in a web request.
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }

        /// <summary>
        /// Optional custom data that can be filled and used.
        /// 自定义数据 
        /// </summary>
        public string CustomData { get; set; }
        
        /// <summary>
        /// Exception object, if an exception occured during execution of the method.
        /// 异常对象，如果在执行过程中发生异常
        /// </summary>
        public Exception Exception { get; set; }

        public override string ToString()
        {
            return string.Format(
                "AUDIT LOG: {0}.{1} is executed by user {2} in {3} ms from {4} IP address.",
                ServiceName, MethodName, UserId, ExecutionDuration, ClientIpAddress
                );
        }
    }
}
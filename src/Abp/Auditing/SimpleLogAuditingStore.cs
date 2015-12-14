﻿using System.Threading.Tasks;
using Castle.Core.Logging;

namespace Abp.Auditing
{
    /// <summary>
    /// Implements <see cref="IAuditingStore"/> to simply write audits to logs.
    /// 简单日志审计存储
    /// </summary>
    public class SimpleLogAuditingStore : IAuditingStore
    {
        /// <summary>
        /// Singleton instance.
        /// 单例实例
        /// </summary>
        public static SimpleLogAuditingStore Instance { get { return SingletonInstance; } }
        private static readonly SimpleLogAuditingStore SingletonInstance = new SimpleLogAuditingStore();

        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SimpleLogAuditingStore()
        {
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="auditInfo">审计信息</param>
        /// <returns></returns>
        public Task SaveAsync(AuditInfo auditInfo)
        {
            Logger.Info(auditInfo.ToString());
            return Task.FromResult(0);
        }
    }
}
using System;
using System.Configuration;
using Abp.Dependency;
using Abp.Extensions;
using StackExchange.Redis;

namespace Abp.Runtime.Caching.Redis
{
    /// <summary>
    /// Implements <see cref="IAbpRedisCacheDatabaseProvider"/>.
    /// Abp Redis缓存数据提供者，实现<see cref="IAbpRedisCacheDatabaseProvider"/>。
    /// </summary>
    public class AbpRedisCacheDatabaseProvider : IAbpRedisCacheDatabaseProvider, ISingletonDependency
    {
        private const string ConnectionStringKey = "Abp.Redis.Cache";
        private const string DatabaseIdSettingKey = "Abp.Redis.Cache.DatabaseId";

        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbpRedisCacheDatabaseProvider"/> class.
        /// 构造函数
        /// </summary>
        public AbpRedisCacheDatabaseProvider()
        {
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(CreateConnectionMultiplexer);
        }

        /// <summary>
        /// Gets the database connection.
        /// 获取数据连接
        /// </summary>
        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(GetDatabaseId());
        }

        /// <summary>
        /// 创建链接多路复用器
        /// </summary>
        /// <returns></returns>
        private static ConnectionMultiplexer CreateConnectionMultiplexer()
        {
            return ConnectionMultiplexer.Connect(GetConnectionString());
        }

        /// <summary>
        /// 获取数据库Id
        /// </summary>
        /// <returns></returns>
        private static int GetDatabaseId()
        {
            var appSetting = ConfigurationManager.AppSettings[DatabaseIdSettingKey];
            if (appSetting.IsNullOrEmpty())
            {
                return -1;
            }

            int databaseId;
            if (!int.TryParse(appSetting, out databaseId))
            {
                return -1;
            }

            return databaseId;
        }

        /// <summary>
        /// 获取链接字符串
        /// </summary>
        /// <returns></returns>
        private static string GetConnectionString()
        {
            var connStr = ConfigurationManager.ConnectionStrings[ConnectionStringKey];
            if (connStr == null || connStr.ConnectionString.IsNullOrWhiteSpace())
            {
                return "localhost";
            }

            return connStr.ConnectionString;
        }
    }
}

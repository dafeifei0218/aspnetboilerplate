using System;
using Abp.Domain.Entities;
using Abp.Json;
using StackExchange.Redis;

namespace Abp.Runtime.Caching.Redis
{
    /// <summary>
    /// Used to store cache in a Redis server.
    /// Abp Redis缓存
    /// </summary>
    public class AbpRedisCache : CacheBase
    {
        private readonly IDatabase _database;

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        /// <param name="name">缓存名称</param>
        /// <param name="redisCacheDatabaseProvider">redis缓存数据提供者</param>
        public AbpRedisCache(string name, IAbpRedisCacheDatabaseProvider redisCacheDatabaseProvider)
            : base(name)
        {
            _database = redisCacheDatabaseProvider.GetDatabase();
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public override object GetOrDefault(string key)
        {
            var objbyte = _database.StringGet(GetLocalizedKey(key));
            return objbyte.HasValue
                ? JsonSerializationHelper.DeserializeWithType(objbyte)
                : null;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="slidingExpireTime">过期时间</param>
        public override void Set(string key, object value, TimeSpan? slidingExpireTime = null)
        {
            if (value == null)
            {
                //不能插入没有值的缓存
                throw new AbpException("Can not insert null values to the cache!");
            }

            //TODO: This is a workaround for serialization problems of entities.
            //TODO: Normally, entities should not be stored in the cache, but currently Abp.Zero packages does it. It will be fixed in the future.
            //这是一个实体的序列化问题解决方法。
            //通常情况下，实体不应存储在缓存中，但目前ABP.Zero包中是。这将是固定的。
            var type = value.GetType();
            if (EntityHelper.IsEntity(type) && type.Assembly.FullName.Contains("EntityFrameworkDynamicProxies"))
            {
                type = type.BaseType;
            }

            _database.StringSet(
                GetLocalizedKey(key),
                JsonSerializationHelper.SerializeWithType(value, type),
                slidingExpireTime
                );
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">键</param>
        public override void Remove(string key)
        {
            _database.KeyDelete(GetLocalizedKey(key));
        }

        /// <summary>
        /// 清除缓存
        /// </summary>
        public override void Clear()
        {
            _database.KeyDeleteWithPrefix(GetLocalizedKey("*"));
        }

        /// <summary>
        /// 获取本地化键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        private string GetLocalizedKey(string key)
        {
            return "n:" + Name + ",c:" + key;
        }
    }
}

using MongoDB.Driver;

namespace Abp.MongoDb
{
    /// <summary>
    /// Defines interface to obtain a <see cref="MongoDatabase"/> object.
    /// Mongo数据库提供者接口，定义<see cref="MongoDatabase"/>对象接口。
    /// </summary>
    public interface IMongoDatabaseProvider
    {
        /// <summary>
        /// Gets the <see cref="MongoDatabase"/>.
        /// 获取<see cref="MongoDatabase"/>Mongo数据库
        /// </summary>
        MongoDatabase Database { get; }
    }
}
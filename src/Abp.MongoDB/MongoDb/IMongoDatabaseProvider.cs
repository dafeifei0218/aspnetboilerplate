using MongoDB.Driver;

namespace Abp.MongoDb
{
    /// <summary>
    /// Defines interface to obtain a <see cref="MongoDatabase"/> object.
    /// Mongo���ݿ��ṩ�߽ӿڣ�����<see cref="MongoDatabase"/>����ӿڡ�
    /// </summary>
    public interface IMongoDatabaseProvider
    {
        /// <summary>
        /// Gets the <see cref="MongoDatabase"/>.
        /// ��ȡ<see cref="MongoDatabase"/>Mongo���ݿ�
        /// </summary>
        MongoDatabase Database { get; }
    }
}
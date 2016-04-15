namespace Abp.MongoDb.Configuration
{
    /// <summary>
    /// AbpMongoDb模块配置接口
    /// </summary>
    public interface IAbpMongoDbModuleConfiguration
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        string DatatabaseName { get; set; }
    }
}

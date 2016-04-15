namespace Abp.MongoDb.Configuration
{
    /// <summary>
    /// AbpMongoDb模块配置
    /// </summary>
    internal class AbpMongoDbModuleConfiguration : IAbpMongoDbModuleConfiguration
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DatatabaseName { get; set; }
    }
}
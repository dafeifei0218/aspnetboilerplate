using System.Reflection;
using Abp.Modules;
using Abp.MongoDb.Configuration;

namespace Abp.MongoDb
{
    /// <summary>
    /// This module is used to implement "Data Access Layer" in MongoDB.
    /// AbpMongo数据模块，这个模块用于实现MongoDB的数据层。
    /// </summary>
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpMongoDbModule : AbpModule
    {
        /// <summary>
        /// 预初始化
        /// </summary>
        public override void PreInitialize()
        {
            IocManager.Register<IAbpMongoDbModuleConfiguration, AbpMongoDbModuleConfiguration>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

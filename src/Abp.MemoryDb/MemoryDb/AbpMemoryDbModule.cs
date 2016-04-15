using System.Reflection;
using Abp.MemoryDb.Configuration;
using Abp.Modules;

namespace Abp.MemoryDb
{
    /// <summary>
    /// This module is used to implement "Data Access Layer" in MemoryDb.
    /// Abp内存数据库模块，这个模块是用来实现内存数据库的“数据访问层”
    /// </summary>
    public class AbpMemoryDbModule : AbpModule
    {
        /// <summary>
        /// 预先初始化
        /// </summary>
        public override void PreInitialize()
        {
            IocManager.Register<IAbpMemoryDbModuleConfiguration, AbpMemoryDbModuleConfiguration>();
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

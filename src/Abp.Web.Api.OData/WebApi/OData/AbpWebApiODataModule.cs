using System.Reflection;
using System.Web.OData;
using System.Web.OData.Extensions;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.WebApi.OData.Configuration;

namespace Abp.WebApi.OData
{
    /// <summary>
    /// AbpWebApiData模块
    /// </summary>
    [DependsOn(typeof(AbpWebApiModule))]
    public class AbpWebApiODataModule : AbpModule
    {
        /// <summary>
        /// 预初始化
        /// </summary>
        public override void PreInitialize()
        {
            IocManager.Register<IAbpWebApiODataModuleConfiguration, AbpWebApiODataModuleConfiguration>();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            IocManager.Register<MetadataController>(DependencyLifeStyle.Transient);
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.AbpWebApi().HttpConfiguration.MapODataServiceRoute(
                    routeName: "ODataRoute",
                    routePrefix: "odata",
                    model: Configuration.Modules.AbpWebApiOData().ODataModelBuilder.GetEdmModel()
                );
        }
    }
}

using Abp.Modules;

namespace Abp.Owin
{
    /// <summary>
    /// OWIN integration module for ABP.
    /// Abp Owin模块
    /// </summary>
    [DependsOn(typeof (AbpKernelModule))]
    public class AbpOwinModule : AbpModule
    {
        //nothing to do...
    }
}

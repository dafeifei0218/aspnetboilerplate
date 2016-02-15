using System.Collections.Generic;
using System.Reflection;

namespace Abp.Reflection
{
    /// <summary>
    /// This interface is used to get all assemblies to investigate special classes
    /// such as ABP modules.
    /// 程序集查找接口
    /// </summary>
    public interface IAssemblyFinder
    {
        /// <summary>
        /// This method should return all assemblies used by application.
        /// 获取应用程序全部程序集
        /// </summary>
        /// <returns>List of assemblies 程序集列表</returns>
        List<Assembly> GetAllAssemblies();
    }
}
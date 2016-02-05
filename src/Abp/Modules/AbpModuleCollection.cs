using System.Collections.Generic;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Extensions;

namespace Abp.Modules
{
    /// <summary>
    /// Used to store AbpModuleInfo objects as a dictionary.
    /// Abp模块字典
    /// </summary>
    internal class AbpModuleCollection : List<AbpModuleInfo>
    {
        /// <summary>
        /// Gets a reference to a module instance.
        /// 获取模块
        /// </summary>
        /// <typeparam name="TModule">Module type 模块类型</typeparam>
        /// <returns>Reference to the module instance</returns>
        public TModule GetModule<TModule>() where TModule : AbpModule
        {
            var module = this.FirstOrDefault(m => m.Type == typeof(TModule));
            if (module == null)
            {
                throw new AbpException("Can not find module for " + typeof(TModule).FullName);
            }

            return (TModule)module.Instance;
        }

        /// <summary>
        /// Sorts modules according to dependencies.
        /// If module A depends on module B, A comes after B in the returned List.
        /// 根据依赖关系排序模块。
        /// 如果模块A依赖模块B，返回的列表中模块A在模块B之后。
        /// </summary>
        /// <returns>Sorted list</returns>
        public List<AbpModuleInfo> GetSortedModuleListByDependency()
        {
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureKernelModuleToBeFirst(sortedModules);
            return sortedModules;
        }

        /// <summary>
        /// 确保核心模块在第一个
        /// </summary>
        /// <param name="sortedModules">排序模块列表</param>
        private static void EnsureKernelModuleToBeFirst(List<AbpModuleInfo> sortedModules)
        {
            var kernelModuleIndex = sortedModules.FindIndex(m => m.Type == typeof (AbpKernelModule));
            if (kernelModuleIndex > 0)
            {
                var kernelModule = sortedModules[kernelModuleIndex];
                sortedModules.RemoveAt(kernelModuleIndex);
                sortedModules.Insert(0, kernelModule);
            }
        }
    }
}
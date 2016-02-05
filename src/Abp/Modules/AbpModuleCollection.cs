using System.Collections.Generic;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Extensions;

namespace Abp.Modules
{
    /// <summary>
    /// Used to store AbpModuleInfo objects as a dictionary.
    /// Abpģ���ֵ�
    /// </summary>
    internal class AbpModuleCollection : List<AbpModuleInfo>
    {
        /// <summary>
        /// Gets a reference to a module instance.
        /// ��ȡģ��
        /// </summary>
        /// <typeparam name="TModule">Module type ģ������</typeparam>
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
        /// ����������ϵ����ģ�顣
        /// ���ģ��A����ģ��B�����ص��б���ģ��A��ģ��B֮��
        /// </summary>
        /// <returns>Sorted list</returns>
        public List<AbpModuleInfo> GetSortedModuleListByDependency()
        {
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureKernelModuleToBeFirst(sortedModules);
            return sortedModules;
        }

        /// <summary>
        /// ȷ������ģ���ڵ�һ��
        /// </summary>
        /// <param name="sortedModules">����ģ���б�</param>
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
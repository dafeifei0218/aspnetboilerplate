using System;
using System.Collections.Generic;
using System.Reflection;

namespace Abp.Modules
{
    /// <summary>
    /// Used to store all needed information for a module.
    /// 模块信息
    /// </summary>
    internal class AbpModuleInfo
    {
        /// <summary>
        /// The assembly which contains the module definition.
        /// 模块的程序集
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Type of the module.
        /// 模块类型
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Instance of the module.
        /// 模块实例
        /// </summary>
        public AbpModule Instance { get; private set; }

        /// <summary>
        /// All dependent modules of this module.
        /// 依赖，该模块所有的相关模块
        /// </summary>
        public List<AbpModuleInfo> Dependencies { get; private set; }

        /// <summary>
        /// Creates a new AbpModuleInfo object.
        /// 模块信息
        /// </summary>
        /// <param name="instance">模块实例</param>
        public AbpModuleInfo(AbpModule instance)
        {
            Dependencies = new List<AbpModuleInfo>();
            Type = instance.GetType();
            Instance = instance;
            Assembly = Type.Assembly;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}", Type.AssemblyQualifiedName);
        }
    }
}
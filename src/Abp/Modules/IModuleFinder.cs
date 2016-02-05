using System;
using System.Collections.Generic;

namespace Abp.Modules
{
    /// <summary>
    /// This interface is responsible to find all modules (derived from <see cref="AbpModule"/>)
    /// in the application.
    /// 模块过滤器，
    /// 这个接口负责查找所有模块（）。
    /// 在应用程序中。
    /// </summary>
    public interface IModuleFinder
    {
        /// <summary>
        /// Finds all modules.
        /// 查找所有模块
        /// </summary>
        /// <returns>List of modules 模块列表</returns>
        ICollection<Type> FindAll();
    }
}
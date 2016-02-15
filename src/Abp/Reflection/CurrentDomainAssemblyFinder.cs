using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Abp.Reflection
{
    /// <summary>
    /// Default implementation of <see cref="IAssemblyFinder"/>.
    /// If gets assemblies from current domain.
    /// 当前域程序集查找
    /// </summary>
    public class CurrentDomainAssemblyFinder : IAssemblyFinder
    {
        /// <summary>
        /// Gets Singleton instance of <see cref="CurrentDomainAssemblyFinder"/>.
        /// 单例
        /// </summary>
        public static CurrentDomainAssemblyFinder Instance { get { return SingletonInstance; } }
        private static readonly CurrentDomainAssemblyFinder SingletonInstance = new CurrentDomainAssemblyFinder();

        /// <summary>
        /// 获取全部程序集
        /// </summary>
        /// <returns></returns>
        public List<Assembly> GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }
    }
}
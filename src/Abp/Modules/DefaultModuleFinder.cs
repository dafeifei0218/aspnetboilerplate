using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Reflection;

namespace Abp.Modules
{
    /// <summary>
    /// 默认模块查找器
    /// </summary>
    internal class DefaultModuleFinder : IModuleFinder
    {
        /// <summary>
        /// 类型查找器
        /// </summary>
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="typeFinder">类型查找器</param>
        public DefaultModuleFinder(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        /// <summary>
        /// 查找全部
        /// </summary>
        /// <returns></returns>
        public ICollection<Type> FindAll()
        {
            return _typeFinder.Find(AbpModule.IsAbpModule).ToList();
        }
    }
}
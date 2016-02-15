using System;

namespace Abp.Modules
{
    /// <summary>
    /// Used to define dependencies of an ABP module to other modules.
    /// It should be used for a class derived from <see cref="AbpModule"/>.
    /// 依赖自定义属性，用于定义一个Abp模块依赖的其他模块。
    /// 用应该用于一个类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        /// Types of depended modules.
        /// 依赖模块的类型
        /// </summary>
        public Type[] DependedModuleTypes { get; private set; }

        /// <summary>
        /// Used to define dependencies of an ABP module to other modules.
        /// 构造函数，用于定义一个Abp模块依赖的其他模块。
        /// </summary>
        /// <param name="dependedModuleTypes">Types of depended modules 依赖模块的类型数组</param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }
    }
}
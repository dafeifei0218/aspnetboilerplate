using System;

namespace Abp
{
    /// <summary>
    /// Used to represent a named type selector.
    /// 名称类型选择器
    /// </summary>
    public class NamedTypeSelector
    {
        /// <summary>
        /// Name of the selector.
        /// 选择器名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Predicate.
        /// 条件
        /// </summary>
        public Func<Type, bool> Predicate { get; set; }

        /// <summary>
        /// Creates new <see cref="NamedTypeSelector"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="name">Name 名称</param>
        /// <param name="predicate">Predicate 条件</param>
        public NamedTypeSelector(string name, Func<Type, bool> predicate)
        {
            Name = name;
            Predicate = predicate;
        }
    }
}
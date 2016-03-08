using System;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// 输入类型属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InputTypeAttribute : Attribute
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        public InputTypeAttribute(string name)
        {
            Name = name;
        }
    }
}
using System;

namespace Abp.AutoMapper
{
    /// <summary>
    /// AutoMapTo自定义属性
    /// </summary>
    public class AutoMapToAttribute : AutoMapAttribute
    {
        /// <summary>
        /// 方向为To
        /// </summary>
        internal override AutoMapDirection Direction
        {
            get { return AutoMapDirection.To; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="targetTypes">目标类型</param>
        public AutoMapToAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
    }
}
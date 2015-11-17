using System;

namespace Abp.AutoMapper
{
    /// <summary>
    /// AutoMapFrom自定义属性
    /// </summary>
    public class AutoMapFromAttribute : AutoMapAttribute
    {
        /// <summary>
        /// 方向为From
        /// </summary>
        internal override AutoMapDirection Direction
        {
            get { return AutoMapDirection.From; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="targetTypes">目标类型</param>
        public AutoMapFromAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
    }
}
using System;

namespace Abp.AutoMapper
{
    /// <summary>
    /// AutoMap自定义属性
    /// </summary>
    public class AutoMapAttribute : Attribute
    {
        /// <summary>
        /// 目标类型
        /// </summary>
        public Type[] TargetTypes { get; private set; }

        /// <summary>
        /// 方向，From或To
        /// </summary>
        internal virtual AutoMapDirection Direction
        {
            get { return AutoMapDirection.From | AutoMapDirection.To; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="targetTypes">目标类型</param>
        public AutoMapAttribute(params Type[] targetTypes)
        {
            TargetTypes = targetTypes;
        }
    }
}
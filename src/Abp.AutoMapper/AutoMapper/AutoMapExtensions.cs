using AutoMapper;

namespace Abp.AutoMapper
{
    /// <summary>
    /// AutoMap扩展
    /// </summary>
    public static class AutoMapExtensions
    {
        /// <summary>
        /// Converts an object to another using AutoMapper library. Creates a new object of <see cref="TDestination"/>.
        /// There must be a mapping between objects before calling this method.
        /// 将对象转换为另一种使用AutoMapper。创建一个新对象的<see cref="TDestination"/>。
        /// 在调用此方法之前，必须有一个对象之间的映射关系。
        /// </summary>
        /// <typeparam name="TDestination">Type of the destination object 目标对象类型</typeparam>
        /// <param name="source">Source object 源对象</param>
        public static TDestination MapTo<TDestination>(this object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object
        /// There must be a mapping between objects before calling this method.
        /// 执行从源对象到现有目标对象的映射。
        /// 在调用此方法之前，必须有一个对象之间的映射关系。
        /// </summary>
        /// <typeparam name="TSource">Source type 源类型</typeparam>
        /// <typeparam name="TDestination">Destination type 目标类型</typeparam>
        /// <param name="source">Source object 源对象</param>
        /// <param name="destination">Destination object 目标对象</param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }
    }
}

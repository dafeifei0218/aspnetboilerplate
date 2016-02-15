using System;

namespace Abp.Reflection
{
    /// <summary>
    /// 类型查找接口
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>
        /// 查找类型
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns>类型数组</returns>
        Type[] Find(Func<Type, bool> predicate);

        /// <summary>
        /// 查找全部类型
        /// </summary>
        /// <returns>类型数组</returns>
        Type[] FindAll();
    }
}
using System.Collections.Generic;

namespace Abp.Application.Features
{
    /// <summary>
    /// Feature manager.
    /// 特征管理类。
    /// </summary>
    /// <remarks>
    /// 该接口定义根据Name返回Feature的一些方法
    /// </remarks>
    public interface IFeatureManager
    {
        /// <summary>
        /// Gets the <see cref="Feature"/> by specified name.
        /// 获取指定名称的功能。
        /// </summary>
        /// <param name="name">Unique name of the feature. 功能名称</param>
        Feature Get(string name);

        /// <summary>
        /// Gets the <see cref="Feature"/> by specified name or returns null if not found.
        /// 获取指定名称的功能，如果未找到返回null。
        /// </summary>
        /// <param name="name">The name. 功能名称</param>
        Feature GetOrNull(string name);

        /// <summary>
        /// Gets all <see cref="Feature"/>s.
        /// 获取全部功能。
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<Feature> GetAll();
    }
}
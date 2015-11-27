using System.Collections.Generic;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to return a list of items to clients.
    /// 列表结果接口
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="Items"/> list 列表项目类型</typeparam>
    public interface IListResult<T>
    {
        /// <summary>
        /// List of items.
        /// 项目列表
        /// </summary>
        IReadOnlyList<T> Items { get; set; }
    }
}
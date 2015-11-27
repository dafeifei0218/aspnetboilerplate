using System;
using System.Collections.Generic;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This class can be used to return a paged list from an <see cref="IApplicationService"/> method.
    /// 分页结果输出数据传输对象
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="ListResultDto{T}.Items"/> list 列表类型</typeparam>
    [Serializable]
    public class PagedResultOutput<T> : PagedResultDto<T>, IOutputDto
    {
        /// <summary>
        /// Creates a new <see cref="PagedResultOutput{T}"/> object.
        /// 构造函数
        /// </summary>
        public PagedResultOutput()
        {

        }

        /// <summary>
        /// Creates a new <see cref="PagedResultOutput{T}"/> object.
        /// 构造函数
        /// </summary>
        /// <param name="totalCount">Total count of Items 项目总数</param>
        /// <param name="items">List of items in current page 项目列表</param>
        public PagedResultOutput(int totalCount, IReadOnlyList<T> items)
            : base(totalCount, items)
        {

        }
    }
}
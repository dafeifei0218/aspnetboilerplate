using System;
using System.Collections.Generic;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// Implements <see cref="IPagedResult{T}"/>.
    /// ��ҳ������ݴ������
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="ListResultDto{T}.Items"/> list</typeparam>
    [Serializable]
    public class PagedResultDto<T> : ListResultDto<T>, IPagedResult<T>, IDto
    {
        /// <summary>
        /// Total count of Items.
        /// ��Ŀ����
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Creates a new <see cref="PagedResultDto{T}"/> object.
        /// ���캯��
        /// </summary>
        public PagedResultDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="PagedResultDto{T}"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="totalCount">Total count of Items ��Ŀ����</param>
        /// <param name="items">List of items in current page ��ǰҳ���е���Ŀ�б�</param>
        public PagedResultDto(int totalCount, IReadOnlyList<T> items)
            : base(items)
        {
            TotalCount = totalCount;
        }
    }
}
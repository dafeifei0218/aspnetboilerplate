using System;
using System.Collections.Generic;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This class can be used to return a paged list from an <see cref="IApplicationService"/> method.
    /// ��ҳ���������ݴ������
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="ListResultDto{T}.Items"/> list �б�����</typeparam>
    [Serializable]
    public class PagedResultOutput<T> : PagedResultDto<T>, IOutputDto
    {
        /// <summary>
        /// Creates a new <see cref="PagedResultOutput{T}"/> object.
        /// ���캯��
        /// </summary>
        public PagedResultOutput()
        {

        }

        /// <summary>
        /// Creates a new <see cref="PagedResultOutput{T}"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="totalCount">Total count of Items ��Ŀ����</param>
        /// <param name="items">List of items in current page ��Ŀ�б�</param>
        public PagedResultOutput(int totalCount, IReadOnlyList<T> items)
            : base(totalCount, items)
        {

        }
    }
}
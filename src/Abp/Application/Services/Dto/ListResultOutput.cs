using System;
using System.Collections.Generic;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This class can be used to return a list from an <see cref="IApplicationService"/> method.
    /// ����б������ݴ������
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="ListResultDto{T}.Items"/> list</typeparam>
    [Serializable]
    public class ListResultOutput<T> : ListResultDto<T>, IOutputDto
    {
        /// <summary>
        /// Creates a new <see cref="ListResultOutput{T}"/> object.
        /// ���캯��
        /// </summary>
        public ListResultOutput()
        {

        }

        /// <summary>
        /// Creates a new <see cref="ListResultOutput{T}"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="items">List of items ��Ŀ�б�</param>
        public ListResultOutput(IReadOnlyList<T> items)
            : base(items)
        {

        }
    }
}
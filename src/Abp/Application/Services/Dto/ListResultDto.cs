using System;
using System.Collections.Generic;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// Implements <see cref="IListResult{T}"/>.
    /// �б������ݴ������
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="Items"/> list</typeparam>
    [Serializable]
    public class ListResultDto<T> : IListResult<T>
    {
        /// <summary>
        /// List of items.
        /// ��Ŀ�б�
        /// </summary>
        public IReadOnlyList<T> Items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
        private IReadOnlyList<T> _items;

        /// <summary>
        /// Creates a new <see cref="ListResultDto{T}"/> object.
        /// ���캯��
        /// </summary>
        public ListResultDto()
        {
            
        }

        /// <summary>
        /// Creates a new <see cref="ListResultDto{T}"/> object.
        /// ���캯��
        /// </summary>
        /// <param name="items">List of items ��Ŀ�б�</param>
        public ListResultDto(IReadOnlyList<T> items)
        {
            Items = items;
        }
    }
}
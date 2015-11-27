using System.Collections.Generic;

namespace Abp.Application.Services.Dto
{
    /// <summary>
    /// This interface is defined to standardize to return a list of items to clients.
    /// �б����ӿ�
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="Items"/> list �б���Ŀ����</typeparam>
    public interface IListResult<T>
    {
        /// <summary>
        /// List of items.
        /// ��Ŀ�б�
        /// </summary>
        IReadOnlyList<T> Items { get; set; }
    }
}
using System.Collections.Generic;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// ���ݹ���������
    /// </summary>
    public class DataFilterConfiguration
    {
        /// <summary>
        /// ����������
        /// </summary>
        public string FilterName { get; private set; }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsEnabled { get; private set; }

        /// <summary>
        /// ��������������
        /// </summary>
        public IDictionary<string, object> FilterParameters { get; private set; }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="filterName">����������</param>
        /// <param name="isEnabled">�Ƿ�����</param>
        public DataFilterConfiguration(string filterName, bool isEnabled)
        {
            FilterName = filterName;
            IsEnabled = isEnabled;
            FilterParameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="filterToClone">���ݹ���������</param>
        internal DataFilterConfiguration(DataFilterConfiguration filterToClone)
            : this(filterToClone.FilterName, filterToClone.IsEnabled)
        {
            foreach (var filterParameter in filterToClone.FilterParameters)
            {
                FilterParameters[filterParameter.Key] = filterParameter.Value;
            }
        }
    }
}
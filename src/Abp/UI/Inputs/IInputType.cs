using System.Collections.Generic;
using Abp.Runtime.Validation;

namespace Abp.UI.Inputs
{
    /// <summary>
    /// �������ͽӿ�
    /// </summary>
    public interface IInputType
    {
        /// <summary>
        /// ����
        /// </summary>
        string Name { get; }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="key">������</param>
        /// <returns></returns>
        object this[string key] { get; set; }

        /// <summary>
        /// �����ֵ�
        /// </summary>
        IDictionary<string, object> Attributes { get; }

        /// <summary>
        /// ��֤
        /// </summary>
        IValueValidator Validator { get; set; }
    }
}
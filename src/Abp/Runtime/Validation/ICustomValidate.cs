using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// Defines interface that must be implemented by classes those must be validated with custom rules.
    /// So, implementing class can define it's own validation logic.
    /// �Զ�����֤����
    /// �����������ʵ�ֵĽӿڣ���Щ�ӿڱ������Զ�����������֤����ˣ�ʵ������Զ������Լ�����֤�߼���
    /// </summary>
    /// <remarks>
    /// �����Զ���Validation ����. ABPĬ�ϵ�Validation ����������System.ComponentModel.DataAnnotations�еĹ���
    /// ���Ҫ����Զ���Validation ������Ҫʵ��ICustomValidate�ӿڡ�
    /// </remarks>
    public interface ICustomValidate : IValidate
    {
        /// <summary>
        /// This method is used to validate the object.
        /// �����֤���󣬴˷���������֤����
        /// </summary>
        /// <param name="results">List of validation results (errors). Add validation errors to this list. ��֤����б����󣩡�����֤������ӵ����б���</param>
        void AddValidationErrors(List<ValidationResult> results);
    }
}
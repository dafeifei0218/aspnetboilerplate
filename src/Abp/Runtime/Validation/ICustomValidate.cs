using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// Defines interface that must be implemented by classes those must be validated with custom rules.
    /// So, implementing class can define it's own validation logic.
    /// 自定义验证器
    /// </summary>
    public interface ICustomValidate : IValidate
    {
        /// <summary>
        /// This method is used to validate the object.
        /// 添加验证错误，此方法用于验证对象
        /// </summary>
        /// <param name="results">List of validation results (errors). Add validation errors to this list. 验证结果列表（错误）。将验证错误添加到该列表中</param>
        void AddValidationErrors(List<ValidationResult> results);
    }
}
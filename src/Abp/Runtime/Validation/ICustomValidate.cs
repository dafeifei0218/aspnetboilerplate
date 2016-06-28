using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// Defines interface that must be implemented by classes those must be validated with custom rules.
    /// So, implementing class can define it's own validation logic.
    /// 自定义验证器，
    /// 定义必须由类实现的接口，这些接口必须与自定义规则进行验证。因此，实现类可以定义它自己的验证逻辑。
    /// </summary>
    /// <remarks>
    /// 用于自定义Validation 规则. ABP默认的Validation 规则是来自System.ComponentModel.DataAnnotations中的规则。
    /// 如果要添加自定义Validation 规则，需要实现ICustomValidate接口。
    /// </remarks>
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
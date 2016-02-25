using System;

namespace Abp.Runtime.Validation
{
    /// <summary>
    /// Can be added to a method to disable auto validation.
    /// 禁用自动验证自定义属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class DisableValidationAttribute : Attribute
    {
        
    }
}
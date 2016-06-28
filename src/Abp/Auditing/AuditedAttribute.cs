using System;

namespace Abp.Auditing
{
    /// <summary>
    /// This attribute is used to apply audit logging for a single method or
    /// all methods of a class or interface.
    /// 审计自定义属性，用于方法或类。
    /// 此属性用于将生和日志应用于一个类或接口的一个单独方法或所有方法。
    /// </summary>
    /// <remarks>
    /// 用于标识一个方法或一个类的所有方法都需要启用Auditing功能。
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuditedAttribute : Attribute
    {

    }
}

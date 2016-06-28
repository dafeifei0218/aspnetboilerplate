using System;

namespace Abp.Auditing
{
    /// <summary>
    /// Used to disable auditing for a single method or
    /// all methods of a class or interface.
    /// 禁用审计自定义属性，用于方法或类
    /// </summary>
    /// <remarks>
    /// 用于标识一个方法或一个类的所有方法都需要关闭Auditing功能。
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableAuditingAttribute : Attribute
    {

    }
}
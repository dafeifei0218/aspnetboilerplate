using System.Collections.Generic;

namespace Abp.Auditing
{
    /// <summary>
    /// List of selector functions to select classes/interfaces to be audited.
    /// 审计选择器列表接口，选择类/接口的选择器列表
    /// </summary>
    /// <remarks>
    /// 是一个NamedTypeSelector对象的集合
    /// </remarks>
    public interface IAuditingSelectorList : IList<NamedTypeSelector>
    {
        /// <summary>
        /// Removes a selector by name.
        /// 根据名称删除选择器
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        bool RemoveByName(string name);
    }
}
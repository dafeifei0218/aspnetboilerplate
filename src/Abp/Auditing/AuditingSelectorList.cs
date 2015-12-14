using System.Collections.Generic;

namespace Abp.Auditing
{
    /// <summary>
    /// 审计选择器列表
    /// </summary>
    internal class AuditingSelectorList : List<NamedTypeSelector>, IAuditingSelectorList
    {
        /// <summary>
        /// 根据名称删除选择器
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public bool RemoveByName(string name)
        {
            return RemoveAll(s => s.Name == name) > 0;
        }
    }
}
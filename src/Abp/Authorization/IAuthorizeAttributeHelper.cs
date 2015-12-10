using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// ��Ȩ�Զ������԰�����
    /// </summary>
    internal interface IAuthorizeAttributeHelper
    {
        /// <summary>
        /// ��Ȩ-�첽
        /// </summary>
        /// <param name="authorizeAttributes">��Ȩ�Զ������Լ���</param>
        /// <returns></returns>
        Task AuthorizeAsync(IEnumerable<IAbpAuthorizeAttribute> authorizeAttributes);
        
        /// <summary>
        /// ��Ȩ-�첽
        /// </summary>
        /// <param name="authorizeAttribute">��Ȩ�Զ�������</param>
        /// <returns></returns>
        Task AuthorizeAsync(IAbpAuthorizeAttribute authorizeAttribute);
        
        /// <summary>
        /// ��Ȩ
        /// </summary>
        /// <param name="authorizeAttributes">��Ȩ�Զ������Լ���</param>
        void Authorize(IEnumerable<IAbpAuthorizeAttribute> authorizeAttributes);
        
        /// <summary>
        /// ��Ȩ
        /// </summary>
        /// <param name="authorizeAttribute">��Ȩ�Զ�������</param>
        void Authorize(IAbpAuthorizeAttribute authorizeAttribute);
    }
}
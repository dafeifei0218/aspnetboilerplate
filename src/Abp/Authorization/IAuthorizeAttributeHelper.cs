using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Authorization
{
    /// <summary>
    /// 授权自定义属性帮助类
    /// </summary>
    internal interface IAuthorizeAttributeHelper
    {
        /// <summary>
        /// 授权-异步
        /// </summary>
        /// <param name="authorizeAttributes">授权自定义属性集合</param>
        /// <returns></returns>
        Task AuthorizeAsync(IEnumerable<IAbpAuthorizeAttribute> authorizeAttributes);
        
        /// <summary>
        /// 授权-异步
        /// </summary>
        /// <param name="authorizeAttribute">授权自定义属性</param>
        /// <returns></returns>
        Task AuthorizeAsync(IAbpAuthorizeAttribute authorizeAttribute);
        
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="authorizeAttributes">授权自定义属性集合</param>
        void Authorize(IEnumerable<IAbpAuthorizeAttribute> authorizeAttributes);
        
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="authorizeAttribute">授权自定义属性</param>
        void Authorize(IAbpAuthorizeAttribute authorizeAttribute);
    }
}
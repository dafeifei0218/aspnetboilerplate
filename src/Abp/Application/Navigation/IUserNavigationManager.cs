using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// Used to manage navigation for users.
    /// 用户导航管理接口
    /// </summary>
    public interface IUserNavigationManager
    {
        /// <summary>
        /// Gets a menu specialized for given user.
        /// 获取给定用户和菜单项名称的导航（菜单）
        /// </summary>
        /// <param name="menuName">Unique name of the menu 菜单名称</param>
        /// <param name="userId">User id or null for anonymous users 用户Id或null（表示匿名用户）</param>
        /// <param name="tenantId">租户Id</param>
        Task<UserMenu> GetMenuAsync(string menuName, long? userId, int? tenantId = null);

        /// <summary>
        /// Gets all menus specialized for given user.
        /// 获取给用户的所有菜单项
        /// </summary>
        /// <param name="userId">User id or null for anonymous users  用户Id或null（表示匿名用户）</param>
        /// <param name="tenantId">租户Id</param>
        Task<IReadOnlyList<UserMenu>> GetMenusAsync(long? userId, int? tenantId = null);
    }
}
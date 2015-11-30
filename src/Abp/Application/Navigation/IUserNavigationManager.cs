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
        /// 获取菜单
        /// </summary>
        /// <param name="menuName">Unique name of the menu 菜单名</param>
        /// <param name="userId">User id or null for anonymous users 用户Id</param>
        Task<UserMenu> GetMenuAsync(string menuName, long? userId);

        /// <summary>
        /// Gets all menus specialized for given user.
        /// 获取菜单
        /// </summary>
        /// <param name="userId">User id or null for anonymous users  用户Id</param>
        Task<IReadOnlyList<UserMenu>> GetMenusAsync(long? userId);
    }
}
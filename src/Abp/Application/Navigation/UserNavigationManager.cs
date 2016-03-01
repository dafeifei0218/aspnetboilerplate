using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Features;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Runtime.Session;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// 用户导航管理类
    /// </summary>
    internal class UserNavigationManager : IUserNavigationManager, ITransientDependency
    {
        /// <summary>
        /// 权限检查器
        /// </summary>
        public IPermissionChecker PermissionChecker { get; set; }

        /// <summary>
        /// Abp会话
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// 导航管理器
        /// </summary>
        private readonly INavigationManager _navigationManager;
        /// <summary>
        /// 本地化上下文
        /// </summary>
        private readonly ILocalizationContext _localizationContext;
        /// <summary>
        /// Ioc解析器
        /// </summary>
        private readonly IIocResolver _iocResolver;

        //private readonly IFeatureDependencyContext _featureDependencyContext;

        //public UserNavigationManager(
        //    INavigationManager navigationManager, 
        //    IFeatureDependencyContext featureDependencyContext)
        //{
        //    _navigationManager = navigationManager;
        //    _featureDependencyContext = featureDependencyContext;
        //    PermissionChecker = NullPermissionChecker.Instance;
        //    AbpSession = NullAbpSession.Instance;
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="navigationManager">导航管理器</param>
        /// <param name="localizationContext">本地化上下文</param>
        /// <param name="iocResolver">Ioc解析</param>
        public UserNavigationManager(
            INavigationManager navigationManager,
            ILocalizationContext localizationContext,
            IIocResolver iocResolver)
        {
            _navigationManager = navigationManager;
            _localizationContext = localizationContext;
            _iocResolver = iocResolver;
            PermissionChecker = NullPermissionChecker.Instance;
            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// 获取给定用户和菜单项名称的菜单-异步
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        /// <param name="userId">用户Id或null(表示匿名用户）</param>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        public async Task<UserMenu> GetMenuAsync(string menuName, long? userId, int? tenantId = null)
        {
            var menuDefinition = _navigationManager.Menus.GetOrDefault(menuName);
            if (menuDefinition == null)
            {
                throw new AbpException("There is no menu with given name: " + menuName);
            }

            var userMenu = new UserMenu(menuDefinition, _localizationContext);
            await FillUserMenuItems(tenantId, userId, menuDefinition.Items, userMenu.Items);
            return userMenu;
        }

        /// <summary>
        ///  获取给用户的所有菜单项-异步
        /// </summary>
        /// <param name="userId">用户Id或null(表示匿名用户）</param>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<UserMenu>> GetMenusAsync(long? userId, int? tenantId = null)
        {
            var userMenus = new List<UserMenu>();

            foreach (var menu in _navigationManager.Menus.Values)
            {
                userMenus.Add(await GetMenuAsync(menu.Name, userId));
            }

            return userMenus;
        }

        /// <summary>
        /// 填充用户菜单项
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <param name="userId">用户Id或null(表示匿名用户）</param>
        /// <param name="menuItemDefinitions">菜单项定义集合</param>
        /// <param name="userMenuItems">用户菜单项集合</param>
        /// <returns></returns>
        private async Task<int> FillUserMenuItems(int? tenantId, long? userId, IList<MenuItemDefinition> menuItemDefinitions, IList<UserMenuItem> userMenuItems)
        {
            var addedMenuItemCount = 0;

            using (var featureDependencyContext = _iocResolver.ResolveAsDisposable<FeatureDependencyContext>())
            {
                featureDependencyContext.Object.TenantId = tenantId;

                foreach (var menuItemDefinition in menuItemDefinitions)
                {
                    if (menuItemDefinition.RequiresAuthentication && !userId.HasValue)
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(menuItemDefinition.RequiredPermissionName) &&
                        (!userId.HasValue ||
                         !(await
                             PermissionChecker.IsGrantedAsync(userId.Value, menuItemDefinition.RequiredPermissionName))))
                    {
                        continue;
                    }

                    if (menuItemDefinition.FeatureDependency != null &&
                        AbpSession.MultiTenancySide == MultiTenancySides.Tenant &&
                        !(await menuItemDefinition.FeatureDependency.IsSatisfiedAsync(featureDependencyContext.Object)))
                    {
                        continue;
                    }

                    var userMenuItem = new UserMenuItem(menuItemDefinition, _localizationContext);
                    //if (menuItemDefinition.IsLeaf ||
                    //    (await FillUserMenuItems(userId, menuItemDefinition.Items, userMenuItem.Items)) > 0)
                    //{
                    //    userMenuItems.Add(userMenuItem);
                    //    ++addedMenuItemCount;
                    //}
                    if (menuItemDefinition.IsLeaf || (await FillUserMenuItems(tenantId, userId, menuItemDefinition.Items, userMenuItem.Items)) > 0)
                    {
                        userMenuItems.Add(userMenuItem);
                        ++addedMenuItemCount;
                    }
                }
            }

            return addedMenuItemCount;
        }
    }
}

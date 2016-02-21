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
    internal class UserNavigationManager : IUserNavigationManager, ITransientDependency
    {
        public IPermissionChecker PermissionChecker { get; set; }

        public IAbpSession AbpSession { get; set; }

        private readonly INavigationManager _navigationManager;
        private readonly ILocalizationContext _localizationContext;
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
        /// <param name="navigationManager"></param>
        /// <param name="localizationContext"></param>
        /// <param name="iocResolver"></param>
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
        /// 获取菜单
        /// </summary>
        /// <param name="menuName">菜单名称</param>
        /// <param name="userId">用户Id</param>
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
        /// 获取菜单
        /// </summary>
        /// <param name="userId">用户Id</param>
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

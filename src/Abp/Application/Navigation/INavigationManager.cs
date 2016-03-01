using System.Collections.Generic;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// Manages navigation in the application.
    /// 导航管理接口
    /// </summary>
    public interface INavigationManager
    {
        /// <summary>
        /// All menus defined in the application.
        /// 菜单，在应用程序中定义的所有菜单
        /// </summary>
        IDictionary<string, MenuDefinition> Menus { get; }

        /// <summary>
        /// Gets the main menu of the application.
        /// A shortcut of <see cref="Menus"/>["MainMenu"].
        /// 获取应用程序的主菜单
        /// </summary>
        MenuDefinition MainMenu { get; }
    }
}

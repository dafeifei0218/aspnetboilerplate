using Abp.Dependency;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// This interface should be implemented by classes which change
    /// navigation of the application.
    /// 导航提供者，
    /// 改变应用导航的类需要实现此接口。
    /// </summary>
    /// <remarks>
    /// 功能类似于FeatureProvider。
    /// 抽象基类，用于设置NavigationManager的Menus和MainMenu（通过 INavigationProviderContext对象访问NavigationManager）。
    /// Abp框架只提供了抽象类，下面代码是一个简单的示例。
    /// 实际项目中可以创建自定义NavigationProvider来从数据库中读取Menu信息来填充到NavigationManager对象中。
    /// 下面以SimpleTask项目为例： 
    /// 该项目自定义了NavigationProvider的派生类，并且在module的PostInitialize方法中将其注册到abp底层框架的configuration 中。
    /// </remarks>
    public abstract class NavigationProvider : ITransientDependency
    {
        /// <summary>
        /// Used to set navigation.
        /// 用于设置导航
        /// </summary>
        /// <param name="context">Navigation context 导航上下文</param>
        public abstract void SetNavigation(INavigationProviderContext context);
    }
}
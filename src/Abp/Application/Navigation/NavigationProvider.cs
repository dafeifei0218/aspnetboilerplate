﻿using Abp.Dependency;

namespace Abp.Application.Navigation
{
    /// <summary>
    /// This interface should be implemented by classes which change
    /// navigation of the application.
    /// 导航提供者
    /// </summary>
    public abstract class NavigationProvider : ITransientDependency
    {
        /// <summary>
        /// Used to set navigation.
        /// 设置导航
        /// </summary>
        /// <param name="context">Navigation context 导航上下文</param>
        public abstract void SetNavigation(INavigationProviderContext context);
    }
}
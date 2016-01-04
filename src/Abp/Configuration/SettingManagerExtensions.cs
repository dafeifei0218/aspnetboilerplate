using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Threading;

namespace Abp.Configuration
{
    /// <summary>
    /// Extension methods for <see cref="ISettingManager"/>.
    /// 设置管理扩展类
    /// </summary>
    public static class SettingManagerExtensions
    {
        /// <summary>
        /// Gets value of a setting in given type (<see cref="T"/>).
        /// 获取设置值-异步
        /// </summary>
        /// <typeparam name="T">Type of the setting to get 设置类型</typeparam>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <returns>Value of the setting 设置值</returns>
        public static async Task<T> GetSettingValueAsync<T>(this ISettingManager settingManager, string name)
            where T : struct
        {
            return (await settingManager.GetSettingValueAsync(name)).To<T>();
        }

        /// <summary>
        /// Gets current value of a setting for the application level.
        /// 获取应用程序级别的设置的当前值-异步
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <returns>Current value of the setting for the application 应用程序的设置值</returns>
        public static async Task<T> GetSettingValueForApplicationAsync<T>(this ISettingManager settingManager, string name)
            where T : struct
        {
            return (await settingManager.GetSettingValueForApplicationAsync(name)).To<T>();
        }

        /// <summary>
        /// Gets current value of a setting for a tenant level.
        /// It gets the setting value, overwritten by given tenant.
        /// 获取租户级别的当前设置值
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="tenantId">Tenant id 租户Id</param>
        /// <returns>Current value of the setting 设置值</returns>
        public static async Task<T> GetSettingValueForTenantAsync<T>(this ISettingManager settingManager, string name, int tenantId)
           where T : struct
        {
            return (await settingManager.GetSettingValueForTenantAsync(name, tenantId)).To<T>();
        }

        /// <summary>
        /// Gets current value of a setting for a user level.
        /// It gets the setting value, overwritten by given tenant and user.
        /// 获取用户级别的当前设置值
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="tenantId">Tenant id 租户Id</param>
        /// <param name="userId">User id 用户Id</param>
        /// <returns>Current value of the setting for the user 设置值</returns>
        public static async Task<T> GetSettingValueForUserAsync<T>(this ISettingManager settingManager, string name, int tenantId, long userId)
           where T : struct
        {
            return (await settingManager.GetSettingValueForUserAsync(name, tenantId, userId)).To<T>();
        }

        /// <summary>
        /// Gets current value of a setting.
        /// It gets the setting value, overwritten by application and the current user if exists.
        /// 获取设置值
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <returns>Current value of the setting 设置值</returns>
        public static string GetSettingValue(this ISettingManager settingManager, string name)
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueAsync(name));
        }

        /// <summary>
        /// Gets current value of a setting for the application level.
        /// 获取应用程序级别的设置值
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <returns>Current value of the setting for the application 设置值</returns>
        public static string GetSettingValueForApplication(this ISettingManager settingManager, string name)
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForApplicationAsync(name));
        }

        /// <summary>
        /// Gets current value of a setting for a tenant level.
        /// It gets the setting value, overwritten by given tenant.
        /// 获取租户级别的设置值
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="tenantId">Tenant id 租户Id</param>
        /// <returns>Current value of the setting 设置值</returns>
        public static string GetSettingValueForTenant(this ISettingManager settingManager, string name, int tenantId)
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForTenantAsync(name, tenantId));
        }

        /// <summary>
        /// Gets current value of a setting for a user level.
        /// It gets the setting value, overwritten by given tenant and user.
        /// 获取用户界别的设置值
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="tenantId">Tenant id 租户Id</param>
        /// <param name="userId">User id 用户Id</param>
        /// <returns>Current value of the setting for the user 设置值</returns>
        public static string GetSettingValueForUser(this ISettingManager settingManager, string name, int tenantId, long userId)
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForUserAsync(name, tenantId, userId));
        }

        /// <summary>
        /// Gets value of a setting.
        /// 获取设置值
        /// </summary>
        /// <typeparam name="T">Type of the setting to get</typeparam>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <returns>Value of the setting 设置值</returns>
        public static T GetSettingValue<T>(this ISettingManager settingManager, string name)
            where T : struct
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueAsync<T>(name));
        }
        
        /// <summary>
        /// Gets current value of a setting for the application level.
        /// 获取应用程序级别的设置值
        /// </summary>
        /// <typeparam name="T">Type of the setting to get 设置类型</typeparam>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <returns>Current value of the setting for the application 应用程序当前的设置值</returns>
        public static T GetSettingValueForApplication<T>(this ISettingManager settingManager, string name)
            where T : struct
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForApplicationAsync<T>(name));
        }

        /// <summary>
        /// Gets current value of a setting for a tenant level.
        /// It gets the setting value, overwritten by given tenant.
        /// 获取租户级别的设置值
        /// </summary>
        /// <typeparam name="T">Type of the setting to get 设置类型</typeparam>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="tenantId">Tenant id 租户Id</param>
        /// <returns>Current value of the setting 当前设置值</returns>
        public static T GetSettingValueForTenant<T>(this ISettingManager settingManager, string name, int tenantId)
            where T : struct
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForTenantAsync<T>(name, tenantId));
        }

        /// <summary>
        /// Gets current value of a setting for a user level.
        /// It gets the setting value, overwritten by given tenant and user.
        /// 获取用户级别的设置值
        /// </summary>
        /// <typeparam name="T">Type of the setting to get 设置类型</typeparam>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="tenantId">Tenant id 租户Id</param>
        /// <param name="userId">User id 用户Id</param>
        /// <returns>Current value of the setting for the user</returns>
        public static T GetSettingValueForUser<T>(this ISettingManager settingManager, string name, int tenantId, long userId)
            where T : struct
        {
            return AsyncHelper.RunSync(() => settingManager.GetSettingValueForUserAsync<T>(name, tenantId, userId));
        }
        
        /// <summary>
        /// Gets current values of all settings.
        /// It gets all setting values, overwritten by application and the current user if exists.
        /// 获取全部设置值
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <returns>List of setting values 设置值列表</returns>
        public static IReadOnlyList<ISettingValue> GetAllSettingValues(this ISettingManager settingManager)
        {
            return AsyncHelper.RunSync(settingManager.GetAllSettingValuesAsync);
        }

        /// <summary>
        /// Gets a list of all setting values specified for the application.
        /// It returns only settings those are explicitly set for the application.
        /// If a setting's default value is used, it's not included the result list.
        /// If you want to get current values of all settings, use <see cref="GetAllSettingValues"/> method.
        /// 获取应用程序的全部值
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <returns>List of setting values 设置值列表</returns>
        public static IReadOnlyList<ISettingValue> GetAllSettingValuesForApplication(this ISettingManager settingManager)
        {
            return AsyncHelper.RunSync(settingManager.GetAllSettingValuesForApplicationAsync);
        }

        /// <summary>
        /// Gets a list of all setting values specified for a tenant.
        /// It returns only settings those are explicitly set for the tenant.
        /// If a setting's default value is used, it's not included the result list.
        /// If you want to get current values of all settings, use <see cref="GetAllSettingValues"/> method.
        /// 获取租户的设置值列表
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="tenantId">Tenant to get settings 租户Id</param>
        /// <returns>List of setting values 设置值列表</returns>
        public static IReadOnlyList<ISettingValue> GetAllSettingValuesForTenant(this ISettingManager settingManager, int tenantId)
        {
            return AsyncHelper.RunSync(() => settingManager.GetAllSettingValuesForTenantAsync(tenantId));
        }

        /// <summary>
        /// Gets a list of all setting values specified for a user.
        /// It returns only settings those are explicitly set for the user.
        /// If a setting's value is not set for the user (for example if user uses the default value), it's not included the result list.
        /// If you want to get current values of all settings, use <see cref="GetAllSettingValues"/> method.
        /// 获取用户的设置值列表
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="userId">User to get settings 用户Id</param>
        /// <returns>All settings of the user 用户全部的设置值</returns>
        public static IReadOnlyList<ISettingValue> GetAllSettingValuesForTenant(this ISettingManager settingManager, long userId)
        {
            return AsyncHelper.RunSync(() => settingManager.GetAllSettingValuesForUserAsync(userId));
        }

        /// <summary>
        /// Changes setting for the application level.
        /// 根据给定的应用程序级别，更改设置
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="value">Value of the setting 设置值</param>
        public static void ChangeSettingForApplication(this ISettingManager settingManager, string name, string value)
        {
            AsyncHelper.RunSync(() => settingManager.ChangeSettingForApplicationAsync(name, value));
        }

        /// <summary>
        /// Changes setting for a Tenant.
        /// 根据给定的租户，更改设置
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="tenantId">TenantId 租户Id</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="value">Value of the setting 设置值</param>
        public static void ChangeSettingForTenant(this ISettingManager settingManager, int tenantId, string name, string value)
        {
            AsyncHelper.RunSync(() => settingManager.ChangeSettingForTenantAsync(tenantId, name, value));
        }

        /// <summary>
        /// Changes setting for a user.
        /// 根据给定的用户，更改设置
        /// </summary>
        /// <param name="settingManager">Setting manager 设置管理类</param>
        /// <param name="userId">UserId 用户Id</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="value">Value of the setting 设置值</param>
        public static void ChangeSettingForUser(this ISettingManager settingManager, long userId, string name, string value)
        {
            AsyncHelper.RunSync(() => settingManager.ChangeSettingForUserAsync(userId, name, value));
        }
    }
}
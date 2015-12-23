using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Configuration
{
    /// <summary>
    /// This is the main interface that must be implemented to be able to load/change values of settings.
    /// 设置管理类接口，
    /// 这是必须实现的主要接口，以能够加载/更改设置值
    /// </summary>
    public interface ISettingManager
    {
        /// <summary>
        /// Gets current value of a setting.
        /// It gets the setting value, overwritten by application, current tenant and current user if exists.
        /// 获取当前设置值-异步
        /// 获取设置值，重写应用程序，当前租户和当前用户如果存在，
        /// </summary>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <returns>Current value of the setting 设置值</returns>
        Task<string> GetSettingValueAsync(string name);

        /// <summary>
        /// Gets current value of a setting for the application level.
        /// 获取应用程序级别的设置的当前值-异步
        /// </summary>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <returns>Current value of the setting for the application 应用程序的设置值</returns>
        Task<string> GetSettingValueForApplicationAsync(string name);

        /// <summary>
        /// Gets current value of a setting for a tenant level.
        /// It gets the setting value, overwritten by given tenant.
        /// 获取租户级别的这只的当前值-异步，
        /// 获取设置值，重写租户
        /// </summary>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="tenantId">Tenant id 租户Id</param>
        /// <returns>Current value of the setting 设置值</returns>
        Task<string> GetSettingValueForTenantAsync(string name, int tenantId);

        /// <summary>
        /// Gets current value of a setting for a user level.
        /// It gets the setting value, overwritten by given tenant and user.
        /// 获取用户级别的设置值-异步，
        /// 获取设置值，重写租户和用户
        /// </summary>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="tenantId">Tenant id 租户Id</param>
        /// <param name="userId">User id 用户Id</param>
        /// <returns>Current value of the setting for the user 当前用户的设置值</returns>
        Task<string> GetSettingValueForUserAsync(string name, int? tenantId, long userId);

        /// <summary>
        /// Gets current values of all settings.
        /// It gets all setting values, overwritten by application, current tenant (if exists) and the current user (if exists).
        /// 获取全部设置值-异步，
        /// 获取全部设置值，覆盖的应用，当前的租户（如果存在）和当前用户（如果存在）
        /// </summary>
        /// <returns>List of setting values 设置列表</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync();

        /// <summary>
        /// Gets current values of all settings.
        /// It gets default values of all settings then overwrites by given scopes.
        /// 获取全部的设置值-异步
        /// </summary>
        /// <param name="scopes">One or more scope to overwrite 设置范围</param>
        /// <returns>List of setting values 设置列表</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync(SettingScopes scopes);

        /// <summary>
        /// Gets a list of all setting values specified for the application.
        /// It returns only settings those are explicitly set for the application.
        /// If a setting's default value is used, it's not included the result list.
        /// If you want to get current values of all settings, use <see cref="GetAllSettingValuesAsync()"/> method.
        /// 获取应用程序的全部设置值列表-异步
        /// </summary>
        /// <returns>List of setting values 设置列表</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForApplicationAsync();

        /// <summary>
        /// Gets a list of all setting values specified for a tenant.
        /// It returns only settings those are explicitly set for the tenant.
        /// If a setting's default value is used, it's not included the result list.
        /// If you want to get current values of all settings, use <see cref="GetAllSettingValuesAsync()"/> method.
        /// 获取给定租户的全部设置值列表-异步，
        /// 它只返回给定租户显示设置的设置。
        /// 如果使用设置的默认值，则不搞扩结果列表。
        /// </summary>
        /// <param name="tenantId">Tenant to get settings 租户id</param>
        /// <returns>List of setting values 设置列表</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForTenantAsync(int tenantId);

        /// <summary>
        /// Gets a list of all setting values specified for a user.
        /// It returns only settings those are explicitly set for the user.
        /// If a setting's value is not set for the user (for example if user uses the default value), it's not included the result list.
        /// If you want to get current values of all settings, use <see cref="GetAllSettingValuesAsync()"/> method.
        /// 获取给定用户的全部设置值列表-异步
        /// </summary>
        /// <param name="userId">User to get settings 用户Id</param>
        /// <returns>All settings of the user 该用户的设置列表</returns>
        Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForUserAsync(long userId);

        /// <summary>
        /// Changes setting for the application level.
        /// 应用程序级别更改设置值
        /// </summary>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="value">Value of the setting 设置值</param>
        Task ChangeSettingForApplicationAsync(string name, string value);

        /// <summary>
        /// Changes setting for a Tenant.
        /// 租户更改设置值
        /// </summary>
        /// <param name="tenantId">TenantId 租户Id</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="value">Value of the setting 设置值</param>
        Task ChangeSettingForTenantAsync(int tenantId, string name, string value);

        /// <summary>
        /// Changes setting for a user.
        /// 用户更改设置值
        /// </summary>
        /// <param name="userId">UserId 用户Id</param>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <param name="value">Value of the setting 设置值</param>
        Task ChangeSettingForUserAsync(long userId, string name, string value);
    }
}

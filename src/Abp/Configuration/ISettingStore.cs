using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Configuration
{
    /// <summary>
    /// This interface is used to get/set settings from/to a data source (database).
    /// 设置存储接口，
    /// </summary>
    public interface ISettingStore
    {
        /// <summary>
        /// Gets a setting or null.
        /// 获取设置或null-异步
        /// </summary>
        /// <param name="tenantId">TenantId or null 租户Id</param>
        /// <param name="userId">UserId or null 用户Id</param>
        /// <param name="name">Name of the setting 用户名</param>
        /// <returns>Setting object</returns>
        Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name);

        /// <summary>
        /// Deletes a setting.
        /// 删除设置-异步
        /// </summary>
        /// <param name="setting">Setting to be deleted 设置信息</param>
        Task DeleteAsync(SettingInfo setting);

        /// <summary>
        /// Adds a setting.
        /// 添加设置-异步
        /// </summary>
        /// <param name="setting">Setting to add 设置信息</param>
        Task CreateAsync(SettingInfo setting);

        /// <summary>
        /// Update a setting.
        /// 更新设置-异步
        /// </summary>
        /// <param name="setting">Setting to Update 设置信息</param>
        Task UpdateAsync(SettingInfo setting);

        /// <summary>
        /// Gets a list of setting.
        /// 获取全部设置-异步
        /// </summary>
        /// <param name="tenantId">TenantId or null</param>
        /// <param name="userId">UserId or null</param>
        /// <returns>List of settings</returns>
        Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId);
    }
}
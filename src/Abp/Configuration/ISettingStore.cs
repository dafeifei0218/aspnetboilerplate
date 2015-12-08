using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abp.Configuration
{
    /// <summary>
    /// This interface is used to get/set settings from/to a data source (database).
    /// ���ô洢�ӿڣ�
    /// </summary>
    public interface ISettingStore
    {
        /// <summary>
        /// Gets a setting or null.
        /// ��ȡ���û�null-�첽
        /// </summary>
        /// <param name="tenantId">TenantId or null �⻧Id</param>
        /// <param name="userId">UserId or null �û�Id</param>
        /// <param name="name">Name of the setting �û���</param>
        /// <returns>Setting object</returns>
        Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name);

        /// <summary>
        /// Deletes a setting.
        /// ɾ������-�첽
        /// </summary>
        /// <param name="setting">Setting to be deleted ������Ϣ</param>
        Task DeleteAsync(SettingInfo setting);

        /// <summary>
        /// Adds a setting.
        /// �������-�첽
        /// </summary>
        /// <param name="setting">Setting to add ������Ϣ</param>
        Task CreateAsync(SettingInfo setting);

        /// <summary>
        /// Update a setting.
        /// ��������-�첽
        /// </summary>
        /// <param name="setting">Setting to Update ������Ϣ</param>
        Task UpdateAsync(SettingInfo setting);

        /// <summary>
        /// Gets a list of setting.
        /// ��ȡȫ������-�첽
        /// </summary>
        /// <param name="tenantId">TenantId or null</param>
        /// <param name="userId">UserId or null</param>
        /// <returns>List of settings</returns>
        Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId);
    }
}
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Abp.Logging;

namespace Abp.Configuration
{
    /// <summary>
    /// Implements default behavior for ISettingStore.
    /// Only <see cref="GetSettingOrNullAsync"/> method is implemented and it gets setting's value
    /// from application's configuration file if exists, or returns null if not.
    /// 默认配置设置存储
    /// </summary>
    public class DefaultConfigSettingStore : ISettingStore
    {
        /// <summary>
        /// Gets singleton instance.
        /// 获取单例实例
        /// </summary>
        public static DefaultConfigSettingStore Instance { get { return SingletonInstance; } }
        private static readonly DefaultConfigSettingStore SingletonInstance = new DefaultConfigSettingStore();

        private DefaultConfigSettingStore()
        {
        }

        /// <summary>
        /// 获取设置-异步
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<SettingInfo> GetSettingOrNullAsync(int? tenantId, long? userId, string name)
        {
            var value = ConfigurationManager.AppSettings[name];
            
            if (value == null)
            {
                return Task.FromResult<SettingInfo>(null);
            }

            return Task.FromResult(new SettingInfo(tenantId, userId, name, value));
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support DeleteAsync.");
        }

        /// <inheritdoc/>
        public async Task CreateAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support CreateAsync.");
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(SettingInfo setting)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support UpdateAsync.");
        }

        /// <inheritdoc/>
        public Task<List<SettingInfo>> GetAllListAsync(int? tenantId, long? userId)
        {
            LogHelper.Logger.Warn("ISettingStore is not implemented, using DefaultConfigSettingStore which does not support GetAllListAsync.");
            return Task.FromResult(new List<SettingInfo>());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Abp.Collections.Extensions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Runtime.Session;

namespace Abp.Configuration
{
    /// <summary>
    /// This class implements <see cref="ISettingManager"/> to manage setting values in the database.
    /// 设置管理类
    /// </summary>
    public class SettingManager : ISettingManager, ISingletonDependency
    {
        /// <summary>
        /// 应用程序设置缓存键
        /// </summary>
        public const string ApplicationSettingsCacheKey = "ApplicationSettings";

        /// <summary>
        /// Reference to the current Session.
        /// Abp会话
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// Reference to the setting store.
        /// 设置范围
        /// </summary>
        public ISettingStore SettingStore { get; set; }

        private readonly ISettingDefinitionManager _settingDefinitionManager;
        private readonly ITypedCache<string, Dictionary<string, SettingInfo>> _applicationSettingCache;
        private readonly ITypedCache<int, Dictionary<string, SettingInfo>> _tenantSettingCache;
        private readonly ITypedCache<long, Dictionary<string, SettingInfo>> _userSettingCache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="settingDefinitionManager">设置定义管理类</param>
        /// <param name="cacheManager">缓存挂历类</param>
        /// <inheritdoc/>
        public SettingManager(ISettingDefinitionManager settingDefinitionManager, ICacheManager cacheManager)
        {
            _settingDefinitionManager = settingDefinitionManager;

            AbpSession = NullAbpSession.Instance;
            SettingStore = DefaultConfigSettingStore.Instance;

            _applicationSettingCache = cacheManager.GetApplicationSettingsCache();
            _tenantSettingCache = cacheManager.GetTenantSettingsCache();
            _userSettingCache = cacheManager.GetUserSettingsCache();
        }

        #region Public methods

        /// <summary>
        /// 获取当前设置值-异步
        /// </summary>
        /// <param name="name">设置名称</param>
        /// <inheritdoc/>
        public Task<string> GetSettingValueAsync(string name)
        {
            return GetSettingValueInternalAsync(name, AbpSession.TenantId, AbpSession.UserId);
        }

        /// <summary>
        /// 获取应用程序级别的设置的当前值-异步
        /// </summary>
        /// <param name="name">设置名称</param>
        /// <returns></returns>
        public Task<string> GetSettingValueForApplicationAsync(string name)
        {
            return GetSettingValueInternalAsync(name);
        }

        /// <summary>
        /// 获取租户级别的设置的当前值-异步
        /// </summary>
        /// <param name="name">设置名称</param>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        public Task<string> GetSettingValueForTenantAsync(string name, int tenantId)
        {
            return GetSettingValueInternalAsync(name, tenantId);
        }

        /// <summary>
        /// 获取用户级别的设置值-异步
        /// </summary>
        /// <param name="name">设置名称</param>
        /// <param name="tenantId">租户Id<</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public Task<string> GetSettingValueForUserAsync(string name, int? tenantId, long userId)
        {
            return GetSettingValueInternalAsync(name, tenantId, userId);
        }

        /// <summary>
        /// 获取全部设置值-异步，
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync()
        {
            return await GetAllSettingValuesAsync(SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User);
        }

        /// <summary>
        /// 获取全部的设置值-异步
        /// </summary>
        /// <param name="scopes">设置范围</param>
        /// <inheritdoc/>
        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesAsync(SettingScopes scopes)
        {
            var settingDefinitions = new Dictionary<string, SettingDefinition>();
            var settingValues = new Dictionary<string, ISettingValue>();

            //Fill all setting with default values.
            //用默认值填充所有设置
            foreach (var setting in _settingDefinitionManager.GetAllSettingDefinitions())
            {
                settingDefinitions[setting.Name] = setting;
                settingValues[setting.Name] = new SettingValueObject(setting.Name, setting.DefaultValue);
            }

            //Overwrite application settings
            //重写应用程序设置
            if (scopes.HasFlag(SettingScopes.Application))
            {
                foreach (var settingValue in await GetAllSettingValuesForApplicationAsync())
                {
                    var setting = settingDefinitions.GetOrDefault(settingValue.Name);

                    //TODO: Conditions get complicated, try to simplify it
                    if (setting == null || !setting.Scopes.HasFlag(SettingScopes.Application))
                    {
                        continue;
                    }

                    if (!setting.IsInherited &&
                        ((setting.Scopes.HasFlag(SettingScopes.Tenant) && AbpSession.TenantId.HasValue) || (setting.Scopes.HasFlag(SettingScopes.User) && AbpSession.UserId.HasValue)))
                    {
                        continue;
                    }

                    settingValues[settingValue.Name] = new SettingValueObject(settingValue.Name, settingValue.Value);
                }
            }

            //Overwrite tenant settings
            //重写租户设置
            if (scopes.HasFlag(SettingScopes.Tenant) && AbpSession.TenantId.HasValue)
            {
                foreach (var settingValue in await GetAllSettingValuesForTenantAsync(AbpSession.TenantId.Value))
                {
                    var setting = settingDefinitions.GetOrDefault(settingValue.Name);

                    //TODO: Conditions get complicated, try to simplify it
                    if (setting == null || !setting.Scopes.HasFlag(SettingScopes.Tenant))
                    {
                        continue;
                    }

                    if (!setting.IsInherited &&
                        (setting.Scopes.HasFlag(SettingScopes.User) && AbpSession.UserId.HasValue))
                    {
                        continue;
                    }

                    settingValues[settingValue.Name] = new SettingValueObject(settingValue.Name, settingValue.Value);
                }
            }

            //Overwrite user settings
            //重写用户设置
            if (scopes.HasFlag(SettingScopes.User) && AbpSession.UserId.HasValue)
            {
                foreach (var settingValue in await GetAllSettingValuesForUserAsync(AbpSession.UserId.Value))
                {
                    var setting = settingDefinitions.GetOrDefault(settingValue.Name);
                    if (setting != null && setting.Scopes.HasFlag(SettingScopes.User))
                    {
                        settingValues[settingValue.Name] = new SettingValueObject(settingValue.Name, settingValue.Value);
                    }
                }
            }

            return settingValues.Values.ToImmutableList();
        }

        /// <summary>
        /// 获取应用程序的全部设置值列表-异步
        /// </summary>
        /// <inheritdoc/>
        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForApplicationAsync()
        {
            return (await GetApplicationSettingsAsync()).Values
                .Select(setting => new SettingValueObject(setting.Name, setting.Value))
                .ToImmutableList();
        }

        /// <summary>
        /// 获取给定租户的全部设置值列表-异步，
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <inheritdoc/>
        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForTenantAsync(int tenantId)
        {
            return (await GetReadOnlyTenantSettings(tenantId)).Values
                .Select(setting => new SettingValueObject(setting.Name, setting.Value))
                .ToImmutableList();
        }

        /// <summary>
        /// 获取给定用户的全部设置值列表-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <inheritdoc/>
        public async Task<IReadOnlyList<ISettingValue>> GetAllSettingValuesForUserAsync(long userId)
        {
            return (await GetReadOnlyUserSettings(userId)).Values
                .Select(setting => new SettingValueObject(setting.Name, setting.Value))
                .ToImmutableList();
        }

        /// <summary>
        /// 应用程序级别更改设置值
        /// </summary>
        /// <param name="name">设置名称</param>
        /// <param name="value">设置值</param>
        /// <inheritdoc/>
        [UnitOfWork]
        public virtual async Task ChangeSettingForApplicationAsync(string name, string value)
        {
            await InsertOrUpdateOrDeleteSettingValueAsync(name, value, null, null);
            await _applicationSettingCache.RemoveAsync(ApplicationSettingsCacheKey);
        }

        /// <summary>
        /// 租户更改设置值
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <param name="name">设置名称</param>
        /// <param name="value">设置值</param>
        /// <inheritdoc/>
        [UnitOfWork]
        public virtual async Task ChangeSettingForTenantAsync(int tenantId, string name, string value)
        {
            await InsertOrUpdateOrDeleteSettingValueAsync(name, value, tenantId, null);
            await _tenantSettingCache.RemoveAsync(tenantId);
        }

        /// <summary> 
        /// 用户更改设置值
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="name">设置名称</param>
        /// <param name="value">设置值</param>
        /// <inheritdoc/>
        [UnitOfWork]
        public virtual async Task ChangeSettingForUserAsync(long userId, string name, string value)
        {
            await InsertOrUpdateOrDeleteSettingValueAsync(name, value, null, userId);
            await _userSettingCache.RemoveAsync(userId);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// 获取程序集设置值-异步
        /// </summary>
        /// <param name="name">设置名称</param>
        /// <param name="tenantId">租户Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        private async Task<string> GetSettingValueInternalAsync(string name, int? tenantId = null, long? userId = null)
        {
            var settingDefinition = _settingDefinitionManager.GetSettingDefinition(name);

            //Get for user if defined
            //如果设置范围为用户
            if (settingDefinition.Scopes.HasFlag(SettingScopes.User) && userId.HasValue)
            {
                var settingValue = await GetSettingValueForUserOrNullAsync(userId.Value, name);
                if (settingValue != null)
                {
                    return settingValue.Value;
                }

                if (!settingDefinition.IsInherited)
                {
                    return settingDefinition.DefaultValue;
                }
            }

            //Get for tenant if defined
            //如果设置范围为租户
            if (settingDefinition.Scopes.HasFlag(SettingScopes.Tenant) && tenantId.HasValue)
            {
                var settingValue = await GetSettingValueForTenantOrNullAsync(tenantId.Value, name);
                if (settingValue != null)
                {
                    return settingValue.Value;
                }

                if (!settingDefinition.IsInherited)
                {
                    return settingDefinition.DefaultValue;
                }
            }

            //Get for application if defined
            //如果设置范围为应用程序
            if (settingDefinition.Scopes.HasFlag(SettingScopes.Application))
            {
                var settingValue = await GetSettingValueForApplicationOrNullAsync(name);
                if (settingValue != null)
                {
                    return settingValue.Value;
                }
            }

            //Not defined, get default value
            //未定义，返回摩恩之
            return settingDefinition.DefaultValue;
        }

        /// <summary>
        /// 插入或更新或删除设置值-异步
        /// </summary>
        /// <param name="name">设置名称</param>
        /// <param name="value"></param>
        /// <param name="tenantId">租户Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        private async Task<SettingInfo> InsertOrUpdateOrDeleteSettingValueAsync(string name, string value, int? tenantId, long? userId)
        {
            if (tenantId.HasValue && userId.HasValue)
            {
                //租户Id和用户Id不能同时设置
                throw new ApplicationException("Both of tenantId and userId can not be set!");
            }

            var settingDefinition = _settingDefinitionManager.GetSettingDefinition(name);
            var settingValue = await SettingStore.GetSettingOrNullAsync(tenantId, userId, name);

            //Determine defaultValue
            //确定默认值
            var defaultValue = settingDefinition.DefaultValue;

            if (settingDefinition.IsInherited)
            {
                //For Tenant and User, Application's value overrides Setting Definition's default value.
                if (tenantId.HasValue || userId.HasValue)
                {
                    var applicationValue = await GetSettingValueForApplicationOrNullAsync(name);
                    if (applicationValue != null)
                    {
                        defaultValue = applicationValue.Value;
                    }
                }

                //For User, Tenants's value overrides Application's default value.
                if (userId.HasValue && AbpSession.TenantId.HasValue)
                {
                    var tenantValue = await GetSettingValueForTenantOrNullAsync(AbpSession.TenantId.Value, name);
                    if (tenantValue != null)
                    {
                        defaultValue = tenantValue.Value;
                    }
                }
            }

            //No need to store on database if the value is the default value
            if (value == defaultValue)
            {
                if (settingValue != null)
                {
                    await SettingStore.DeleteAsync(settingValue);
                }

                return null;
            }

            //If it's not default value and not stored on database, then insert it
            if (settingValue == null)
            {
                settingValue = new SettingInfo
                {
                    TenantId = tenantId,
                    UserId = userId,
                    Name = name,
                    Value = value
                };

                await SettingStore.CreateAsync(settingValue);
                return settingValue;
            }

            //It's same value in database, no need to update
            if (settingValue.Value == value)
            {
                return settingValue;
            }

            //Update the setting on database.
            settingValue.Value = value;
            await SettingStore.UpdateAsync(settingValue);

            return settingValue;
        }

        /// <summary>
        /// 获取给定设置名称的应用程序的设置值-异步
        /// </summary>
        /// <param name="name">设置名称</param>
        /// <returns></returns>
        private async Task<SettingInfo> GetSettingValueForApplicationOrNullAsync(string name)
        {
            return (await GetApplicationSettingsAsync()).GetOrDefault(name);
        }

        /// <summary>
        /// 获取租户的设置值-异步
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <param name="name">设置名称</param>
        /// <returns></returns>
        private async Task<SettingInfo> GetSettingValueForTenantOrNullAsync(int tenantId, string name)
        {
            return (await GetReadOnlyTenantSettings(tenantId)).GetOrDefault(name);
        }

        /// <summary>
        /// 获取用户的设置值-异步
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="name">设置名称</param>
        /// <returns></returns>
        private async Task<SettingInfo> GetSettingValueForUserOrNullAsync(long userId, string name)
        {
            return (await GetReadOnlyUserSettings(userId)).GetOrDefault(name);
        }

        /// <summary>
        /// 获取应用程序设置-异步
        /// </summary>
        /// <returns></returns>
        private async Task<Dictionary<string, SettingInfo>> GetApplicationSettingsAsync()
        {
            return await _applicationSettingCache.GetAsync(ApplicationSettingsCacheKey, async () =>
            {
                var dictionary = new Dictionary<string, SettingInfo>();

                var settingValues = await SettingStore.GetAllListAsync(null, null);
                foreach (var settingValue in settingValues)
                {
                    dictionary[settingValue.Name] = settingValue;
                }

                return dictionary;
            });
        }

        /// <summary>
        /// 获取给定租户的设置
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        private async Task<ImmutableDictionary<string, SettingInfo>> GetReadOnlyTenantSettings(int tenantId)
        {
            var cachedDictionary = await GetTenantSettingsFromCache(tenantId);
            lock (cachedDictionary)
            {
                return cachedDictionary.ToImmutableDictionary();
            }
        }

        /// <summary>
        /// 获取给定用户的设置
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        private async Task<ImmutableDictionary<string, SettingInfo>> GetReadOnlyUserSettings(long userId)
        {
            var cachedDictionary = await GetUserSettingsFromCache(userId);
            lock (cachedDictionary)
            {
                return cachedDictionary.ToImmutableDictionary();
            }
        }

        /// <summary>
        /// 获取给定租户的设置
        /// </summary>
        /// <param name="tenantId">租户Id</param>
        /// <returns></returns>
        private async Task<Dictionary<string, SettingInfo>> GetTenantSettingsFromCache(int tenantId)
        {
            return await _tenantSettingCache.GetAsync(
                tenantId,
                async () =>
                {
                    var dictionary = new Dictionary<string, SettingInfo>();

                    var settingValues = await SettingStore.GetAllListAsync(tenantId, null);
                    foreach (var settingValue in settingValues)
                    {
                        dictionary[settingValue.Name] = settingValue;
                    }

                    return dictionary;
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        private async Task<Dictionary<string, SettingInfo>> GetUserSettingsFromCache(long userId)
        {
            return await _userSettingCache.GetAsync(
                userId,
                async () =>
                {
                    var dictionary = new Dictionary<string, SettingInfo>();

                    var settingValues = await SettingStore.GetAllListAsync(null, userId);
                    foreach (var settingValue in settingValues)
                    {
                        dictionary[settingValue.Name] = settingValue;
                    }

                    return dictionary;
                });
        }

        #endregion

        #region Nested classes

        /// <summary>
        /// 设置值对象
        /// </summary>
        private class SettingValueObject : ISettingValue
        {
            /// <summary>
            /// 设置名称
            /// </summary>
            public string Name { get; private set; }

            /// <summary>
            /// 设置值
            /// </summary>
            public string Value { get; private set; }

            /// <summary>
            /// 设置值对象
            /// </summary>
            /// <param name="name">设置名称</param>
            /// <param name="value">设置值</param>
            public SettingValueObject(string name, string value)
            {
                Value = value;
                Name = name;
            }
        }

        #endregion
    }
}
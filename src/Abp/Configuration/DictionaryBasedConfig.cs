using System;
using System.Collections.Generic;
using Abp.Collections.Extensions;

namespace Abp.Configuration
{
    /// <summary>
    /// Used to set/get custom configuration.
    /// 字典基础配置
    /// </summary>
    public class DictionaryBasedConfig : IDictionaryBasedConfig
    {
        /// <summary>
        /// Dictionary of custom configuration.
        /// 自定义配置字典
        /// </summary>
        protected Dictionary<string, object> CustomSettings { get; private set; }

        /// <summary>
        /// Gets/sets a config value.
        /// Returns null if no config with given name.
        /// 获取/设置配置值。
        /// 如果未找到对象返回null
        /// </summary>
        /// <param name="name">Name of the config 配置名称</param>
        /// <returns>Value of the config 配置值</returns>
        public object this[string name]
        {
            get { return CustomSettings.GetOrDefault(name); }
            set { CustomSettings[name] = value; }
        }

        /// <summary>
        /// Constructor.
        /// 构造函数
        /// </summary>
        protected DictionaryBasedConfig()
        {
            CustomSettings = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets a configuration value as a specific type.
        /// 根据配置名称获取配置值
        /// </summary>
        /// <param name="name">Name of the config 配置名称</param>
        /// <typeparam name="T">Type of the config 配置类型</typeparam>
        /// <returns>Value of the configuration or null if not found  配置值或未找到返回null</returns>
        public T Get<T>(string name)
        {
            var value = this[name];
            return value == null
                ? default(T)
                : (T) Convert.ChangeType(value, typeof (T));
        }

        /// <summary>
        /// Used to set a string named configuration.
        /// If there is already a configuration with same <paramref name="name"/>, it's overwritten.
        /// 设置配置
        /// </summary>
        /// <param name="name">Unique name of the configuration 配置名称</param>
        /// <param name="value">Value of the configuration 配置值</param>
        public void Set<T>(string name, T value)
        {
            this[name] = value;
        }

        /// <summary>
        /// Gets a configuration object with given name.
        /// 根据配置名称获取配置值
        /// </summary>
        /// <param name="name">Unique name of the configuration 配置名称</param>
        /// <returns>Value of the configuration or null if not found  配置值或未找到返回null</returns>
        public object Get(string name)
        {
            return Get(name, null);
        }

        /// <summary>
        /// Gets a configuration object with given name.
        /// 根据配置名称获取配置值
        /// </summary>
        /// <param name="name">Unique name of the configuration 配置名称</param>
        /// <param name="defaultValue">Default value of the object if can not found given configuration</param>
        /// <returns>Value of the configuration or null if not found</returns>
        public object Get(string name, object defaultValue)
        {
            var value = this[name];
            if (value == null)
            {
                return defaultValue;
            }

            return this[name];
        }

        /// <summary>
        /// Gets a configuration object with given name.
        /// 根据配置名称获取配置值
        /// </summary>
        /// <typeparam name="T">Type of the object 类型</typeparam>
        /// <param name="name">Unique name of the configuration 配置名称</param>
        /// <param name="defaultValue">Default value of the object if can not found given configuration</param>
        /// <returns>Value of the configuration or null if not found</returns>
        public T Get<T>(string name, T defaultValue)
        {
            return (T)Get(name, (object)defaultValue);
        }

        /// <summary>
        /// Gets a configuration object with given name.
        /// 根据配置名称获取配置值
        /// </summary>
        /// <typeparam name="T">Type of the object 类型</typeparam>
        /// <param name="name">Unique name of the configuration 配置名称</param>
        /// <param name="creator">The function that will be called to create if given configuration is not found</param>
        /// <returns>Value of the configuration or null if not found</returns>
        public T GetOrCreate<T>(string name, Func<T> creator)
        {
            var value = Get(name);
            if (value == null)
            {
                value = creator();
                Set(name, value);
            }
            return (T) value;
        }
    }
}
using System;

namespace Abp.Configuration
{
    /// <summary>
    /// Defines interface to use a dictionary to make configurations.
    /// 字典基础配置接口 
    /// </summary>
    public interface IDictionaryBasedConfig
    {
        /// <summary>
        /// Used to set a string named configuration.
        /// If there is already a configuration with same <paramref name="name"/>, it's overwritten.
        /// 设置配置
        /// </summary>
        /// <param name="name">Unique name of the configuration 配置名称</param>
        /// <param name="value">Value of the configuration 配置值</param>
        /// <returns>Returns the passed <paramref name="value"/></returns>
        void Set<T>(string name, T value);

        /// <summary>
        /// Gets a configuration object with given name.
        /// 获取给定名称的配置对象。
        /// </summary>
        /// <param name="name">Unique name of the configuration 配置名称</param>
        /// <returns>Value of the configuration or null if not found 配置值或未找到返回null</returns>
        object Get(string name);

        /// <summary>
        /// Gets a configuration object with given name.
        /// 获取给定名称的配置对象。
        /// </summary>
        /// <typeparam name="T">Type of the object 类型</typeparam>
        /// <param name="name">Unique name of the configuration 配置名称</param>
        /// <returns>Value of the configuration or null if not found 配置值或未找到返回null</returns>
        T Get<T>(string name);

        /// <summary>
        /// Gets a configuration object with given name.
        /// 获取给定名称的配置对象。
        /// </summary>
        /// <param name="name">Unique name of the configuration 配置名称</param>
        /// <param name="defaultValue">Default value of the object if can not found given configuration</param>
        /// <returns>Value of the configuration or null if not found</returns>
        object Get(string name, object defaultValue);

        /// <summary>
        /// Gets a configuration object with given name.
        /// 获取给定名称的配置对象。
        /// </summary>
        /// <typeparam name="T">Type of the object 类型</typeparam>
        /// <param name="name">Unique name of the configuration 配置名称</param>
        /// <param name="defaultValue">Default value of the object if can not found given configuration</param>
        /// <returns>Value of the configuration or null if not found</returns>
        T Get<T>(string name, T defaultValue);

        /// <summary>
        /// Gets a configuration object with given name.
        /// 获取给定名称的配置对象。
        /// </summary>
        /// <typeparam name="T">Type of the object 类型</typeparam>
        /// <param name="name">Unique name of the configuration 配置名称</param>
        /// <param name="creator">The function that will be called to create if given configuration is not found</param>
        /// <returns>Value of the configuration or null if not found</returns>
        T GetOrCreate<T>(string name, Func<T> creator);
    }
}
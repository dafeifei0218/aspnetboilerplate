using System.Collections.Generic;

namespace Abp.Configuration
{
    /// <summary>
    /// Defines setting definition manager.
    /// 设置定义管理类接口
    /// </summary>
    public interface ISettingDefinitionManager
    {
        /// <summary>
        /// Gets the <see cref="SettingDefinition"/> object with given unique name.
        /// Throws exception if can not find the setting.
        /// 获取设置定义
        /// </summary>
        /// <param name="name">Unique name of the setting 设置名称</param>
        /// <returns>The <see cref="SettingDefinition"/> object. 返回设置定义对象</returns>
        SettingDefinition GetSettingDefinition(string name);

        /// <summary>
        /// Gets a list of all setting definitions.
        /// 获取全部设置定义
        /// </summary>
        /// <returns>All settings.返回全部设置定义对象列表</returns>
        IReadOnlyList<SettingDefinition> GetAllSettingDefinitions();
    }
}

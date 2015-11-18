using System.Collections.Generic;

namespace Abp.Domain.Uow
{
    /// <summary>
    /// 数据过滤器配置
    /// </summary>
    public class DataFilterConfiguration
    {
        /// <summary>
        /// 过滤器名称
        /// </summary>
        public string FilterName { get; private set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled { get; private set; }

        /// <summary>
        /// 过滤器参数集合
        /// </summary>
        public IDictionary<string, object> FilterParameters { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filterName">过滤器名称</param>
        /// <param name="isEnabled">是否启用</param>
        public DataFilterConfiguration(string filterName, bool isEnabled)
        {
            FilterName = filterName;
            IsEnabled = isEnabled;
            FilterParameters = new Dictionary<string, object>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filterToClone">数据过滤器配置</param>
        internal DataFilterConfiguration(DataFilterConfiguration filterToClone)
            : this(filterToClone.FilterName, filterToClone.IsEnabled)
        {
            foreach (var filterParameter in filterToClone.FilterParameters)
            {
                FilterParameters[filterParameter.Key] = filterParameter.Value;
            }
        }
    }
}
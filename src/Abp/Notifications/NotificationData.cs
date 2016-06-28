using System;
using System.Collections.Generic;
using Abp.Json;

namespace Abp.Notifications
{
    /// <summary>
    /// Used to store data for a notification.
    /// It can be directly used or can be derived.
    /// 通知数据，
    /// 用于存储通知的数据。
    /// 可直接使用或可衍生。
    /// </summary>
    /// <remarks>
    /// 用于储存真正的Notification的数据(即内容)
    /// </remarks>
    [Serializable]
    public class NotificationData
    {
        /// <summary>
        /// Gets notification data type name.
        /// It returns the full class name by default.
        /// 类型，
        /// 获取通知数据类型名称。
        /// 默认情况下，返回全类名称。
        /// </summary>
        public virtual string Type
        {
            get { return GetType().FullName; }
        }

        /// <summary>
        /// Shortcut to set/get <see cref="Properties"/>.
        /// 获取/设置属性
        /// </summary>
        public object this[string key]
        {
            get { return Properties[key]; }
            set { Properties[key] = value; }
        }

        /// <summary>
        /// Can be used to add custom properties to this notification.
        /// 属性字典，
        /// 可用于此通知中添加自定义属性。
        /// </summary>
        public Dictionary<string, object> Properties
        {
            get { return _properties; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _properties = value;
            }
        }
        private Dictionary<string, object> _properties;

        /// <summary>
        /// Createa a new NotificationData object.
        /// 创建一个通知数据对象
        /// </summary>
        public NotificationData()
        {
            Properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// 重写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToJsonString();
        }
    }
}
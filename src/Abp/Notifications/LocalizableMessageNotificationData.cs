using System;
using Abp.Localization;

namespace Abp.Notifications
{
    /// <summary>
    /// Can be used to store a simple message as notification data.
    /// 本地消息通知数据，可用于将一个简单的消息存储为通知数据。
    /// </summary>
    [Serializable]
    public class LocalizableMessageNotificationData : NotificationData
    {
        /// <summary>
        /// The message.
        /// 消息。
        /// </summary>
        public LocalizableString Message { get; private set; }

        /// <summary>
        /// Needed for serialization.
        /// 序列化所需。
        /// </summary>
        private LocalizableMessageNotificationData()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalizableMessageNotificationData"/> class.
        /// 初始化一个新的<see cref="LocalizableMessageNotificationData"/>类
        /// </summary>
        /// <param name="message">The message. 消息</param>
        public LocalizableMessageNotificationData(LocalizableString message)
        {
            Message = message;
        }
    }
}
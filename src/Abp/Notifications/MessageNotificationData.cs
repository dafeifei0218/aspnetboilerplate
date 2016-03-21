using System;

namespace Abp.Notifications
{
    /// <summary>
    /// Can be used to store a simple message as notification data.
    /// 消息通知数据，可用于将一个简单的消息存储为通知数据。
    /// </summary>
    [Serializable]
    public class MessageNotificationData : NotificationData
    {
        /// <summary>
        /// The message.
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Needed for serialization.
        /// 序列化所需。
        /// </summary>
        private MessageNotificationData()
        {
            
        }

        public MessageNotificationData(string message)
        {
            Message = message;
        }
    }
}
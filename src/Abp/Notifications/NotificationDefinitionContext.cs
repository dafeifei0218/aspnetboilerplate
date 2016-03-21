namespace Abp.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    internal class NotificationDefinitionContext : INotificationDefinitionContext
    {
        /// <summary>
        /// 
        /// </summary>
        public INotificationDefinitionManager Manager { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        public NotificationDefinitionContext(INotificationDefinitionManager manager)
        {
            Manager = manager;
        }
    }
}
using System;

namespace Plugin.Toast
{
    /// <summary>
    /// Notification event observer. You can subscribe to receive events using <see cref="ISystemEventSource"/>
    /// </summary>
    public interface INotificationEventObserver
    {
        /// <summary>
        /// The method that is called when <see cref="NotificationEvent"/> is occurs.
        /// </summary>
        void OnNotificationReceived(NotificationEvent @event);
    }
}

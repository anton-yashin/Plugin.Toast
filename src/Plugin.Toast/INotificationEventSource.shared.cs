using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// An object that will dispatch an event to you when user taps the notification.
    /// </summary>
    public interface INotificationEventSource
    {
        /// <summary>
        /// Send events registered via <see cref="Platform"/> class
        /// </summary>
        void SendPendingEvents();
        /// <summary>
        /// Notification received event handler.
        /// </summary>
        event EventHandler<NotificationEvent> NotificationReceived;
    }
}

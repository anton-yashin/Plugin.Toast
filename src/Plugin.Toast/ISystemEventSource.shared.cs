using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Event source, that distributes notification events from OS to observers
    /// </summary>
    public interface ISystemEventSource
    {
        /// <summary>
        /// Send events received at startup to observers.
        /// </summary>
        void SendPendingEvents();
        /// <summary>
        /// Sends a event to observers.
        /// </summary>
        /// <param name="event">Event</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        void SendEvent(NotificationEvent @event);
        /// <summary>
        /// Stores a weak reference to observer, to deliver events later.
        /// </summary>
        /// <param name="observer">Observer</param>
        void Subscribe(INotificationEventObserver observer);
        /// <summary>
        /// Removes a weak reference to observer.
        /// </summary>
        /// <param name="observer">Observer</param>
        void Unsubscribe(INotificationEventObserver observer);
    }
}

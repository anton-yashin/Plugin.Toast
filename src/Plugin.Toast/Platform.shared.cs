using System;
using System.Collections.Generic;
using System.Linq;

namespace Plugin.Toast
{
    /// <summary>
    /// Store for notification events received at startup
    /// </summary>
    public static partial class Platform
    {
        internal static object Lock { get; } = new object();

        static List<NotificationEvent>? pendingEvents;

        /// <summary>
        /// List of pending event received at startup. May be empty.
        /// </summary>
        public static IEnumerable<NotificationEvent> PendingEvents
        { 
            get
            {
                lock (Lock)
                {
                    return pendingEvents == null ? Enumerable.Empty<NotificationEvent>() : new List<NotificationEvent>(pendingEvents);
                }
            }
        }

        /// <summary>
        /// An <see cref="ISystemEventSource"/> that will send you <see cref="PendingEvents"/>
        /// after you call <see cref="ISystemEventSource.SendPendingEvents"/>.
        /// </summary>
        public static ISystemEventSource? SystemEventSource
        {
            get => _systemEventRouter;
            set
            {
                lock (Lock) { _systemEventRouter = value; }
            }
        }
        static ISystemEventSource? _systemEventRouter;

        internal static void AddPendingEvent(NotificationEvent @event)
        {
            lock (Lock)
            {
                List<NotificationEvent> events;
                if (pendingEvents == null)
                    pendingEvents = new List<NotificationEvent>();
                events = pendingEvents;
                events.Add(@event);
            }
        }

        /// <summary>
        /// Clears the <see cref="PendingEvents"/> list.
        /// </summary>
        public static void ClearPendingEvents()
        {
            lock (Lock)
            {
                pendingEvents = null;
            }
        }
    }
}

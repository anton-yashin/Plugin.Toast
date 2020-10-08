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
        static object @lock = new object();

        static List<NotificationEvent>? pendingEvents;
        public static IEnumerable<NotificationEvent> PendingEvents
        { 
            get
            {
                lock (@lock)
                {
                    return pendingEvents == null ? Enumerable.Empty<NotificationEvent>() : new List<NotificationEvent>(pendingEvents);
                }
            }
        }

        public static ISystemEventSource? SystemEventSource
        {
            get => _systemEventRouter;
            set
            {
                lock (@lock) { _systemEventRouter = value; }
            }
        }
        static ISystemEventSource? _systemEventRouter;

        static void AddPendingEvent(NotificationEvent @event)
        {
            lock (@lock)
            {
                List<NotificationEvent> events;
                if (pendingEvents == null)
                    pendingEvents = new List<NotificationEvent>();
                events = pendingEvents;
                events.Add(@event);
            }
        }

        public static void ClearPendingEvents()
        {
            lock (@lock)
            {
                pendingEvents = null;
            }
        }
    }
}

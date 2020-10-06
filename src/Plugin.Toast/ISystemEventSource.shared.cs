using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface ISystemEventSource
    {
        void SendPendingEvents();
        void SendEvent(NotificationEvent @event);
        void Subscribe(INotificationEventObserver observer);
    }
}

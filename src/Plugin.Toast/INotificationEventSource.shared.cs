using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface INotificationEventSource
    {
        void SendPendingEvents();

        event EventHandler<NotificationEvent> NotificationReceived;
    }
}

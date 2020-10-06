using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface INotificationEventObserver
    {
        void OnNotificationReceived(NotificationEvent @event);
    }
}

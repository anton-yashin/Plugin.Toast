using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface IActivator
    {
        void OnSystemEvent(object args);

        event EventHandler<NotificationEvent> NotificationEvent;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using UserNotifications;

namespace Plugin.Toast
{
    public interface IToastOptions
    {
        UNNotificationSound? Sound { get; }
        bool MultipleAuthorizationRequests { get; }
    }
}

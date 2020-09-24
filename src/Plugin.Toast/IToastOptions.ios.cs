using System;
using System.Collections.Generic;
using System.Text;
using UserNotifications;

namespace Plugin.Toast
{
    public interface IToastOptions
    {
        UNNotificationSound? Sound { get; }
        [Obsolete("not supported by OS", true)]
        bool MultipleAuthorizationRequests { get; }
    }
}

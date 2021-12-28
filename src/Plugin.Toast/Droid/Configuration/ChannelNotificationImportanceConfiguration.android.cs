using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Droid.Configuration
{
    interface IChannelNotificationImportanceConfiguration
    {
        DroidNotificationImportance NotificationImportance { get; }
    }
}

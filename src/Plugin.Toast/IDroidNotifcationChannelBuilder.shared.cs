using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface IDroidNotifcationChannelBuilder
    {
        IDroidNotifcationChannelBuilder SetId(string id);
        IDroidNotifcationChannelBuilder SetName(string name);
        IDroidNotifcationChannelBuilder SetImportance(NotificationImportance notificationImportance);
        IDroidNotifcationChannelBuilder SetGroup(string group);
        IDroidNotifcationChannelBuilder EnableLights(bool lights);
        IDroidNotifcationChannelBuilder EnableVibration(bool vibration);
        IDroidNotifcationChannelBuilder SetAllowBubbles(bool allowBubbles);
        IDroidNotifcationChannelBuilder SetBypassDnd(bool bypassDnd);
        IDroidNotifcationChannelBuilder SetShowBadge(bool showBadge);
        IDroidNotifcationChannelBuilder SetVibrationPattern(long[] vibrationPattern);
        IDroidNotifcationChannelBuilder SetDescription(string description);
    }

    /// <summary>
    /// <see cref="global::Android.App.NotificationImportance"/>
    /// </summary>
    public enum NotificationImportance
    {
        Unspecified = -1000,
        None = 0,
        Min = 1,
        Low = 2,
        Default = 3,
        High = 4,
        Max = 5
    }
}

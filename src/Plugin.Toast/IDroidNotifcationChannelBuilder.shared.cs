using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Used to setup channel for android notification using <see cref="global::Android.App.NotificationChannel"/>
    /// </summary>
    public interface IDroidNotifcationChannelBuilder
    {
        /// <summary>
        /// Set custom id for channel. If not set it will be generated from name.
        /// </summary>
        /// <param name="id">Channel id</param>
        /// <returns>This object</returns>
        IDroidNotifcationChannelBuilder SetId(string id);
        /// <summary>
        /// Set name of channel. This call is mandatory. Builder will throw
        /// <see cref="InvalidOperationException"/> if call is omitted.
        /// </summary>
        /// <param name="name">Channel name</param>
        /// <returns>This object</returns>
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

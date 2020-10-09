using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Used to setup channel for android notification using <see cref="global::Android.App.NotificationChannel"/>
    /// <seealso cref="https://developer.android.com/reference/android/app/NotificationChannel"/>
    /// </summary>
    public interface IDroidNotifcationChannelBuilder
    {
        /// <summary>
        /// Set custom id for channel. If not set it will be generated from name.
        /// </summary>
        /// <param name="id">Channel id</param>
        /// <returns>Notification channel builder</returns>
        IDroidNotifcationChannelBuilder SetId(string id);
        /// <summary>
        /// Set name of channel. This call is mandatory. Builder will throw
        /// <see cref="InvalidOperationException"/> if call is omitted.
        /// </summary>
        /// <param name="name">Channel name</param>
        /// <returns>Notification channel builder</returns>
        IDroidNotifcationChannelBuilder SetName(string name);
        /// <summary>
        /// <see cref="global::Android.App.NotificationChannel.Importance"/>
        /// <see cref="global::Android.App.NotificationChannel.NotificationChannel(string?, string?, Android.App.NotificationImportance)"/>
        /// </summary>
        /// <param name="notificationImportance">Notification importance</param>
        /// <returns>Notification channel builder</returns>
        IDroidNotifcationChannelBuilder SetImportance(NotificationImportance notificationImportance);
        /// <summary>
        /// <see cref="global::Android.App.NotificationChannel.Group"/>
        /// </summary>
        /// <param name="group">Group name</param>
        /// <returns>Notification channel builder</returns>
        IDroidNotifcationChannelBuilder SetGroup(string group);
        /// <summary>
        /// <see cref="global::Android.App.NotificationChannel.EnableLights(bool)"/>
        /// </summary>
        /// <param name="lights">lights</param>
        /// <returns>Notification channel builder</returns>
        IDroidNotifcationChannelBuilder EnableLights(bool lights);
        /// <summary>
        /// <see cref="global::Android.App.NotificationChannel.EnableVibration(bool)"/>
        /// </summary>
        /// <param name="vibration">vibration</param>
        /// <returns>Notification channel builder</returns>
        IDroidNotifcationChannelBuilder EnableVibration(bool vibration);
        /// <summary>
        /// <see cref="global::Android.App.NotificationChannel.SetAllowBubbles(bool)"/>
        /// Android 10 is required (API 29), otherwise ignored.
        /// </summary>
        /// <param name="allowBubbles">Allow bubbles</param>
        /// <returns>Notification channel builder</returns>
        IDroidNotifcationChannelBuilder SetAllowBubbles(bool allowBubbles);
        /// <summary>
        /// <see cref="global::Android.App.NotificationChannel.SetBypassDnd(bool)"/>
        /// </summary>
        /// <param name="bypassDnd">Bypass Do Not Distrub</param>
        /// <returns>Notification channel builder</returns>
        IDroidNotifcationChannelBuilder SetBypassDnd(bool bypassDnd);
        /// <summary>
        /// <see cref="global::Android.App.NotificationChannel.SetShowBadge(bool)"/>
        /// </summary>
        /// <param name="showBadge">Show badge</param>
        /// <returns>Notification channel builder</returns>
        IDroidNotifcationChannelBuilder SetShowBadge(bool showBadge);
        /// <summary>
        /// <see cref="global::Android.App.NotificationChannel.SetVibrationPattern(long[]?)"/>
        /// </summary>
        /// <param name="vibrationPattern">Vibration pattern</param>
        /// <returns>Notification channel builder</returns>
        IDroidNotifcationChannelBuilder SetVibrationPattern(long[] vibrationPattern);
        /// <summary>
        /// <see cref="global::Android.App.NotificationChannel.Description"/>
        /// </summary>
        /// <param name="description">Description </param>
        /// <returns>Notification channel builder</returns>
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

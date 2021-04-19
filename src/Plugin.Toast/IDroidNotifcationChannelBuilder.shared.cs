using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
#if __ANDROID__ == false
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
#endif
    /// <summary>
    /// Used to setup channel for android notification using <see cref="global::Android.App.NotificationChannel"/>
    /// <seealso href="https://developer.android.com/reference/android/app/NotificationChannel"/>
    /// </summary>
    public interface IDroidNotifcationChannelBuilder
    {
#if __ANDROID__ == false
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
#endif
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
        /// Sets the level of interruption of this notification channel.
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationChannel#setImportance(int)"/>
        /// </summary>
        /// <param name="notificationImportance">Notification importance</param>
        /// <returns>Notification channel builder</returns>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotifcationChannelBuilder SetImportance(DroidNotificationImportance notificationImportance);
        /// <summary>
        /// Sets what group this channel belongs to. Group information is only used for presentation, not for behavior.
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationChannel#setGroup(java.lang.String)"/>
        /// </summary>
        /// <param name="group">Group name</param>
        /// <returns>Notification channel builder</returns>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotifcationChannelBuilder SetGroup(string group);
        /// <summary>
        /// Sets whether notifications posted to this channel should display notification lights, on devices that support that feature.
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationChannel#enableLights(boolean)"/>
        /// </summary>
        /// <param name="lights">lights</param>
        /// <returns>Notification channel builder</returns>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotifcationChannelBuilder EnableLights(bool lights);
        /// <summary>
        /// Sets whether notification posted to this channel should vibrate.
        /// The vibration pattern can be set with <seealso cref="SetVibrationPattern(long[])"/>
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationChannel#enableVibration(boolean)"/>
        /// </summary>
        /// <param name="vibration">vibration</param>
        /// <returns>Notification channel builder</returns>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotifcationChannelBuilder EnableVibration(bool vibration);
        /// <summary>
        /// Android 10 is required (API 29), otherwise ignored.
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationChannel#setAllowBubbles(boolean)"/>
        /// </summary>
        /// <param name="allowBubbles">Allow bubbles</param>
        /// <returns>Notification channel builder</returns>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotifcationChannelBuilder SetAllowBubbles(bool allowBubbles);
        /// <summary>
        /// Sets whether or not notifications posted to this channel can interrupt the user in NotificationManager.INTERRUPTION_FILTER_PRIORITY mode. 
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationChannel#setBypassDnd(boolean)"/>
        /// </summary>
        /// <param name="bypassDnd">Bypass Do Not Distrub</param>
        /// <returns>Notification channel builder</returns>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotifcationChannelBuilder SetBypassDnd(bool bypassDnd);
        /// <summary>
        /// Sets whether notifications posted to this channel can appear as application icon badges in a Launcher. 
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationChannel#setShowBadge(boolean)"/>
        /// </summary>
        /// <param name="showBadge">Show badge</param>
        /// <returns>Notification channel builder</returns>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotifcationChannelBuilder SetShowBadge(bool showBadge);
        /// <summary>
        /// Sets the vibration pattern for notifications posted to this channel.
        /// If the provided pattern is valid (non-null, non-empty), will <see cref="EnableVibration(bool)"/>
        /// enable vibration as well. Otherwise, vibration will be disabled.
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationChannel#setVibrationPattern(long[])"/>
        /// </summary>
        /// <param name="vibrationPattern">Vibration pattern</param>
        /// <returns>Notification channel builder</returns>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotifcationChannelBuilder SetVibrationPattern(long[] vibrationPattern);
        /// <summary>
        /// Sets the user visible description of this channel. 
        /// <seealso href="https://developer.android.com/reference/android/app/NotificationChannel#setDescription(java.lang.String)"/>
        /// </summary>
        /// <param name="description">Description </param>
        /// <returns>Notification channel builder</returns>
        /// <remarks>The recommended maximum length is 300 characters; the value may be truncated if it is too long.
        /// <br/>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotifcationChannelBuilder SetDescription(string description);
    }
}


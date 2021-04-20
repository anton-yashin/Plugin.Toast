using Android.App;
using Android.Content;
using System;

namespace Plugin.Toast.Droid
{
    /// <summary>
    /// Interface of android notification builder.
    /// </summary>
    /// <remarks>
    /// WARNING: This is part of internal api. And may be changed at any time.
    /// </remarks>
    public interface IPlatformNotificationBuilder : IPlatformSpecificExtension
    {
        /// <summary>
        /// Add extra data to a intent using custom argumets
        /// </summary>
        /// <param name="intent">An intent to add an extra data</param>
        void AddCustomArgsTo(Intent intent);
        /// <summary>
        /// true if <see cref="IPlatformSpecificExtension.SetContentIntent(PendingIntent)"/> is called, otherwise is false
        /// </summary>
        bool UsingCustomContentIntent { get; }
        /// <summary>
        /// true if <see cref="IPlatformSpecificExtension.SetDeleteIntent(PendingIntent)"/> is called, otherwise is false
        /// </summary>
        bool UsingCustomDeleteIntent { get; }
        /// <summary>
        /// parameter of <see cref="IDroidNotificationExtension.ForceOpenAppOnNotificationTap(bool)"/>
        /// </summary>
        bool GetForceOpenAppOnNotificationTap();

        /// <summary>
        /// parameter of <see cref="IDroidNotificationExtension.SetCleanupOnTimeout(bool)"/>
        /// </summary>
        bool CleanupOnTimeout { get; }

        /// <summary>
        /// parameter of <see cref="IDroidNotificationExtension.SetTimeout(TimeSpan)"/>
        /// </summary>
        TimeSpan Timeout { get; }

        /// <summary>
        /// Builds android notification.
        /// </summary>
        /// <returns>Android noitification.</returns>
        global::Android.App.Notification Build();
    }
}

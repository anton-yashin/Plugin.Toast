using Android.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Droid
{
    /// <summary>
    /// Keys that passed to intents by this library.
    /// </summary>
    public sealed class IntentConstants
    {
        /// <summary>
        /// Id of notification. <seealso cref="ToastId.Id"/>
        /// </summary>
        public const string KNotificationId = nameof(KNotificationId);
        /// <summary>
        /// Tag of notification. <seealso cref="ToastId.Tag"/>
        /// </summary>
        public const string KNotifcationTag = nameof(KNotifcationTag);
        /// <summary>
        /// May contain <see cref="global::Android.App.Notification"/>
        /// </summary>
        public const string KNotification = nameof(KNotification);
        const string KAndroidActionPrefix = "android.intent.action.";
        /// <summary>
        /// Used as action of <see cref="Intent(string)"/> for dismissed notifications.
        /// </summary>
        public const string KDismissed = KAndroidActionPrefix + "DISMISSED";
        /// <summary>
        /// Used as action of <see cref="Intent(string)"/> for tapped notifications.
        /// </summary>
        public const string KTapped = KAndroidActionPrefix + "TAPPED";
        /// <summary>
        /// Used as action of <see cref="Intent(string)"/> for scheduled notifications.
        /// </summary>
        public const string KScheduled = KAndroidActionPrefix + "SCHEDULED";

    }
}

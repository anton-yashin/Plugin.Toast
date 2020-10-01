using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Droid
{
    public sealed class IntentConstants
    {
        public const string KNotificationId = nameof(KNotificationId);
        public const string KNotifcationTag = nameof(KNotifcationTag);
        public const string KForceOpen = nameof(KForceOpen);
        public const string KNotification = nameof(KNotification);
        const string KAndroidActionPrefix = "android.intent.action.";
        public const string KDismissed = KAndroidActionPrefix + "DISMISSED";
        public const string KTapped = KAndroidActionPrefix + "TAPPED";
        public const string KScheduled = KAndroidActionPrefix + "SCHEDULED";

    }
}

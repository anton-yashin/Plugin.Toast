using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Widget;
using Java.Lang;
using Plugin.Toast.Abstractions;
using System;

namespace Plugin.Toast.Droid
{
    /// <summary>
    /// Interface of proxy of <see cref="NotificationCompat.Builder"/>.
    /// This interface contains platform specific functions, that can't be placed into <see cref="IDroidNotificationExtension"/>.
    /// More docs: https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder.html
    /// </summary>
    public interface IPlatformSpecificExtension : IDroidNotificationExtension, INotificationBuilderExtension<IPlatformSpecificExtension>
    {
        /// <summary>
        /// <see cref="NotificationCompat.Builder.AddAction(int, ICharSequence, PendingIntent)"/>
        /// </summary>
        IPlatformSpecificExtension AddAction(int icon, ICharSequence title, PendingIntent intent);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.AddAction(NotificationCompat.Action)"/>
        /// </summary>
        IPlatformSpecificExtension AddAction(NotificationCompat.Action action);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.AddExtras(Bundle)"/>
        /// </summary>
        IPlatformSpecificExtension AddExtras(Bundle extras);
#if __ANDROID_28__
        /// <summary>
        /// <see cref="NotificationCompat.Builder.AddInvisibleAction(int, string, PendingIntent)"/>
        /// </summary>
        IPlatformSpecificExtension AddInvisibleAction(int icon, string title, PendingIntent intent);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.AddInvisibleAction(int, ICharSequence, PendingIntent)"/>
        /// </summary>
        IPlatformSpecificExtension AddInvisibleAction(int icon, ICharSequence title, PendingIntent intent);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.AddInvisibleAction(NotificationCompat.Action)"/>
        /// </summary>
        IPlatformSpecificExtension AddInvisibleAction(NotificationCompat.Action action);
#endif
        /// <summary>
        /// <see cref="NotificationCompat.Builder.Extend(NotificationCompat.IExtender extender)"/>
        /// </summary>
        IPlatformSpecificExtension Extend(NotificationCompat.IExtender extender);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetContent(RemoteViews views)"/>
        /// </summary>
        IPlatformSpecificExtension SetContent(RemoteViews views);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetContentInfo(ICharSequence)"/>
        /// </summary>
        IPlatformSpecificExtension SetContentInfo(ICharSequence info);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetContentIntent(PendingIntent)"/>
        /// </summary>
        IPlatformSpecificExtension SetContentIntent(PendingIntent intent);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetContentText(ICharSequence)"/>
        /// </summary>
        IPlatformSpecificExtension SetContentText(ICharSequence text);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetContentTitle(ICharSequence title)"/>
        /// </summary>
        IPlatformSpecificExtension SetContentTitle(ICharSequence title);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetCustomBigContentView(RemoteViews)"/>
        /// </summary>
        IPlatformSpecificExtension SetCustomBigContentView(RemoteViews contentView);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetCustomContentView(RemoteViews)"/>
        /// </summary>
        IPlatformSpecificExtension SetCustomContentView(RemoteViews contentView);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetCustomHeadsUpContentView(RemoteViews)"/>
        /// </summary>
        IPlatformSpecificExtension SetCustomHeadsUpContentView(RemoteViews contentView);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetDeleteIntent(PendingIntent)"/>
        /// </summary>
        IPlatformSpecificExtension SetDeleteIntent(PendingIntent intent);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetExtras(Bundle extras)"/>
        /// </summary>
        IPlatformSpecificExtension SetExtras(Bundle extras);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetFullScreenIntent(PendingIntent, bool)"/>
        /// </summary>
        IPlatformSpecificExtension SetFullScreenIntent(PendingIntent intent, bool highPriority);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetLargeIcon(Bitmap)"/>
        /// </summary>
        IPlatformSpecificExtension SetLargeIcon(Bitmap icon);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetPublicVersion(global::Android.App.Notification)"/>
        /// </summary>
        IPlatformSpecificExtension SetPublicVersion(global::Android.App.Notification n);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetRemoteInputHistory(ICharSequence[])"/>
        /// </summary>
        IPlatformSpecificExtension SetRemoteInputHistory(ICharSequence[] text);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetSound(global::Android.Net.Uri)"/>
        /// </summary>
        IPlatformSpecificExtension SetSound(global::Android.Net.Uri sound);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetSound(global::Android.Net.Uri, int)"/>
        /// </summary>
        IPlatformSpecificExtension SetSound(global::Android.Net.Uri sound, int streamType);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetStyle(NotificationCompat.Style)"/>
        /// </summary>
        IPlatformSpecificExtension SetStyle(NotificationCompat.Style style);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetSubText(ICharSequence)"/>
        /// </summary>
        IPlatformSpecificExtension SetSubText(ICharSequence text);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetTicker(string, RemoteViews)"/>
        /// </summary>
        IPlatformSpecificExtension SetTicker(string tickerText, RemoteViews views);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetTicker(ICharSequence, RemoteViews)"/>
        /// </summary>
        IPlatformSpecificExtension SetTicker(ICharSequence tickerText, RemoteViews views);
        /// <summary>
        /// <see cref="NotificationCompat.Builder.SetTicker(ICharSequence)"/>
        /// </summary>
        IPlatformSpecificExtension SetTicker(ICharSequence tickerText);
    }
}

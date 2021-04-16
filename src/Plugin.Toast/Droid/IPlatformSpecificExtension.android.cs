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
        /// Add an action to this notification. Actions are typically displayed by the system as a button adjacent to the notification content. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#addaction_1"/>
        /// </summary>
        /// <param name="icon">Resource ID of a drawable that represents the action.</param>
        /// <param name="title">Text describing the action.</param>
        /// <param name="intent"><see cref="PendingIntent"/> to be fired when the action is invoked.</param>
        /// <remarks>
        /// <para>Action buttons won't appear on platforms prior to Android 4.1. Action buttons depend on
        /// expanded notifications, which are only available in Android 4.1 and later. To ensure that an
        /// action button's functionality is always available, first implement the functionality in the
        /// <see cref="Activity"/> that starts when a user clicks the notification (see
        /// <see cref="SetContentIntent(PendingIntent)"/>), and then enhance the notification by implementing
        /// the same functionality with <see cref="AddAction(NotificationCompat.Action)"/>.</para>
        ///
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension AddAction(int icon, ICharSequence title, PendingIntent intent);
        /// <summary>
        /// Add an action to this notification. Actions are typically displayed by the system as a button adjacent to the notification content. 
        /// <see href="NotificationCompat.Builder.AddAction(NotificationCompat.Action)"/>
        /// </summary>
        /// <param name="action">The action to add.</param>
        /// <remarks>
        /// <para>Action buttons won't appear on platforms prior to Android 4.1. Action buttons depend on
        /// expanded notifications, which are only available in Android 4.1 and later. To ensure that an
        /// action button's functionality is always available, first implement the functionality in the
        /// <see cref="Activity"/> that starts when a user clicks the notification (see
        /// <see cref="SetContentIntent(PendingIntent)"/>), and then enhance the notification by implementing
        /// the same functionality with <see cref="AddAction(NotificationCompat.Action)"/>.</para>
        /// 
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension AddAction(NotificationCompat.Action action);
        /// <summary>
        /// Merge additional metadata into this notification. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#addextras"/>
        /// </summary>
        /// <remarks>
        /// <para>Values within the Bundle will replace existing extras values in this Builder.</para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension AddExtras(Bundle extras);
#if __ANDROID_28__
        /// <summary>
        /// Add an invisible action to this notification. Invisible actions are never displayed by the
        /// system, but can be retrieved and used by other application listening to system notifications.
        /// Invisible actions are supported from Android 4.4.4 (API 20) and can be retrieved using
        /// <see cref="NotificationCompat.GetInvisibleActions(Android.App.Notification)"/>.
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#addinvisibleaction_1"/>
        /// </summary>
        /// <param name="icon">Resource ID of a drawable that represents the action.</param>
        /// <param name="title">Text describing the action.</param>
        /// <param name="intent"><see cref="PendingIntent"/> to be fired when the action is invoked.</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension AddInvisibleAction(int icon, string title, PendingIntent intent);
        /// <summary>
        /// Add an invisible action to this notification. Invisible actions are never displayed by the
        /// system, but can be retrieved and used by other application listening to system notifications.
        /// Invisible actions are supported from Android 4.4.4 (API 20) and can be retrieved using
        /// <see cref="NotificationCompat.GetInvisibleActions(Android.App.Notification)"/>.
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#addinvisibleaction_1"/>
        /// </summary>
        /// <param name="icon">Resource ID of a drawable that represents the action.</param>
        /// <param name="title">Text describing the action.</param>
        /// <param name="intent"><see cref="PendingIntent"/> to be fired when the action is invoked.</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension AddInvisibleAction(int icon, ICharSequence title, PendingIntent intent);
        /// <summary>
        /// Add an invisible action to this notification. Invisible actions are never displayed by the
        /// system, but can be retrieved and used by other application listening to system notifications.
        /// Invisible actions are supported from Android 4.4.4 (API 20) and can be retrieved using
        /// <see cref="NotificationCompat.GetInvisibleActions(Android.App.Notification)"/>.
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#addinvisibleaction_1"/>
        /// </summary>
        /// <param name="action">The action to add.</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension AddInvisibleAction(NotificationCompat.Action action);
#endif
        /// <summary>
        /// <see href="NotificationCompat.Builder.Extend(NotificationCompat.IExtender extender)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension Extend(NotificationCompat.IExtender extender);
        /// <summary>
        /// Supply a custom RemoteViews to use instead of the standard one.
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcontent"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetContent(RemoteViews views);
        /// <summary>
        /// Set the large text at the right-hand side of the notification. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcontentinfo"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetContentInfo(ICharSequence info);
        /// <summary>
        /// Supply a <see cref="PendingIntent "/> to send when the notification is clicked.
        /// If you do not supply an intent, you can now add PendingIntents to individual
        /// views to be launched when clicked by calling <see cref="RemoteViews.SetOnClickPendingIntent(int, PendingIntent?)"/>.
        /// Be sure to read <see cref="Android.App.Notification.ContentIntent"/> for how to correctly use this. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcontentintent"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetContentIntent(PendingIntent intent);
        /// <summary>
        /// Set the text (second row) of the notification, in a standard notification. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcontenttext"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetContentText(ICharSequence text);
        /// <summary>
        /// Set the title (first row) of the notification, in a standard notification. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcontenttitle"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetContentTitle(ICharSequence title);
        /// <summary>
        /// Supply custom <see cref="RemoteViews"/> to use instead of the platform
        /// template in the expanded form. This will override the expanded layout
        /// that would otherwise be constructed by this Builder object. No-op on
        /// versions prior to JELLY_BEAN. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcustombigcontentview"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetCustomBigContentView(RemoteViews contentView);
        /// <summary>
        /// Supply custom <see cref="RemoteViews"/> to use instead of the platform
        /// template. This will override the layout that would otherwise be
        /// constructed by this Builder object. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcustomcontentview"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetCustomContentView(RemoteViews contentView);
        /// <summary>
        /// Supply custom <see cref="RemoteViews"/> to use instead of the platform
        /// template in the heads up dialog. This will override the heads-up layout
        /// that would otherwise be constructed by this Builder object. No-op on
        /// versions prior to LOLLIPOP.
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcustomheadsupcontentview"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetCustomHeadsUpContentView(RemoteViews contentView);
        /// <summary>
        /// Supply a <see cref="PendingIntent"/> to send when the notification is
        /// cleared by the user directly from the notification panel. For example,
        /// this intent is sent when the user clicks the "Clear all" button, or
        /// the individual "X" buttons on notifications. This intent is not sent
        /// when the application calls <see cref="Android.App.NotificationManager.Cancel(int)"/>. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setdeleteintent"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetDeleteIntent(PendingIntent intent);
        /// <summary>
        /// Set metadata for this notification.
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setextras"/>
        /// </summary>
        /// <remarks>
        /// <para> A reference to the Bundle is held for the lifetime of this Builder,
        /// and the Bundle's current contents are copied into the Notification each
        /// time <see cref="IBuilder.Build()"/> is called. </para>
        /// <para> Replaces any existing extras values with those from the provided
        /// Bundle. Use <see cref="AddExtras(Bundle)"/> to merge in metadata instead.</para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetExtras(Bundle extras);
        /// <summary>
        /// <see href="NotificationCompat.Builder.SetFullScreenIntent(PendingIntent, bool)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetFullScreenIntent(PendingIntent intent, bool highPriority);
        /// <summary>
        /// <see href="NotificationCompat.Builder.SetLargeIcon(Bitmap)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetLargeIcon(Bitmap icon);
        /// <summary>
        /// <see href="NotificationCompat.Builder.SetPublicVersion(global::Android.App.Notification)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetPublicVersion(global::Android.App.Notification n);
        /// <summary>
        /// <see href="NotificationCompat.Builder.SetRemoteInputHistory(ICharSequence[])"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetRemoteInputHistory(ICharSequence[] text);
        /// <summary>
        /// <see href="NotificationCompat.Builder.SetSound(global::Android.Net.Uri)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetSound(global::Android.Net.Uri sound);
        /// <summary>
        /// <see href="NotificationCompat.Builder.SetSound(global::Android.Net.Uri, int)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetSound(global::Android.Net.Uri sound, int streamType);
        /// <summary>
        /// <see href="NotificationCompat.Builder.SetStyle(NotificationCompat.Style)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetStyle(NotificationCompat.Style style);
        /// <summary>
        /// <see href="NotificationCompat.Builder.SetSubText(ICharSequence)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetSubText(ICharSequence text);
        /// <summary>
        /// <see href="NotificationCompat.Builder.SetTicker(string, RemoteViews)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetTicker(string tickerText, RemoteViews views);
        /// <summary>
        /// <see href="NotificationCompat.Builder.SetTicker(ICharSequence, RemoteViews)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetTicker(ICharSequence tickerText, RemoteViews views);
        /// <summary>
        /// <see href="NotificationCompat.Builder.SetTicker(ICharSequence)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetTicker(ICharSequence tickerText);
    }
}

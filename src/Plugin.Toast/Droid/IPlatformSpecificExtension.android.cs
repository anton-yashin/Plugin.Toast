using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.Core.App;
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
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#addAction(int,%20java.lang.CharSequence,%20android.app.PendingIntent)"/>
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
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#addAction(androidx.core.app.NotificationCompat.Action)"/>
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
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#addExtras(android.os.Bundle)"/>
        /// </summary>
        /// <remarks>
        /// <para>Values within the Bundle will replace existing extras values in this Builder.</para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension AddExtras(Bundle extras);
        /// <summary>
        /// Add an invisible action to this notification. Invisible actions are never displayed by the
        /// system, but can be retrieved and used by other application listening to system notifications.
        /// Invisible actions are supported from Android 4.4.4 (API 20) and can be retrieved using
        /// <see cref="NotificationCompat.GetInvisibleActions(Android.App.Notification)"/>.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#addInvisibleAction(int,%20java.lang.CharSequence,%20android.app.PendingIntent)"/>
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
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#addInvisibleAction(int,%20java.lang.CharSequence,%20android.app.PendingIntent)"/>
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
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#addInvisibleAction(androidx.core.app.NotificationCompat.Action)"/>
        /// </summary>
        /// <param name="action">The action to add.</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension AddInvisibleAction(NotificationCompat.Action action);
        /// <summary>
        /// Apply an extender to this notification builder. Extenders may be used
        /// to add metadata or change options on this builder. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#extend(androidx.core.app.NotificationCompat.Extender)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension Extend(NotificationCompat.IExtender extender);
        /// <summary>
        /// Supply a custom RemoteViews to use instead of the standard one.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setContent(android.widget.RemoteViews)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetContent(RemoteViews views);
        /// <summary>
        /// A small piece of additional information pertaining to this notification.
        /// Where this text is displayed varies between platform versions. Use
        /// setSubText(CharSequence) instead to set a text in the header. For legacy
        /// apps targeting a version below Build.VERSION_CODES.N this field will
        /// still show up, but the subtext will take precedence. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setContentInfo(java.lang.CharSequence)"/>
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
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setContentIntent(android.app.PendingIntent)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetContentIntent(PendingIntent intent);
        /// <summary>
        /// Set the text (second row) of the notification, in a standard notification. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setContentText(java.lang.CharSequence)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetContentText(ICharSequence text);
        /// <summary>
        /// Set the title (first row) of the notification, in a standard notification. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setContentTitle(java.lang.CharSequence)"/>
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
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setCustomBigContentView(android.widget.RemoteViews)"/>
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
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setCustomContentView(android.widget.RemoteViews)"/>
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
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setCustomHeadsUpContentView(android.widget.RemoteViews)"/>
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
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setDeleteIntent(android.app.PendingIntent)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetDeleteIntent(PendingIntent intent);
        /// <summary>
        /// Set metadata for this notification.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setExtras(android.os.Bundle)"/>
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
        /// An intent to launch instead of posting the notification to the status bar.
        /// Only for use with extremely high-priority notifications demanding the user's
        /// immediate attention, such as an incoming phone call or alarm clock that the
        /// user has explicitly set to a particular time. If this facility is used for
        /// something else, please give the user an option to turn it off and use a
        /// normal notification, as this can be extremely disruptive. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setFullScreenIntent(android.app.PendingIntent,%20boolean)"/>
        /// </summary>
        /// <param name="intent">The pending intent to launch.</param>
        /// <param name="highPriority">Passing true will cause this notification to be sent even if other notifications are suppressed. </param>
        /// <remarks>
        /// <para>On some platforms, the system UI may choose to display a heads-up
        /// notification, instead of launching this intent, while the user is using
        /// the device. </para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetFullScreenIntent(PendingIntent intent, bool highPriority);
        /// <summary>
        /// Set the large icon that is shown in the notification. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setLargeIcon(android.graphics.Bitmap)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetLargeIcon(Bitmap icon);
        /// <summary>
        /// Supply a replacement Notification whose contents should be shown in
        /// insecure contexts (i.e. atop the secure lockscreen).
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setPublicVersion(android.app.Notification)"/>
        /// </summary>
        /// <param name="n">A replacement notification, presumably with some or all info redacted.</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetPublicVersion(global::Android.App.Notification n);
        /// <summary>
        /// Set the remote input history. This should be set to the most recent inputs that have been sent through a
        /// <see href="https://developer.android.com/reference/androidx/core/app/RemoteInput">RemoteInput</see>
        /// of this Notification and cleared once the it is no longer relevant (e.g. for chat notifications
        /// once the other party has responded). The most recent input must be stored at the 0 index, the second most
        /// recent at the 1 index, etc. Note that the system will limit both how far back the inputs will be shown and
        /// how much of each individual input is shown. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setRemoteInputHistory(java.lang.CharSequence[])"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetRemoteInputHistory(ICharSequence[] text);
        /// <summary>
        /// Set the sound to play. It will play on the default stream. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setSound(android.net.Uri)"/>
        /// </summary>
        /// <remarks>
        /// <para>
        /// On some platforms, a notification that is noisy is more likely to be
        /// presented as a heads-up notification.
        /// <br/> 
        /// On platforms Build.VERSION_CODES.O and above this value is ignored in
        /// favor of the value set on the <see cref="IDroidNotificationExtension.SetChannelId(string)"/>
        /// notification's channel. On older platforms, this value is still used,
        /// so it is still required for apps supporting those platforms.
        /// </para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetSound(global::Android.Net.Uri sound);
        /// <summary>
        /// Set the sound to play. It will play on the default stream. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setSound(android.net.Uri,%20int)"/>
        /// </summary>
        /// <remarks>
        /// <para>
        /// On some platforms, a notification that is noisy is more likely to be
        /// presented as a heads-up notification.
        /// <br/> 
        /// On platforms Build.VERSION_CODES.O and above this value is ignored in
        /// favor of the value set on the <see cref="IDroidNotificationExtension.SetChannelId(string)"/>
        /// notification's channel. On older platforms, this value is still used,
        /// so it is still required for apps supporting those platforms.
        /// </para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetSound(global::Android.Net.Uri sound, int streamType);
        /// <summary>
        /// Add a rich notification style to be applied at build time. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setStyle(androidx.core.app.NotificationCompat.Style)"/>
        /// </summary>
        /// <param name="style">Object responsible for modifying the notification style.</param>
        /// <remarks>
        /// <para>If the platform does not provide rich notification styles, this
        /// method has no effect. The user will always see the normal notification
        /// style.</para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetStyle(NotificationCompat.Style style);
        /// <summary>
        /// This provides some additional information that is displayed in the
        /// notification. No guarantees are given where exactly it is displayed. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setSubText(java.lang.CharSequence)"/>
        /// </summary>
        /// <remarks>
        /// <para>
        /// This information should only be provided if it provides an essential
        /// benefit to the understanding of the notification. The more text you
        /// provide the less readable it becomes. For example, an email client
        /// should only provide the account name here if more than one email
        /// account has been added.
        /// <br/>
        /// As of Build.VERSION_CODES.N this information is displayed in the
        /// notification header area.
        /// <br/>
        /// On Android versions before Build.VERSION_CODES.N this will be shown
        /// in the third line of text in the platform notification template. You
        /// should not be using <see cref="IDroidNotificationExtension.SetProgress(int, int, bool)"/> at the
        /// same time on those versions; they occupy the same place. 
        /// </para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetSubText(ICharSequence text);
        /// <summary>
        /// Sets the "ticker" text which is sent to accessibility services. Prior to
        /// <see cref="BuildVersionCodes.Lollipop"/>, sets the text that is displayed
        /// in the status bar when the notification first arrives, and also a
        /// <see cref="RemoteViews"/> object that may be displayed instead on some devices. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setTicker(java.lang.CharSequence,%20android.widget.RemoteViews)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetTicker(string tickerText, RemoteViews views);
        /// <summary>
        /// Sets the "ticker" text which is sent to accessibility services. Prior to
        /// <see cref="BuildVersionCodes.Lollipop"/>, sets the text that is displayed
        /// in the status bar when the notification first arrives, and also a
        /// <see cref="RemoteViews"/> object that may be displayed instead on some devices. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setTicker(java.lang.CharSequence,%20android.widget.RemoteViews)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetTicker(ICharSequence tickerText, RemoteViews views);
        /// <summary>
        /// Sets the "ticker" text which is sent to accessibility services. Prior to
        /// <see cref="BuildVersionCodes.Lollipop"/>, sets the text that is displayed
        /// in the status bar when the notification first arrives. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setTicker(java.lang.CharSequence)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IPlatformSpecificExtension SetTicker(ICharSequence tickerText);
    }
}

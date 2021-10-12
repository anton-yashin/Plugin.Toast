using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugin.Toast
{
#if __ANDROID__ == false
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
#endif
    /// <summary>
    /// Interface is the proxy for <see cref="global::AndroidX.Core.App.NotificationCompat.Builder"/>.
    /// Additional functions: <seealso cref="SetCleanupOnTimeout(bool)"/>,
    /// <seealso cref="SetTimeout(TimeSpan)"/>, <seealso cref="SetChannel(Action{IDroidNotifcationChannelBuilder})"/>.
    /// Other platform specific functions located <see cref="global::Plugin.Toast.Droid.IPlatformSpecificExtension"/>
    /// More docs: <seealso href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder.html"/>
    /// </summary>
    public interface IDroidNotificationExtension : INotificationBuilderExtension<IDroidNotificationExtension>
    {
        /// <summary>
        /// Delete notification from notification center on timeout <seealso cref="SetTimeout(TimeSpan)"/>.
        /// By default cleanup is disabled.
        /// </summary>
        /// <param name="cleanup">true if cleanup is required</param>
        /// <returns>builder</returns>
        IDroidNotificationExtension SetCleanupOnTimeout(bool cleanup);
        /// <summary>
        /// Set timeout to wait user input in <see cref="INotification.ShowAsync(out ToastId, System.Threading.CancellationToken)"/>.
        /// Default is 7 seconds. Set <see cref="System.Threading.Timeout.InfiniteTimeSpan"/> to wait infinite
        /// </summary>
        /// <param name="timeout">Timeout</param>
        /// <returns>builder</returns>
        IDroidNotificationExtension SetTimeout(TimeSpan timeout);

        /// <summary>
        /// Add a person that is relevant to this notification. 
        /// <seealso href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#addPerson(java.lang.String)"/>
        /// </summary>
        /// <remarks>
        /// Depending on user preferences, this annotation may allow the notification to 
        /// pass through interruption filters, and to appear more prominently in the user interface.
        /// <br/>
        /// The person should be specified by the String representation of a
        /// <see cref="global::Android.Provider.ContactsContract.Contacts.ContentLookupUri"/>.
        /// <br/>
        /// The system will also attempt to resolve mailto: and tel: schema URIs. The path part
        /// of these URIs must exist in the contacts database, in the appropriate column, or the
        /// reference will be discarded as invalid.Telephone schema URIs will be resolved by <see cref="global::Android.Provider.ContactsContract.PhoneLookup"/>.
        /// <br/>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension AddPerson(string uri);
        /// <summary>
        /// Will create main activity and remove previous if it is exists.
        /// </summary>
        IDroidNotificationExtension ForceOpenAppOnNotificationTap(bool forceOpenAppOnNotificationTap);
        /// <summary>
        /// Setting this flag will make it so the notification is automatically canceled when the
        /// user clicks it in the panel. The PendingIntent set with
        /// <see cref="Droid.IPlatformSpecificExtension.SetDeleteIntent(Android.App.PendingIntent)"/>
        /// will be broadcast when the notification is canceled.
        /// <seealso href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setAutoCancel(boolean)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetAutoCancel(bool autoCancel);
        /// <summary>
        /// Sets which icon to display as a badge for this notification. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setBadgeIconType(int)"/>
        /// </summary>
        /// <remarks>
        /// <b>Note:</b> This value might be ignored, for launchers that don't support badge icons.
        /// <br/>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetBadgeIconType(DroidBadgeIcon badgeIconType);
        /// <summary>
        /// Set the notification category.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setCategory(java.lang.String)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetCategory(DroidNotificationCategory category);
        /// <summary>
        /// Specifies the channel the notification should be delivered on. No-op on versions prior to O . 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setChannelId(java.lang.String)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetChannelId(string channelId);
        /// <summary>
        /// Sets color.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setColor(int)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetColor(int argb);
#if NETSTANDARD1_4 == false
        /// <summary>
        /// Sets color.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setColor(int)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetColor(System.Drawing.Color color);
#endif
        /// <summary>
        /// Set whether this notification should be colorized. When set, the color set with <see cref="SetColor(int)"/>
        /// will be used as the background color of this notification. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setColorized(boolean)"/>
        /// </summary>
        /// <remarks>
        /// This should only be used for high priority ongoing tasks like navigation, an ongoing call,
        /// or other similarly high-priority events for the user.
        /// <br/>
        /// For most styles, the coloring will only be applied if the notification is for a foreground
        /// service notification.
        /// <br/>
        /// However, for MediaStyle and DecoratedMediaCustomViewStyle notifications that have a media
        /// session attached there is no such requirement.
        /// <br/>
        /// Calling this method on any version prior to O will not have an effect on the notification
        /// and it won't be colorized.
        /// <br/>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetColorized(bool colorize);
        /// <summary>
        /// A small piece of additional information pertaining to this notification.
        /// Where this text is displayed varies between platform versions. Use
        /// <see cref="SetSubText(string)"/> instead to set a text in the header.
        /// For legacy apps targeting a version below Build.VERSION_CODES.N this
        /// field will still show up, but the subtext will take precedence.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setContentInfo(java.lang.CharSequence)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetContentInfo(string info);
        /// <summary>
        /// Set the text (second row) of the notification, in a standard notification. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setContentText(java.lang.CharSequence)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetContentText(string text);
        /// <summary>
        /// Set the title (first row) of the notification, in a standard notification. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setContentTitle(java.lang.CharSequence)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetContentTitle(string title);
        /// <summary>
        /// Set the default notification options that will be used. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setDefaults(int)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetDefaults(DroidNotificationDefaults droidNotificationDefaults);
        /// <summary>
        /// Set this notification to be part of a group of notifications sharing the same key.
        /// Grouped notifications may display in a cluster or stack on devices which support such rendering. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setGroup(java.lang.String)"/>
        /// </summary>
        /// <remarks>
        /// To make this notification the summary for its group, also call <see cref="SetGroupSummary(bool)"/>.
        /// A sort order can be specified for group members by using <see cref="SetSortKey(string)"/>.
        /// <br/>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetGroup(string groupKey);
        /// <summary>
        /// Sets the group alert behavior for this notification. Use this method to mute
        /// this notification if alerts for this notification's group should be handled
        /// by a different notification. This is only applicable for notifications that
        /// belong to a group <see cref="SetGroup(string)"/>. This must be called on all notifications you want to mute.
        /// For example, if you want only the summary of your group to make noise, all
        /// children in the group should have the group alert behavior <see cref="DroidGroupAlert.Summary"/>.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setGroupAlertBehavior(int)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetGroupAlertBehavior(DroidGroupAlert groupAlertBehavior);
        /// <summary>
        /// Set this notification to be the group summary for a group of notifications.
        /// Grouped notifications may display in a cluster or stack on devices which support
        /// such rendering. Requires a group key also be set using <see cref="SetGroup(string)"/>.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setGroupSummary(boolean)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetGroupSummary(bool isGroupSummary);
        /// <summary>
        /// Set the argb value that you would like the LED on the device to blink, as well
        /// as the rate. The rate is specified in terms of the number of milliseconds to
        /// be on and then the number of milliseconds to be off. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setLights(int,%20int,%20int)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetLights(int argb, int onMs, int offMs);
#if NETSTANDARD1_4 == false
        /// <summary>
        /// Set the argb value that you would like the LED on the device to blink, as well
        /// as the rate. The rate is specified in terms of the number of milliseconds to
        /// be on and then the number of milliseconds to be off. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setLights(int,%20int,%20int)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetLights(System.Drawing.Color color, TimeSpan on, TimeSpan off);
#endif
        /// <summary>
        /// Set whether or not this notification is only relevant to the current device.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setLocalOnly(boolean)"/>
        /// </summary>
        /// <remarks>
        /// Some notifications can be bridged to other devices for remote display.
        /// This hint can be set to recommend this notification not be bridged. 
        /// <br/>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetLocalOnly(bool b);
        /// <summary>
        /// Sets the number of items this notification represents. On the latest
        /// platforms, this may be displayed as a badge count for Launchers that
        /// support badging. Prior to Build.VERSION_CODES.O it could be shown in
        /// the header. And prior to Build.VERSION_CODES.N this was shown in the
        /// notification on the right side. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setNumber(int)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetNumber(int number);
        /// <summary>
        /// Set whether this is an ongoing notification. Ongoing notifications
        /// cannot be dismissed by the user, so your application or service must
        /// take care of canceling them. They are typically used to indicate a
        /// background task that the user is actively engaged with (e.g., playing
        /// music) or is pending in some way and therefore occupying the device
        /// (e.g., a file download, sync operation, active network connection).
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setOngoing(boolean)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetOngoing(bool ongoing);
        /// <summary>
        /// Set this flag if you would only like the sound, vibrate and ticker
        /// to be played if the notification is not already showing. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setOnlyAlertOnce(boolean)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetOnlyAlertOnce(bool onlyAlertOnce);
        /// <summary>
        /// Set the relative priority for this notification.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setPriority(int)"/>
        /// </summary>
        /// <param name="pri">Relative priority for this notification.</param>
        /// <remarks>
        /// Priority is an indication of how much of the user's valuable attention
        /// should be consumed by this notification. Low-priority notifications may
        /// be hidden from the user in certain situations, while the user might be
        /// interrupted for a higher-priority notification. The system sets a
        /// notification's priority based on various factors including the setPriority
        /// value. The effect may differ slightly on different platforms. 
        /// <br/>
        /// On platforms Build.VERSION_CODES.O and above this value is ignored in
        /// favor of the importance value set on the
        /// <see cref="SetChannelId(string)">notification's channel</see>. On older
        /// platforms, this value is still used, so it is still required for apps
        /// supporting those platforms.
        /// <br/>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetPriority(DroidPriority pri);
        /// <summary>
        /// Set the progress this notification represents, which may be represented as a
        /// <see href="https://developer.android.com/reference/android/widget/ProgressBar.html">ProgressBar</see>. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setProgress(int,%20int,%20boolean)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetProgress(int max, int progress, bool indeterminate);
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
        /// <para><b>Note</b>: The reply text will only be shown on notifications that have least one action with a RemoteInput.</para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetRemoteInputHistory(string[] text);
        /// <summary>
        /// If this notification is duplicative of a Launcher shortcut, sets the id of the shortcut,
        /// in case the Launcher wants to hide the shortcut. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setShortcutId(java.lang.String)"/>
        /// </summary>
        /// <remarks>
        /// <para><b>Note</b>:This field will be ignored by Launchers that don't support badging or shortcuts.</para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetShortcutId(string shortcutId);
        /// <summary>
        /// Control whether the timestamp set with <see cref="SetWhen(long)"/> is shown in the content view. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setShowWhen(boolean)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetShowWhen(bool show);
        /// <summary>
        /// Set the small icon to use in the notification layouts. Different classes
        /// of devices may return different sizes. See the UX guidelines for more
        /// information on how to design these icons.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setSmallIcon(int)"/>
        /// </summary>
        /// <param name="icon">A resource ID in the application's package of the drawable to use.</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetSmallIcon(int icon);
        /// <summary>
        /// A variant of <see cref="SetSmallIcon(int)"/> that takes an additional level
        /// parameter for when the icon is a
        /// <see href="https://developer.android.com/reference/android/graphics/drawable/LevelListDrawable.html">LevelListDrawable</see>.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setSmallIcon(int,%20int)"/>
        /// </summary>
        /// <param name="icon">A resource ID in the application's package of the drawable to use.</param>
        /// <param name="level">The level to use for the icon.</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetSmallIcon(int icon, int level);
        /// <summary>
        /// Set a sort key that orders this notification among other notifications from the same package.
        /// This can be useful if an external sort was already applied and an app would like to preserve
        /// this. Notifications will be sorted lexicographically using this value, although providing
        /// different priorities in addition to providing sort key may cause this value to be ignored. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setSortKey(java.lang.String)"/>
        /// </summary>
        /// <remarks>
        /// <para>This sort key can also be used to order members of a notification group. See <see cref="SetGroup(string)"/>.</para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetSortKey(string sortKey);
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
        /// should not be using <see cref="SetProgress(int, int, bool)"/> at the
        /// same time on those versions; they occupy the same place. 
        /// </para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetSubText(string text);
        /// <summary>
        /// Sets the "ticker" text which is sent to accessibility services. Prior
        /// to Build.VERSION_CODES.LOLLIPOP, sets the text that is displayed in
        /// the status bar when the notification first arrives. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setTicker(java.lang.CharSequence)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetTicker(string tickerText);
        /// <summary>
        /// Specifies the time at which this notification should be canceled, if it
        /// is not already canceled. No-op on versions prior to Build.VERSION_CODES.O. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setTimeoutAfter(long)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetTimeoutAfter(long durationMs);
        /// <summary>
        /// Specifies the time at which this notification should be canceled, if it
        /// is not already canceled. No-op on versions prior to Build.VERSION_CODES.O. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setTimeoutAfter(long)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetTimeoutAfter(TimeSpan duration);
        /// <summary>
        /// Show the <see cref="SetWhen(long)"/> field as a stopwatch. Instead of
        /// presenting <c>when</c> as a timestamp, the notification will show an
        /// automatically updating display of the minutes and seconds since
        /// <c>when</c>. Useful when showing an elapsed time (like an ongoing
        /// phone call).
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setUsesChronometer(boolean)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetUsesChronometer(bool b);
        /// <summary>
        /// Set the vibration pattern to use. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setVibrate(long[])"/>
        /// </summary>
        /// <remarks>
        /// <para>
        /// On some platforms, a notification that vibrates is more likely to be
        /// presented as a heads-up notification.
        /// <br/>
        /// On platforms Build.VERSION_CODES.O and above this value is ignored in
        /// favor of the value set on the
        /// <see cref="SetChannelId(string)">notification's channel</see>. On
        /// older platforms, this value is still used, so it is still required for
        /// apps supporting those platforms.
        /// </para>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetVibrate(long[] pattern);
        /// <summary>
        /// Sets visibility.
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setVisibility(int)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetVisibility(int visibility);
        /// <summary>
        /// Set the time that the event occurred. Notifications in the panel are
        /// sorted by this time. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setWhen(long)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetWhen(long when);
        /// <summary>
        /// Set the time that the event occurred. Notifications in the panel are
        /// sorted by this time. 
        /// <see href="https://developer.android.com/reference/androidx/core/app/NotificationCompat.Builder#setWhen(long)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetWhen(DateTime when);

        /// <summary>
        /// Add extra data to ContentIntent if used a default intent
        /// </summary>
        IDroidNotificationExtension WithCustomArg(string key, string value);
        /// <summary>
        /// Add extra data to ContentIntent if used a default intent
        /// </summary>
        IDroidNotificationExtension WithCustomArgs(IEnumerable<(string key, string value)> args);

        /// <summary>
        /// Creates channel if it don't exists &amp; uses it
        /// </summary>
        IDroidNotificationExtension SetChannel(Action<IDroidNotifcationChannelBuilder> buildAction);

        /// <summary>
        /// Applies a notification style to the notification
        /// </summary>
        IDroidNotificationExtension SetStyle<T>(Action<T> styleBuilder) where T : IDroidStyleBuilder;

        /// <summary>
        /// Applies a notification style to the notification
        /// </summary>
        Task<IDroidNotificationExtension> SetStyleAsync<T>(Func<T, Task> styleBuilder) where T : IDroidStyleBuilder;
    }
#if __ANDROID__ == false
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
#endif
}

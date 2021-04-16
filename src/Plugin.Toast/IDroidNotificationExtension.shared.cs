using System;
using System.Collections.Generic;

namespace Plugin.Toast
{
#if __ANDROID__ == false
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
#endif
    /// <summary>
    /// Interface is the proxy for <see cref="global::Android.Support.V4.App.NotificationCompat.Builder"/>.
    /// Additional functions: <seealso cref="SetCleanupOnTimeout(bool)"/>,
    /// <seealso cref="SetTimeout(TimeSpan)"/>, <seealso cref="SetChannel(Action{IDroidNotifcationChannelBuilder})"/>.
    /// Other platform specific functions located <see cref="global::Plugin.Toast.Droid.IPlatformSpecificExtension"/>
    /// More docs: <seealso href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder.html"/>
    /// </summary>
    public interface IDroidNotificationExtension : INotificationBuilderExtension<IDroidNotificationExtension>
#if __ANDROID__ == false
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
#endif
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
        /// <seealso href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder.html#addperson"/>
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
        /// user clicks it in the panel. The PendingIntent set with <see cref="Droid.IPlatformSpecificExtension.SetDeleteIntent(Android.App.PendingIntent)"/>
        /// will be broadcast when the notification is canceled.
        /// <seealso href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder.html#setautocancel"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetAutoCancel(bool autoCancel);
        /// <summary>
        /// Sets which icon to display as a badge for this notification. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setbadgeicontype"/>
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
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcategory"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetCategory(DroidNotificationCategory category);
        /// <summary>
        /// Specifies the channel the notification should be delivered on. No-op on versions prior to O . 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setChannelId(java.lang.String)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetChannelId(string channelId);
        /// <summary>
        /// Sets color.
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcolor"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetColor(int argb);
        /// <summary>
        /// Set whether this notification should be colorized. When set, the color set with <see cref="SetColor(int)"/>
        /// will be used as the background color of this notification. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcolorized"/>
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
        /// Set the large text at the right-hand side of the notification. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcontentinfo"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetContentInfo(string info);
        /// <summary>
        /// Set the text (second row) of the notification, in a standard notification. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcontenttext"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetContentText(string text);
        /// <summary>
        /// Set the title (first row) of the notification, in a standard notification. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setcontenttitle"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetContentTitle(string title);
        /// <summary>
        /// Set the default notification options that will be used. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setdefaults"/>
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
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setgroup"/>
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
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setgroupalertbehavior"/>
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
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setgroupsummary"/>
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
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setlights"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetLights(int argb, int onMs, int offMs);
        /// <summary>
        /// Set whether or not this notification is only relevant to the current device.
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setlocalonly"/>
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
        /// Set the large number at the right-hand side of the notification.
        /// This is equivalent to setContentInfo, although it might show the
        /// number in a different font size for readability. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setnumber"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetNumber(int number);
        /// <summary>
        /// Set whether this is an ongoing notification. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setongoing"/>
        /// </summary>
        /// <remarks>
        /// Ongoing notifications differ from regular notifications in the following ways:
        /// <list type="bullet">
        /// <item>Ongoing notifications are sorted above the regular notifications in the notification panel.</item>
        /// <item>Ongoing notifications do not have an 'X' close button, and are not affected by the "Clear all" button.</item>
        /// </list>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetOngoing(bool ongoing);
        /// <summary>
        /// Set this flag if you would only like the sound, vibrate and ticker
        /// to be played if the notification is not already showing. 
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setonlyalertonce"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetOnlyAlertOnce(bool onlyAlertOnce);
        /// <summary>
        /// Set the relative priority for this notification. Priority is an indication of
        /// how much of the user's valuable attention should be consumed by this notification.
        /// Low-priority notifications may be hidden from the user in certain situations,
        /// while the user might be interrupted for a higher-priority notification. The system
        /// sets a notification's priority based on various factors including the setPriority
        /// value. The effect may differ slightly on different platforms.
        /// <see href="https://developer.android.com/reference/android/support/v4/app/NotificationCompat.Builder#setpriority"/>
        /// </summary>
        /// <param name="pri">Relative priority for this notification.</param>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetPriority(int pri);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetProgress(int, int, bool)"/>
        /// </summary>
        IDroidNotificationExtension SetProgress(int max, int progress, bool indeterminate);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetRemoteInputHistory(string[])"/>
        /// </summary>
        IDroidNotificationExtension SetRemoteInputHistory(string[] text);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetShortcutId(string)"/>
        /// </summary>
        IDroidNotificationExtension SetShortcutId(string shortcutId);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetShowWhen(bool)"/>
        /// </summary>
        IDroidNotificationExtension SetShowWhen(bool show);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetSmallIcon(int)"/>
        /// </summary>
        IDroidNotificationExtension SetSmallIcon(int icon);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetSmallIcon(int, int)"/>
        /// </summary>
        IDroidNotificationExtension SetSmallIcon(int icon, int level);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetSortKey(string)"/>
        /// </summary>
        IDroidNotificationExtension SetSortKey(string sortKey);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetSubText(string)"/>
        /// </summary>
        IDroidNotificationExtension SetSubText(string text);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetTicker(string)"/>
        /// </summary>
        IDroidNotificationExtension SetTicker(string tickerText);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetTimeoutAfter(long)"/>
        /// </summary>
        IDroidNotificationExtension SetTimeoutAfter(long durationMs);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetUsesChronometer(bool)"/>
        /// </summary>
        IDroidNotificationExtension SetUsesChronometer(bool b);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetVibrate(long[])"/>
        /// </summary>
        IDroidNotificationExtension SetVibrate(long[] pattern);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetVisibility(int)"/>
        /// </summary>
        IDroidNotificationExtension SetVisibility(int visibility);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetWhen(long)"/>
        /// </summary>
        IDroidNotificationExtension SetWhen(long when);

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
    }
}

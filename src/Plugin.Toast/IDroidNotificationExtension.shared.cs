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
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetBadgeIconType(int)"/>
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
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetCategory(string)"/>
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        IDroidNotificationExtension SetCategory(DroidNotificationCategory category);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetChannelId(string)"/>
        /// </summary>
        IDroidNotificationExtension SetChannelId(string channelId);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetColor(int)"/>
        /// </summary>
        IDroidNotificationExtension SetColor(int argb);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetColorized(bool)"/>
        /// </summary>
        IDroidNotificationExtension SetColorized(bool colorize);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetContentInfo(string)"/>
        /// </summary>
        IDroidNotificationExtension SetContentInfo(string info);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetContentText(stringtext)"/>
        /// </summary>
        IDroidNotificationExtension SetContentText(string text);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetContentTitle(string)"/>
        /// </summary>
        IDroidNotificationExtension SetContentTitle(string title);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetDefaults(int)"/>
        /// </summary>
        IDroidNotificationExtension SetDefaults(int defaults);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetGroup(string)"/>
        /// </summary>
        IDroidNotificationExtension SetGroup(string groupKey);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetGroupAlertBehavior(int)"/>
        /// </summary>
        IDroidNotificationExtension SetGroupAlertBehavior(int groupAlertBehavior);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetGroupSummary(bool)"/>
        /// </summary>
        IDroidNotificationExtension SetGroupSummary(bool isGroupSummary);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetLights(int, int, int)"/>
        /// </summary>
        IDroidNotificationExtension SetLights(int argb, int onMs, int offMs);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetLocalOnly(bool)"/>
        /// </summary>
        IDroidNotificationExtension SetLocalOnly(bool b);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetNumber(int)"/>
        /// </summary>
        IDroidNotificationExtension SetNumber(int number);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetOngoing(bool)"/>
        /// </summary>
        IDroidNotificationExtension SetOngoing(bool ongoing);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetOnlyAlertOnce(bool)"/>
        /// </summary>
        IDroidNotificationExtension SetOnlyAlertOnce(bool onlyAlertOnce);
        /// <summary>
        /// <see cref="global::Android.Support.V4.App.NotificationCompat.Builder.SetPriority(int)"/>
        /// </summary>
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

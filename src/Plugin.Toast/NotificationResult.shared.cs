namespace Plugin.Toast
{
    /// <summary>
    /// Specifies the result of user interaction with the notification
    /// </summary>
    public enum NotificationResult
    {
#if __ANDROID__ == false
#pragma warning disable CS1574 // XML comment has cref attribute that could not be resolved
#endif
        /// <summary>
        /// Unable to determine the result of the operation. For example, 
        /// you set your intent using <seealso cref="Droid.IPlatformSpecificExtension.SetContentIntent(Android.App.PendingIntent)"/>
        /// for notification, or there is no technical ability to determine the result of
        /// the notification (ex: <seealso cref="IIosLocalNotificationExtension"/>).
        /// </summary>
        Unknown,
#if __ANDROID__ == false
#pragma warning restore CS1574 // XML comment has cref attribute that could not be resolved
#endif
        /// <summary>
        /// The user activated the toast notification
        /// </summary>
        Activated,
        /// <summary>
        /// The user dismissed the toast notification.
        /// </summary>
        UserCanceled,
        /// <summary>
        /// The app explicitly hid the toast notification.
        /// </summary>
        ApplicationHidden,
        /// <summary>
        /// The toast notification had been shown for the maximum allowed time and was faded out.
        /// </summary>
        TimedOut,
    }
}

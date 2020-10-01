namespace Plugin.Toast
{
    public enum NotificationResult
    {
        /// <summary>
        /// Unable to determine the result of the operation. For example, 
        /// you set your intent using <seealso cref="Droid.IPlatformSpecificExtension.SetContentIntent(PendingIntent)"/>
        /// for notification, or there is no technical ability to determine the result of
        /// the notification (ex: <seealso cref="IIosLocalNotificationExtension"/>).
        /// </summary>
        Unknown,
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

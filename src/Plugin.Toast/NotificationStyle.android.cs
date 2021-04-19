using System;
using Plugin.Toast.Droid;

namespace Plugin.Toast
{
    /// <summary>
    /// Notification style used by <see cref="IToastOptions.NotificationStyle"/>
    /// </summary>
    public enum NotificationStyle
    {
        /// <summary>
        /// Will choose Snackbar (via <seealso cref="ISnackbarExtension"/>) when &lt; Lollipop and
        /// Notifications (via <seealso cref="IDroidNotificationExtension"/>) when &gt;= Lollipop
        /// </summary>
        Default = 0,
        /// <summary>
        /// Snackbar notifications.
        /// <seealso cref="INotificationManager.GetBuilder()"/> will create a <seealso cref="ISnackbarExtension"/>
        /// </summary>
        Snackbar = 1,
        /// <summary>
        /// Heads-up notifications. These will work on lower than Lollipop but it wont pop up at the top of the screen.
        /// <seealso cref="INotificationManager.GetBuilder()"/> will create a <seealso cref="IDroidNotificationExtension"/>
        /// </summary>
        Notifications = 2
    }

    static class NotificationStyleExtensions
    {
        public static T Resolve<T>(this NotificationStyle @this, T snackbar, T notification)
            => @this switch
            {
                NotificationStyle.Notifications => notification,
                NotificationStyle.Snackbar => snackbar,
                NotificationStyle.Default => AndroidPlatform.IsLollipop ? notification : snackbar,
                _ => throw new ArgumentException(nameof(@this), string.Format("unknown notification style {0}", @this))
            };
    }
}

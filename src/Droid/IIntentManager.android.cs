using Android.App;
using System;
using System.Threading.Tasks;

namespace Plugin.Toast.Droid
{
    /// <summary>
    /// Intent manager must create a content intent & delete intent for notifications,
    /// must watch for broadcast intents.
    /// </summary>
    public interface IIntentManager
    {
        /// <summary>
        /// Creates a content intent & delete intent for notifications.
        /// </summary>
        /// <param name="builder">notification builder</param>
        /// <param name="notificationId">id in <seealso cref="global::Android.App.NotificationManager"/></param>
        /// <exception cref="InvalidOperationException">
        /// if can't get PendingIntent for activity or broadcast from Application.Context
        /// </exception>
        /// <returns>Task source for result of <see cref="INotification.ShowAsync(System.Threading.CancellationToken)"/></returns>
        TaskCompletionSource<NotificationResult> RegisterToShowImmediatly(INotificationBuilder builder, out int notificationId);
        /// <summary>
        /// Creates a content intent for notifications.
        /// </summary>
        /// <param name="builder">notification builder</param>
        /// <param name="build">action to build a android notification</param>
        /// <param name="notificationId">notification id. Currently not used in <seealso cref="global::Android.App.NotificationManager"/></param>
        /// <exception cref="InvalidOperationException">
        /// if can't get PendingIntent for activity or broadcast from Application.Context
        /// </exception>
        /// <returns>pending intent to use in <seealso cref="global::Android.App.AlarmManager"/></returns>
        PendingIntent RegisterToShowWithDelay(INotificationBuilder builder, out int notificationId);
    }
}
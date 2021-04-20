using Android.App;
using System;
using System.Threading.Tasks;

namespace Plugin.Toast.Droid
{
    /// <summary>
    /// Intent manager must create a content intent &amp; delete intent for notifications,
    /// must watch for broadcast intents.
    /// </summary>
    public interface IIntentManager
    {
        /// <summary>
        /// Checks if a <see cref="PendingIntent"/> that will perform a broadcast and unique to <paramref name="toastId"/> is exists.
        /// </summary>
        /// <param name="toastId">Id of <see cref="PendingIntent"/></param>
        /// <returns><b>True</b> if exists</returns>
        bool IsPendingIntentExists(ToastId toastId);
        /// <summary>
        /// Retrieve a <see cref="PendingIntent"/> that will perform a broadcast and unique to <paramref name="toastId"/>.
        /// </summary>
        /// <param name="toastId">Id of <see cref="PendingIntent"/></param>
        /// <returns><see cref="PendingIntent"/> or <b>null</b> if not exists</returns>
        PendingIntent? GetPendingIntentById(ToastId toastId);

        /// <summary>
        /// Creates a content intent &amp; delete intent for notifications.
        /// </summary>
        /// <param name="builder">notification builder</param>
        /// <param name="notificationId">id in <seealso cref="global::Android.App.NotificationManager"/></param>
        /// <exception cref="InvalidOperationException">
        /// if can't get PendingIntent for activity or broadcast from Application.Context
        /// </exception>
        /// <returns>Task source for result of <see cref="INotification.ShowAsync(out ToastId, System.Threading.CancellationToken)"/></returns>
        TaskCompletionSource<NotificationResult> RegisterToShowImmediatly(IPlatformNotificationBuilder builder, ToastId notificationId);
        /// <summary>
        /// Creates a content intent for notifications.
        /// </summary>
        /// <param name="builder">notification builder</param>
        /// <param name="notificationId">notification id. Currently not used in <seealso cref="global::Android.App.NotificationManager"/></param>
        /// <exception cref="InvalidOperationException">
        /// if can't get PendingIntent for activity or broadcast from Application.Context
        /// </exception>
        /// <returns>pending intent to use in <seealso cref="global::Android.App.AlarmManager"/></returns>
        PendingIntent RegisterToShowWithDelay(IPlatformNotificationBuilder builder, ToastId notificationId);
    }
}
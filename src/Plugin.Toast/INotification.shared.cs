using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    /// <summary>
    /// Notification abstraction.
    /// </summary>
    public interface INotification
    {
        /// <summary>
        /// Show notification and wait for timeout or user action
        /// </summary>
        /// <param name="toastId">Identifier, that you can use to remove notification from history, store somewhere, etc</param>
        /// <param name="cancellationToken">token to hide notification</param>
        /// <returns>Notification result</returns>
        /// <exception cref="Exceptions.NotificationException"/>
        /// <exception cref="InvalidOperationException">
        /// Can be thrown on android if library can not get 
        /// an Android.App.NotificationManager from Application.Context. Unlikely
        /// </exception>
        Task<NotificationResult> ShowAsync(out ToastId toastId, CancellationToken cancellationToken);

        /// <summary>
        /// Schedule a notification in due time
        /// </summary>
        /// <param name="deliveryTime">due time</param>
        /// <exception cref="Exceptions.NotificationException"/>
        /// <exception cref="InvalidOperationException">
        /// Can be thrown on android if library can not get
        /// an Android.App.AlarmManager from Application.Context. Unlikely
        /// </exception>
        /// <returns>Token to remove a notification from schedule. You can ignore it</returns>
        /// <remarks>
        /// When using a <see cref="ISnackbarExtension"/> the deliveryTime is always ignored
        /// </remarks>
        IScheduledToastCancellation ScheduleTo(DateTimeOffset deliveryTime);
    }

    /// <summary>
    /// Extensions for <see cref="INotification"/>
    /// </summary>
    public static class NotificationExtensions
    {
        /// <summary>
        /// Show notification and wait for timeout or user action
        /// </summary>
        /// <returns>Notification result</returns>
        /// <exception cref="Exceptions.NotificationException"/>
        /// <exception cref="InvalidOperationException">
        /// Can be thrown on android if library can not get 
        /// an Android.App.NotificationManager from Application.Context. Unlikely
        /// </exception>
        public static Task<NotificationResult> ShowAsync(this INotification @this)
            => @this.ShowAsync(CancellationToken.None);

        /// <summary>
        /// Show notification and wait for timeout or user action
        /// </summary>
        /// <param name="this">The notification</param>
        /// <param name="cancellationToken">token to hide notification</param>
        /// <returns>Notification result</returns>
        /// <exception cref="Exceptions.NotificationException"/>
        /// <exception cref="InvalidOperationException">
        /// Can be thrown on android if library can not get 
        /// an Android.App.NotificationManager from Application.Context. Unlikely
        /// </exception>
        public static Task<NotificationResult> ShowAsync(this INotification @this, CancellationToken cancellationToken)
            => @this.ShowAsync(out _, cancellationToken);

        /// <summary>
        /// Show notification and wait for timeout or user action
        /// </summary>
        /// <param name="this">The notification</param>
        /// <param name="toastId">Identifier, that you can use to remove notification from history, store somewhere, etc</param>
        /// <returns>Notification result</returns>
        /// <exception cref="Exceptions.NotificationException"/>
        /// <exception cref="InvalidOperationException">
        /// Can be thrown on android if library can not get 
        /// an Android.App.NotificationManager from Application.Context. Unlikely
        /// </exception>
        public static Task<NotificationResult> ShowAsync(this INotification @this, out ToastId toastId)
            => @this.ShowAsync(out toastId, CancellationToken.None);


        /// <summary>
        /// Show notification and wait for timeout or user action
        /// </summary>
        /// <returns>Notification result</returns>
        /// <exception cref="Exceptions.NotificationException"/>
        /// <exception cref="InvalidOperationException">
        /// Can be thrown on android if library can not get 
        /// an Android.App.NotificationManager from Application.Context. Unlikely
        /// </exception>
        public static async Task<NotificationResult> ShowAsync(this Task<INotification> @this)
            => await (await @this).ShowAsync(out _, CancellationToken.None);

        /// <summary>
        /// Show notification and wait for timeout or user action
        /// </summary>
        /// <param name="this">The notification</param>
        /// <param name="cancellationToken">token to hide notification</param>
        /// <returns>Notification result</returns>
        /// <exception cref="Exceptions.NotificationException"/>
        /// <exception cref="InvalidOperationException">
        /// Can be thrown on android if library can not get 
        /// an Android.App.NotificationManager from Application.Context. Unlikely
        /// </exception>
        public static async Task<NotificationResult> ShowAsync(this Task<INotification> @this, CancellationToken cancellationToken)
            => await (await @this).ShowAsync(out _, cancellationToken);
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public interface INotification
    {
        /// <summary>
        /// Show notification and wait for timeout or user action
        /// </summary>
        /// <param name="cancellationToken">token to hide notification</param>
        /// <returns>Notification result</returns>
        /// <exception cref="Exceptions.NotificationException"/>
        /// <exception cref="InvalidOperationException">
        /// Can be thrown on android if library can not get 
        /// an Android.App.NotificationManager from Application.Context. Unlikely
        /// </exception>
        Task<NotificationResult> ShowAsync(CancellationToken cancellationToken);

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

    public static class NotificationExtensions
    {
        public static Task<NotificationResult> ShowAsync(this INotification @this)
            => @this.ShowAsync(CancellationToken.None);
    }

}

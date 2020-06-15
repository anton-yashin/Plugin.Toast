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
        Task<NotificationResult> ShowAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Schedule a notification in due time
        /// </summary>
        /// <param name="deliveryTime">due time</param>
        /// <exception cref="Exceptions.NotificationException"/>
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

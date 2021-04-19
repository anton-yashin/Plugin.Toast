using System;
using System.Threading.Tasks;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    /// <summary>
    /// The interface for notification receiver that extends the <see cref="UNUserNotificationCenterDelegate"/> class.
    /// </summary>
    public interface INotificationReceiver
    {
        /// <summary>
        /// Adds a request to tracking list and returns a token that can remove
        /// the request from tracking list by calling <see cref="IDisposable.Dispose"/>.
        /// </summary>
        /// <param name="toastId">The request to be tracking.</param>
        /// <param name="onShown">Action that to be called when notification is shown.</param>
        /// <param name="onTapped">Action that to be called when user interacts with the notification.</param>
        /// <returns>Token to remove <paramref name="toastId"/> from tracking list</returns>
        IDisposable RegisterRequest(ToastId toastId, Action onShown, Action onTapped);
    }
}
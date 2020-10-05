using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public interface IHistory
    {
        /// <summary>
        /// Remove all notifications from the notification center
        /// </summary>
        void RemoveAll();
        /// <summary>
        /// Remove a notification from the notification center
        /// </summary>
        /// <param name="toastId">Identifier of notification</param>
        void Remove(ToastId toastId);

        /// <summary>
        /// Remove a scheduled notification, that not yet shown in the notification center
        /// </summary>
        /// <param name="toastId">Identifier of notification</param>
        void RemoveScheduled(ToastId toastId);

        /// <summary>
        /// Checks if there is a notification with the specified identifier in the delivered notifications
        /// </summary>
        /// <param name="toastId">Identifier of notification</param>
        /// <returns>True if found</returns>
        /// <remarks>
        /// On android api level < 23 this function will always return false
        /// </remarks>
        Task<bool> IsDeliveredAsync(ToastId toastId);

        /// <summary>
        /// Check if there is a notification with the specified identifier in the scheduled notifications
        /// </summary>
        /// <param name="toastId">Identifier of notification</param>
        /// <returns>True if found</returns>
        Task<bool> IsScheduledAsync(ToastId toastId);
    }
}

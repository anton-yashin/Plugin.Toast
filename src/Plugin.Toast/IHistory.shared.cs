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
        /// Checks if there is a notification with the specified identifier in the delivered notifications
        /// </summary>
        /// <param name="toastId">id of notification</param>
        /// <returns>true if found</returns>
        /// <remarks>
        /// On android api level < 23 this function will always return false
        /// </remarks>
        Task<bool> IsDeliveredAsync(ToastId toastId);
    }
}

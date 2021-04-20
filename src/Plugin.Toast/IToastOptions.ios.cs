using System;
using UserNotifications;

namespace Plugin.Toast
{
    /// <summary>
    /// Defaults used by iOS platform to show notifications.
    /// </summary>
    public interface IToastOptions
    {
        /// <summary>
        /// Sound that will be played when the notification is delivered. Default value is null.
        /// </summary>
        UNNotificationSound? Sound { get; }
    }
}

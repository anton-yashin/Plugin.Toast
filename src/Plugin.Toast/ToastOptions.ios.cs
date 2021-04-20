using System;
using UserNotifications;

namespace Plugin.Toast
{
    /// <inheritdoc/>
    public sealed class ToastOptions : IToastOptions
    {
        /// <inheritdoc/>
        public UNNotificationSound? Sound { get; set; }
    }
}

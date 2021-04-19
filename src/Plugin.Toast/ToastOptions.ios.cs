using System;
using UserNotifications;

namespace Plugin.Toast
{
    /// <inheritdoc/>
    public sealed class ToastOptions : IToastOptions
    {
        /// <summary>
        /// Default value is null
        /// </summary>
        public UNNotificationSound? Sound { get; set; }
        /// <summary>
        /// Ignored, always false.
        /// </summary>
        [Obsolete("not supported by OS", true)]
        public bool MultipleAuthorizationRequests { get; set; }
    }
}

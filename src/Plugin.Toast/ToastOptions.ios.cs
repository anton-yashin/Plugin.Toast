using System;
using System.Collections.Generic;
using System.Text;
using UserNotifications;

namespace Plugin.Toast
{
    public sealed class ToastOptions : IToastOptions
    {
        /// <summary>
        /// Default value is null
        /// </summary>
        public UNNotificationSound? Sound { get; set; }
        /// <summary>
        /// Default value is false
        /// </summary>
        public bool MultipleAuthorizationRequests { get; set; }
    }
}

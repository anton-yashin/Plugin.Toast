﻿using System.Threading.Tasks;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    /// <summary>
    /// The abstraction for <see cref="UNUserNotificationCenter.RequestAuthorizationAsync(UNAuthorizationOptions)"/>
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        /// This function invokes <see cref="UNUserNotificationCenter.RequestAuthorizationAsync(UNAuthorizationOptions)"/>
        /// </summary>
        /// <exception cref="Exceptions.NotificationException">
        /// When request isn't approved. See additional data for reason.
        /// </exception>
        Task RequestAuthorizationAsync();

        /// <summary>
        /// True if application is approved to show notifications.
        /// </summary>
        bool IsApproved { get; }
    }
}
using Android.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Options used by android platform to show notifications.
    /// </summary>
    public interface IToastOptions
    {
        /// <summary>
        /// Activity that used to show a snackbar notification.
        /// </summary>
        public Activity Activity { get; }
        /// <summary>
        /// Default notification style used by <see cref="INotificationManager"/>
        /// </summary>
        public NotificationStyle NotificationStyle { get; }
        /// <summary>
        /// The default drawable icon for the small icon on the notification, using <see cref="IDroidNotificationExtension"/>.
        /// Not applicable to <seealso cref="ISnackbarExtension"/>
        /// </summary>
        public int DefaultIconId { get; }
        /// <summary>
        /// Application package name.
        /// </summary>
        string PackageName { get; }
        /// <summary>
        /// Default channel options used by <see cref="INotificationManager"/>
        /// </summary>
        IChannelOptions ChannelOptions { get; }
    }
}

using Android.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface IToastOptions
    {
        public Activity Activity { get; }

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
        IChannelOptions ChannelOptions { get; }
    }
}

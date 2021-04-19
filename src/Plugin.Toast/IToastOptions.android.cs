using Android.App;
using System;

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
        /// Notification style selected by <seealso cref="INotificationManager.GetBuilder()"/>.
        /// Default value is <seealso cref="NotificationStyle.Default"/>
        /// </summary>
        public NotificationStyle NotificationStyle { get; }
        /// <summary>
        /// The default drawable icon for the small icon on the notification, using <see cref="IDroidNotificationExtension"/>.
        /// Not applicable to <seealso cref="ISnackbarExtension"/>. Default value is 
        /// <seealso cref="global::Android.Resource.Drawable.IcDialogInfo"/>
        /// </summary>
        public int DefaultIconId { get; }
        /// <summary>
        /// Aplication package name. By default it is Application.Context.PackageName. See also:<br/>
        /// <seealso cref="global::Android.App.Application.Context"/> <br/>
        /// <seealso cref="global::Android.Content.Context.PackageName"/>
        /// </summary>
        string PackageName { get; }
        /// <summary>
        /// Channel options. Default value is <seealso cref="Toast.ChannelOptions"/>. You can also setup channel using 
        /// <see cref="IDroidNotificationExtension.SetChannel(Action{IDroidNotifcationChannelBuilder})"/>
        /// </summary>
        IChannelOptions ChannelOptions { get; }
    }
}

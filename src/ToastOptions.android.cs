using Android.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public sealed class ToastOptions : IToastOptions
    {
        public ToastOptions(Activity activity) => Activity = activity ?? throw new ArgumentNullException(nameof(activity));

        /// <summary>
        /// Aplication package name. By default it is Application.Context.PackageName. See also:<br/>
        /// <seealso cref="global::Android.App.Application.Context"/> <br/>
        /// <seealso cref="global::Android.Content.Context.PackageName"/>
        /// </summary>
        public string PackageName { get; set; } = Application.Context.PackageName;
        /// <summary>
        /// Notification style selected by <seealso cref="INotificationManager.BuildNotification()"/>.
        /// Default value is <seealso cref="NotificationStyle.Default"/>
        /// </summary>
        public NotificationStyle NotificationStyle { get; set; } = NotificationStyle.Default;
        /// <summary>
        /// The default drawable icon for the small icon on the notification, using <see cref="IDroidNotificationExtension"/>.
        /// Not applicable to <seealso cref="ISnackbarExtension"/>. Default value is 
        /// <seealso cref="global::Android.Resource.Drawable.IcDialogInfo"/>
        /// </summary>
        public int DefaultIconId { get; set; } = global::Android.Resource.Drawable.IcDialogInfo;
        /// <summary>
        /// Channel options. Default value <seealso cref="Droid.ChannelOptions"/>
        /// </summary>
        public IChannelOptions ChannelOptions { get; set; } = new ChannelOptions();
        public Activity Activity { get; }
    }
}

using Android.App;
using System;

namespace Plugin.Toast
{
    /// <inheritdoc/>
    public sealed class ToastOptions : IToastOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToastOptions"/> class.
        /// </summary>
        /// <param name="activity">Activity that used to show a snackbar notification.</param>
        public ToastOptions(Activity activity) => Activity = activity ?? throw new ArgumentNullException(nameof(activity));

        /// <inheritdoc/>
        public string PackageName { get; set; } = Application.Context.PackageName ?? "";
        /// <inheritdoc/>
        public NotificationStyle NotificationStyle { get; set; } = NotificationStyle.Default;
        /// <inheritdoc/>
        public int DefaultIconId { get; set; } = global::Android.Resource.Drawable.IcDialogInfo;
        /// <inheritdoc/>
        public IChannelOptions ChannelOptions { get; set; } = new ChannelOptions();
        /// <inheritdoc/>
        public Activity Activity { get; }
    }
}

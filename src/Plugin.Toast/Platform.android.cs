using Android.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public static partial class Platform
    {
        /// <summary>
        /// You must call this function if you want to receive an event received with an activity intent.
        /// </summary>
        /// <param name="activity">Your main launcher activity</param>
        public static void OnActivated(Activity activity)
        {
            var toastId = ToastId.FromIntent(activity.Intent);
            if (toastId != null)
                AddPendingEvent(new NotificationEvent(toastId));
        }
    }
}

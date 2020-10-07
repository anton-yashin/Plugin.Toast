using Android.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public static partial class Platform
    {
        public static void OnActivated(Activity activity)
        {
            var toastId = ToastId.FromIntent(activity.Intent);
            if (toastId != null)
                AddPendingEvent(new NotificationEvent(toastId));
        }
    }
}

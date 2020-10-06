using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Droid
{
    public interface IAndroidNotificationManager
    {
        void Notify(global::Android.App.Notification notification, ToastId toastId);
        void Cancel(ToastId toastId);
        void CancelAll();
        bool IsDelivered(ToastId toastId);
    }
}

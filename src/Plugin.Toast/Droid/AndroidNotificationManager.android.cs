using Android.App;
using System;
using System.Linq;

namespace Plugin.Toast.Droid
{
    sealed class AndroidNotificationManager : IAndroidNotificationManager
    {
        public AndroidNotificationManager() { }

        public static Android.App.NotificationManager NotificationManager => Android.App.NotificationManager.FromContext(Application.Context)
            ?? throw new InvalidOperationException(ErrorStrings.KNotificationManagerError);

        public void Notify(Android.App.Notification notification, ToastId toastId)
            => NotificationManager.Notify(toastId.Id, notification);

        public void Cancel(ToastId toastId)
            => NotificationManager.Cancel(toastId.Id);

        public bool IsDelivered(ToastId toastId) => false;

        public void CancelAll() => NotificationManager.CancelAll();
    }

    sealed class AndroidNotificationManagerEclair : IAndroidNotificationManager
    {
        public AndroidNotificationManagerEclair() { }

        public void Notify(Android.App.Notification notification, ToastId toastId)
            => AndroidNotificationManager.NotificationManager.Notify(toastId.Tag, toastId.Id, notification);

        public void Cancel(ToastId toastId)
            => AndroidNotificationManager.NotificationManager.Cancel(toastId.Tag, toastId.Id);
        public void CancelAll() => AndroidNotificationManager.NotificationManager.CancelAll();

        public bool IsDelivered(ToastId toastId) => false;
    }

    sealed class AndroidNotificationManagerM : IAndroidNotificationManager
    {
        public AndroidNotificationManagerM() { }

        public void Notify(Android.App.Notification notification, ToastId toastId)
            => AndroidNotificationManager.NotificationManager.Notify(toastId.Tag, toastId.Id, notification);

        public void Cancel(ToastId toastId)
            => AndroidNotificationManager.NotificationManager.Cancel(toastId.Tag, toastId.Id);

        public void CancelAll() => AndroidNotificationManager.NotificationManager.CancelAll();

        public bool IsDelivered(ToastId toastId)
            => AndroidNotificationManager.NotificationManager.GetActiveNotifications()
            ?.Where(n => n.Tag == toastId.Tag && n.Id == toastId.Id)?.Any() == true;
    }

}

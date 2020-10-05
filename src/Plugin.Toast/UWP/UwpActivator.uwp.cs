using System;
using System.Web;
using Windows.ApplicationModel.Activation;

namespace Plugin.Toast.UWP
{
    sealed class UwpActivator : IActivator
    {
        public UwpActivator() { }

        public event EventHandler<NotificationEvent>? NotificationEvent;

        public void OnSystemEvent(object args)
        {
            if (args is ToastNotificationActivatedEventArgs toastArgs)
            {
                var collection = HttpUtility.ParseQueryString(toastArgs.Argument);
                var tag = collection[UwpConstants.KTag];
                var group = collection[UwpConstants.KGroup];
                if (tag == null)
                    throw new InvalidOperationException("Tag value isn't found");
                var tid = new ToastId(tag, group, ToastIdNotificationType.ToastNotification);
                NotificationEvent?.Invoke(this, new NotificationEvent(tid));
            }
        }
    }
}

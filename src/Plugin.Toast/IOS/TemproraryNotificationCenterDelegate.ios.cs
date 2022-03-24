using System;
using UIKit;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    sealed class TemproraryNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        private readonly Action<NotificationEvent> addPendingEvent;

        public TemproraryNotificationCenterDelegate(Action<NotificationEvent> addPendingEvent)
        {
            this.addPendingEvent = addPendingEvent;
        }

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            var ca =
#if NET6_0_OR_GREATER
                OperatingSystem.IsIOSVersionAtLeast(14)
#else
                UIDevice.CurrentDevice.CheckSystemVersion(14, 0)
#endif
                ? (UNNotificationPresentationOptions.Banner | UNNotificationPresentationOptions.List) : UNNotificationPresentationOptions.Alert;

            completionHandler(ca);
        }

        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            addPendingEvent(new NotificationEvent(new ToastId(response.Notification.Request.Identifier)));
            completionHandler();
        }
    }
}

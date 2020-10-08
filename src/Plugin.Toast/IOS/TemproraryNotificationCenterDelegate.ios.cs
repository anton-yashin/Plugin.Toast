using System;
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
            completionHandler(UNNotificationPresentationOptions.Alert);
        }

        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            addPendingEvent(new NotificationEvent(new ToastId(response.Notification.Request.Identifier)));
            completionHandler();
        }
    }
}

using System;
using System.Collections.Generic;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    sealed class NotificationReceiver : UNUserNotificationCenterDelegate, INotificationReceiver
    {
        private readonly Dictionary<ToastId, Request> requests;
        private readonly ISystemEventSource systemEventSource;

        public NotificationReceiver(ISystemEventSource systemEventSource)
        {
            requests = new Dictionary<ToastId, Request>();
            UNUserNotificationCenter.Current.Delegate = this;
            this.systemEventSource = systemEventSource;
        }

        public IDisposable RegisterRequest(ToastId toastId, Action onShown, Action onTapped)
        {
            lock (requests)
                requests[toastId] = new Request(onShown, onTapped);
            UNUserNotificationCenter.Current.Delegate = this;
            return new RegistrationToken(this, toastId);
        }

        void RemoveRequest(ToastId toastId)
        {
            lock (requests)
                requests.Remove(toastId);
        }

        Request? GetRequest(ToastId toastId)
        {
            lock (requests)
            {
                if (requests.TryGetValue(toastId, out var value))
                    return value;
            }
            return null;
        }

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            GetRequest(new ToastId(notification.Request.Identifier))?.OnShown();
            completionHandler(UNNotificationPresentationOptions.Alert);
        }

        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            var tid = new ToastId(response.Notification.Request.Identifier);
            GetRequest(tid)?.OnTapped();
            systemEventSource.SendEvent(new NotificationEvent(tid));
            completionHandler();
        }

        sealed class Request
        {
            public Request(Action onShown, Action onTapped)
                => (OnShown, OnTapped) = (onShown, onTapped);

            public Action OnShown { get; }
            public Action OnTapped { get; }
        }


        sealed class RegistrationToken : IDisposable
        {
            private readonly NotificationReceiver receiver;
            private readonly ToastId toastId;

            public RegistrationToken(NotificationReceiver receiver, ToastId toastId)
                => (this.receiver, this.toastId) = (receiver, toastId);

            public void Dispose() => receiver.RemoveRequest(toastId);
        }
    }
}

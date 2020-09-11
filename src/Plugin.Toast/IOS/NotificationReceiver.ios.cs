using System;
using System.Collections.Generic;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    sealed class NotificationReceiver : UNUserNotificationCenterDelegate, INotificationReceiver
    {
        private readonly Dictionary<string, Request> requests;

        public NotificationReceiver()
        {
            requests = new Dictionary<string, Request>();
        }

        public IDisposable RegisterRequest(string id, Action onShown, Action onTapped)
        {
            lock (requests)
                requests[id] = new Request(onShown, onTapped);
            UNUserNotificationCenter.Current.Delegate = this;
            return new RegistrationToken(this, id);
        }

        void RemoveRequest(string id)
        {
            lock (requests)
                requests.Remove(id);
        }

        Request? GetRequest(string id)
        {
            lock (requests)
            {
                if (requests.TryGetValue(id, out var value))
                    return value;
            }
            return null;
        }

        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            GetRequest(notification.Request.Identifier)?.OnShown();
            completionHandler(UNNotificationPresentationOptions.Alert);
        }

        public override void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
            => GetRequest(response.Notification.Request.Identifier)?.OnTapped();

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
            private readonly string id;

            public RegistrationToken(NotificationReceiver receiver, string id)
                => (this.receiver, this.id) = (receiver, id);

            public void Dispose() => receiver.RemoveRequest(id);
        }
    }
}

using System;
using System.Collections.Generic;
using UIKit;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    sealed class NotificationReceiver : INotificationReceiver
    {
        readonly NotificationReceiverImpl impl;

        public NotificationReceiver(ISystemEventSource systemEventSource)
        {
            lock (UNUserNotificationCenter.Current)
            {
                var impl = UNUserNotificationCenter.Current.Delegate as NotificationReceiverImpl;
                if (impl != null)
                    this.impl = impl;
                else
                    UNUserNotificationCenter.Current.Delegate = this.impl = new NotificationReceiverImpl(systemEventSource);
            }
        }

        public IDisposable RegisterRequest(ToastId toastId, Action onShown, Action onTapped)
            => impl.RegisterRequest(toastId, onShown, onTapped);

        sealed class NotificationReceiverImpl : UNUserNotificationCenterDelegate, INotificationReceiver
        {
            private readonly Dictionary<ToastId, Request> requests;
            private readonly ISystemEventSource systemEventSource;

            public NotificationReceiverImpl(ISystemEventSource systemEventSource)
            {
                requests = new Dictionary<ToastId, Request>();
                this.systemEventSource = systemEventSource;
            }

            public IDisposable RegisterRequest(ToastId toastId, Action onShown, Action onTapped)
            {
                lock (requests)
                    requests[toastId] = new Request(onShown, onTapped);
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
                private readonly NotificationReceiverImpl receiver;
                private readonly ToastId toastId;

                public RegistrationToken(NotificationReceiverImpl receiver, ToastId toastId)
                    => (this.receiver, this.toastId) = (receiver, toastId);

                public void Dispose() => receiver.RemoveRequest(toastId);
            }
        }

    }
}

using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Plugin.Toast.Exceptions;

namespace Plugin.Toast.UWP
{
    sealed class Notification : INotification
    {
        private readonly XmlDocument xmlDocument;
        private readonly IPlatformNotificationBuilder notificationBuilder;

        public Notification(XmlDocument xmlDocument, IPlatformNotificationBuilder notificationBuilder)
        {
            this.xmlDocument = xmlDocument;
            this.notificationBuilder = notificationBuilder;
        }

        public Notification(ToastContent toastContent, IPlatformNotificationBuilder notificationBuilder)
            : this(toastContent.GetXml(), notificationBuilder)
        { }

        public Task<NotificationResult> ShowAsync(out ToastId toastId, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<NotificationResult>();
            var tn = new ToastNotification(xmlDocument);
            tn.Tag = string.IsNullOrEmpty(notificationBuilder.Tag) ? Guid.NewGuid().ToString() : notificationBuilder.Tag;
            if (string.IsNullOrEmpty(notificationBuilder.Group) == false)
                tn.Group = notificationBuilder.Group;
            if (string.IsNullOrEmpty(notificationBuilder.RemoteId) == false)
                tn.RemoteId = notificationBuilder.RemoteId;
            tn.Activated += (o, e) => tcs.TrySetResult(NotificationResult.Activated);
            tn.Dismissed += (o, e) => tcs.TrySetResult(ToNotificationResult(e.Reason));
            tn.Failed += (o, e) => tcs.TrySetException(new NotificationException(e.ErrorCode.Message, e.ErrorCode));
            ToastNotificationManager.CreateToastNotifier().Show(tn);
            toastId = ToastId.FromNotification(tn);

            if (cancellationToken.CanBeCanceled)
                return tcs.WatchCancellationAsync(cancellationToken, () => ToastNotificationManager.History.Remove(tn.Tag));
            return tcs.Task;
        }

        internal static NotificationResult ToNotificationResult(ToastDismissalReason reason)
        {
            switch (reason)
            {
                case ToastDismissalReason.ApplicationHidden:
                    return NotificationResult.ApplicationHidden;
                case ToastDismissalReason.TimedOut:
                    return NotificationResult.TimedOut;
                case ToastDismissalReason.UserCanceled:
                    return NotificationResult.UserCanceled;
            }
            throw new InvalidOperationException("Unknown value of " + nameof(ToastDismissalReason) + ": " + reason);
        }

        public IScheduledToastCancellation ScheduleTo(DateTimeOffset deliveryTime)
        {
            var tn = notificationBuilder.SnoozeInterval != null ?
                new ScheduledToastNotification(xmlDocument, deliveryTime, notificationBuilder.SnoozeInterval.Value, notificationBuilder.MaximumSnoozeCount) :
                new ScheduledToastNotification(xmlDocument, deliveryTime);
            tn.Tag = string.IsNullOrEmpty(notificationBuilder.Tag) ? Guid.NewGuid().ToString() : notificationBuilder.Tag;
            tn.SuppressPopup = notificationBuilder.SuppressPopup;
            if (string.IsNullOrEmpty(notificationBuilder.Group) == false)
                tn.Group = notificationBuilder.Group;
            if (string.IsNullOrEmpty(notificationBuilder.RemoteId) == false)
                tn.RemoteId = notificationBuilder.RemoteId;
            return new ScheduledToastCancellation(tn);
        }
    }
}

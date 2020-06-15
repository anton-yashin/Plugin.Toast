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

        public Notification(string xmlContent)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlContent ?? throw new ArgumentNullException(nameof(xmlContent)));
            this.xmlDocument = xmlDocument;
        }

        public Notification(XmlDocument xmlDocument) => this.xmlDocument = xmlDocument;

        public Notification(ToastContent toastContent) : this(toastContent.GetXml()) { }

        public Task<NotificationResult> ShowAsync(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<NotificationResult>();
            var tn = new ToastNotification(xmlDocument);
            tn.Tag = Guid.NewGuid().ToString();
            tn.Activated += (o, e) => tcs.TrySetResult(NotificationResult.Activated);
            tn.Dismissed += (o, e) => tcs.TrySetResult(ToNotificationResult(e.Reason));
            tn.Failed += (o, e) => tcs.TrySetException(new NotificationException(e.ErrorCode.Message, e.ErrorCode));
            ToastNotificationManager.CreateToastNotifier().Show(tn);

            if (cancellationToken.CanBeCanceled)
                return tcs.WatchCancellationAsync(cancellationToken, () => ToastNotificationManager.History.Remove(tn.Tag));
            return tcs.Task;
        }

        static NotificationResult ToNotificationResult(ToastDismissalReason reason)
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
            throw new NotImplementedException("dead code");
        }

        public IScheduledToastCancellation ScheduleTo(DateTimeOffset deliveryTime)
        {
            return new ScheduledToastCancellation(new ScheduledToastNotification(xmlDocument, deliveryTime));
        }
    }
}

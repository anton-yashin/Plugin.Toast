using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UIKit;

namespace Plugin.Toast.IOS
{
    sealed class LocalNotification : INotification
    {
        private readonly ILocalNotificationBuilder builder;

        public LocalNotification(ILocalNotificationBuilder builder) => this.builder = builder;

        public IScheduledToastCancellation ScheduleTo(DateTimeOffset deliveryTime)
        {
            var notification = builder.Notification;
            notification.FireDate = (NSDate)deliveryTime.UtcDateTime;
            UIApplication.SharedApplication.ScheduleLocalNotification(notification);
            return new ScheduledToastCancellation(notification);
        }

        public Task<NotificationResult> ShowAsync(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<NotificationResult>();
            var notification = builder.Notification;
            UIApplication.SharedApplication.PresentLocalNotificationNow(notification);
            // FIXME: may be it possible to get result of notification?
            tcs.TrySetResult(NotificationResult.Unknown);
            return tcs.Task;
        }

        sealed class ScheduledToastCancellation : IScheduledToastCancellation
        {
            private readonly UILocalNotification notification;

            public ScheduledToastCancellation(UILocalNotification notification) => this.notification = notification;

            public void Dispose() => UIApplication.SharedApplication.CancelLocalNotification(notification);
        }

    }
}

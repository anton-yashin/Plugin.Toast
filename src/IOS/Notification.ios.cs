using System;
using System.Threading;
using System.Threading.Tasks;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    sealed class Notification : INotification
    {
        private readonly INotificationBuilder builder;
        private readonly INotificationReceiver notificationReceiver;
        private readonly IPermission permission;
        const double KMagicTimeout = 7;
        UNUserNotificationCenter UNC => UNUserNotificationCenter.Current;

        public Notification(INotificationBuilder builder, INotificationReceiver notificationReceiver, IPermission permission)
            => (this.builder, this.notificationReceiver, this.permission) = (builder, notificationReceiver, permission);

        public IScheduledToastCancellation ScheduleTo(DateTimeOffset deliveryTime)
        {
            permission.RequestAuthorizationAsync().GetAwaiter().GetResult();
            string id = Guid.NewGuid().ToString();
            var trigger = UNCalendarNotificationTrigger.CreateTrigger(deliveryTime.ToNSDateComponents(), false);
            var request = UNNotificationRequest.FromIdentifier(id, builder.Notification, trigger);

            UNC.AddNotificationRequest(request, _ => { });

            return new ScheduledToastCancellation(id);
        }

        public async Task<NotificationResult> ShowAsync(CancellationToken cancellationToken)
        {
            await permission.RequestAuthorizationAsync();
            var tcs = new TaskCompletionSource<NotificationResult>();
            string id = Guid.NewGuid().ToString();
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(0.25, false);
            var request = UNNotificationRequest.FromIdentifier(id, builder.Notification, trigger);

            using (var timer = new Timer(_ => tcs.TrySetResult(NotificationResult.TimedOut), null, Timeout.Infinite, Timeout.Infinite))
            using (notificationReceiver.RegisterRequest(id,
                onShown: () => timer.Change(TimeSpan.FromSeconds(KMagicTimeout), Timeout.InfiniteTimeSpan),
                onTapped: () => tcs.TrySetResult(NotificationResult.Activated)))
            {
                UNC.AddNotificationRequest(request,
                    error => tcs.TrySetException(new Exceptions.NotificationException(error.ToString())));

                if (cancellationToken.CanBeCanceled)
                    return await tcs.WatchCancellationAsync(cancellationToken,
                        onCancellation: () => UNC.RemoveDeliveredNotifications(new string[] { id }));
                return await tcs.Task;
            }
        }

        sealed class ScheduledToastCancellation : IScheduledToastCancellation
        {
            private readonly string id;

            public ScheduledToastCancellation(string id) => this.id = id;

            public void Dispose() => UNUserNotificationCenter.Current.RemoveDeliveredNotifications(new string[] { id });
        }
    }
}

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
        static UNUserNotificationCenter UNC => UNUserNotificationCenter.Current;

        public Notification(INotificationBuilder builder, INotificationReceiver notificationReceiver, IPermission permission)
            => (this.builder, this.notificationReceiver, this.permission) = (builder, notificationReceiver, permission);

        public IScheduledToastCancellation ScheduleTo(DateTimeOffset deliveryTime)
        {
            if (permission.IsApproved == false)
                throw new Exceptions.NotificationException("not authorized. Please call " + nameof(IInitialization) + "." + nameof(IInitialization.InitializeAsync));
            string id = Guid.NewGuid().ToString();
            var trigger = UNCalendarNotificationTrigger.CreateTrigger(deliveryTime.ToNSDateComponents(), false);
            var request = UNNotificationRequest.FromIdentifier(id, builder.Notification, trigger);

            UNC.AddNotificationRequest(request, _ => { });

            return new ScheduledToastCancellation(id);
        }

        public Task<NotificationResult> ShowAsync(out ToastId toastId, CancellationToken cancellationToken)
        {
            toastId = new ToastId(Guid.NewGuid().ToString());
            return PrivateShowAsync(toastId, cancellationToken);
        }

        async Task<NotificationResult> PrivateShowAsync(ToastId toastId, CancellationToken cancellationToken)
        {
            await permission.RequestAuthorizationAsync();
            var tcs = new TaskCompletionSource<NotificationResult>();
            var trigger = UNTimeIntervalNotificationTrigger.CreateTrigger(0.25, false);
            var request = UNNotificationRequest.FromIdentifier(toastId.Id, builder.Notification, trigger);

            using (var timer = new Timer(_ => tcs.TrySetResult(NotificationResult.TimedOut), null, Timeout.Infinite, Timeout.Infinite))
            using (notificationReceiver.RegisterRequest(toastId,
                onShown: () => timer.Change(TimeSpan.FromSeconds(KMagicTimeout), Timeout.InfiniteTimeSpan),
                onTapped: () => tcs.TrySetResult(NotificationResult.Activated)))
            {
                UNC.AddNotificationRequest(request,
                    error =>
                    {
                        if (error != null)
                            tcs.TrySetException(new Exceptions.NotificationException(error.ToString()));
                    });

                if (cancellationToken.CanBeCanceled)
                    return await tcs.WatchCancellationAsync(cancellationToken,
                        onCancellation: () => UNC.RemoveDeliveredNotifications(new string[] { toastId.Id }));
                return await tcs.Task;
            }
        }

        sealed class ScheduledToastCancellation : IScheduledToastCancellation
        {
            private readonly string id;

            public ScheduledToastCancellation(string id) => this.id = id;

            public ToastId ToastId => new ToastId(id);

            public void Dispose() => UNC.RemovePendingNotificationRequests(new string[] { id });
        }
    }
}

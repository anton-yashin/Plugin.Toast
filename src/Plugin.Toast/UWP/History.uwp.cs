using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Plugin.Toast.UWP
{
    sealed class History : IHistory
    {
        private readonly ToastNotifier notifier;

        public History() 
        {
            this.notifier = ToastNotificationManager.CreateToastNotifier();
        }

        public Task<bool> IsDeliveredAsync(ToastId toastId)
            => Task.FromResult(ToastNotificationManager.History.GetHistory()
                .Where(n => n.Tag == toastId.Tag && n.Group == toastId.Group).Any());

        public Task<bool> IsScheduledAsync(ToastId toastId)
        {
            if (toastId.NotificationType != ToastIdNotificationType.ScheduledToastNotification)
                throw new ArgumentException("Required id of scheduled toast notification", nameof(toastId));
            return Task.FromResult(notifier.GetScheduledToastNotifications().Where(n => n.Tag == toastId.Tag && n.Group == toastId.Group).Any());
        }

        public void RemoveDelivered(ToastId toastId)
        {
            if (string.IsNullOrEmpty(toastId.Group) == false)
                ToastNotificationManager.History.Remove(toastId.Tag, toastId.Group);
            else
                ToastNotificationManager.History.Remove(toastId.Tag);
        }

        public void RemoveScheduled(ToastId toastId)
        {
            if (toastId.NotificationType != ToastIdNotificationType.ScheduledToastNotification)
                throw new ArgumentException("Required id of scheduled toast notification", nameof(toastId));
            var notification = notifier.GetScheduledToastNotifications().Where(n => n.Tag == toastId.Tag && n.Group == toastId.Group).FirstOrDefault();
            if (notification != null)
                notifier.RemoveFromSchedule(notification);
        }

        public void RemoveAllDelivered() => ToastNotificationManager.History.Clear();
    }
}

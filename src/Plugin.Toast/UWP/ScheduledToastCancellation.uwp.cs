using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Notifications;

namespace Plugin.Toast.UWP
{
    sealed class ScheduledToastCancellation : IScheduledToastCancellation
    {
        private readonly ToastNotifier toastNotifier;
        private readonly ScheduledToastNotification toastNotification;

        public ScheduledToastCancellation(ScheduledToastNotification toastNotification)
        {
            this.toastNotifier = ToastNotificationManager.CreateToastNotifier();
            this.toastNotification = toastNotification;

            toastNotifier.AddToSchedule(toastNotification);
        }

        public void Dispose() => toastNotifier.RemoveFromSchedule(toastNotification);
    }
}

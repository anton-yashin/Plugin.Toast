﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    sealed class History : IHistory
    {
        public async Task<bool> IsDeliveredAsync(ToastId toastId)
        {
            var list = await UNUserNotificationCenter.Current.GetDeliveredNotificationsAsync();
            return list.Where(n => n.Request.Identifier == toastId.Id).Any();
        }

        public async Task<bool> IsScheduledAsync(ToastId toastId)
        {
            var pendingRequests = await UNUserNotificationCenter.Current.GetPendingNotificationRequestsAsync();
            return pendingRequests.Where(n => n.Identifier == toastId.Id).Any();
        }

        public void RemoveDelivered(ToastId toastId)
            => UNUserNotificationCenter.Current.RemoveDeliveredNotifications(new string[] { toastId.Id });

        public void RemoveScheduled(ToastId toastId)
            => UNUserNotificationCenter.Current.RemovePendingNotificationRequests(new string[] { toastId.Id });

        public void RemoveAllDelivered() => UNUserNotificationCenter.Current.RemoveAllDeliveredNotifications();
    }
}

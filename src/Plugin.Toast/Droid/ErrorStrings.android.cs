using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Droid
{
    static class ErrorStrings
    {
        public const string KActivityError = "can't get PendingIntent for activity from Application.Context";
        public const string KBroadcastError = "can't get PendingIntent for broadcast from Application.Context";
        public const string KNotificationManagerError = "can't get NotificationManager from Application.Context";
        public const string KAlarmManagerError = "can't get AlarmManager from Application.Context";
        public const string KChannelIdError = "Channel id not found";
    }
}

using System;
using ANotificationChannel = global::Android.App.NotificationChannel;

namespace Plugin.Toast.Droid
{
    internal interface IInternalNotificationChannelBuilder : INotificationChannelBuilder
    {
        ANotificationChannel Build();
    }
}

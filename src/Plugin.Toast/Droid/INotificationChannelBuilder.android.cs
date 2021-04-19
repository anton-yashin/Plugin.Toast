using System;

namespace Plugin.Toast.Droid
{
    public interface INotificationChannelBuilder : IDroidNotifcationChannelBuilder
    {
        INotificationChannelBuilder SetSound(global::Android.Net.Uri sound, global::Android.Media.AudioAttributes audioAttributes);
    }
}

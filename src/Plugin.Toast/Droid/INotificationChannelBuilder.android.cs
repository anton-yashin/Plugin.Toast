using System;

namespace Plugin.Toast.Droid
{
    /// <summary>
    /// Interface of platform dependent channel builder.
    /// </summary>
    public interface INotificationChannelBuilder : IDroidNotifcationChannelBuilder
    {
        /// <summary>
        /// Sets the sound that should be played for notifications posted to this
        /// channel and its audio attributes. Notification channels with an
        /// <see cref="IDroidNotifcationChannelBuilder.SetImportance(DroidNotificationImportance)"/>
        /// of at least <see cref="DroidNotificationImportance.Default"/> should
        /// have a sound.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the Android Open Source Project and used according to terms described
        /// in the Creative Commons 2.5 Attribution License. 
        /// </remarks>
        INotificationChannelBuilder SetSound(global::Android.Net.Uri sound, global::Android.Media.AudioAttributes audioAttributes);
    }
}

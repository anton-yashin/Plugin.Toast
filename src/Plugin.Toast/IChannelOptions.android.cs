using System;

namespace Plugin.Toast
{
    /// <summary>
    /// Options that used when andriod notification channel is created.
    /// </summary>
    public interface IChannelOptions
    {
        /// <summary>
        /// Channel name. Default value is "default".
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Channel id. Default value is "default".
        /// </summary>
        string Id { get; }
        /// <summary>
        /// Notification importance of channel. Default value is <see cref="DroidNotificationImportance.High"/>.
        /// </summary>
        DroidNotificationImportance NotificationImportance { get; }
        /// <summary>
        /// Show application icon badges in Launcher. Default value is true.
        /// </summary>
        bool ShowBadge { get; }
        /// <summary>
        /// Enable vibration for channel. Default value is true.
        /// </summary>
        bool EnableVibration { get; }
    }
}

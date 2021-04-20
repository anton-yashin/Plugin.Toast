using System;

namespace Plugin.Toast
{
    /// <inheritdoc/>
    public class ChannelOptions : IChannelOptions
    {
        /// <inheritdoc/>
        public string Name { get; set; } = "default";
        /// <inheritdoc/>
        public string Id { get; set; } = "default";
        /// <inheritdoc/>
        public DroidNotificationImportance NotificationImportance { get; set; } = DroidNotificationImportance.High;
        /// <inheritdoc/>
        public bool ShowBadge { get; set; } = true;
        /// <inheritdoc/>
        public bool EnableVibration { get; set; } = true;
    }
}

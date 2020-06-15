using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public class ChannelOptions : IChannelOptions
    {
        /// <summary>
        /// Channel name. Default value is "default".
        /// </summary>
        public string Name { get; set; } = "default";
        /// <summary>
        /// Channel id. Default value is "default".
        /// </summary>
        public string Id { get; set; } = "default";
        /// <summary>
        /// Notification importance of channel. Default value is <see cref="NotificationImportance.High"/>.
        /// </summary>
        public NotificationImportance NotificationImportance { get; set; } = NotificationImportance.High;
        /// <summary>
        /// Show application icon badges in Launcher. Default value is true.
        /// </summary>
        public bool ShowBadge { get; set; } = true;
        /// <summary>
        /// Enable vibration for channel. Default value is true.
        /// </summary>
        public bool EnableVibration { get; set; } = true;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface IChannelOptions
    {
        string Name { get; }
        string Id { get; }
        NotificationImportance NotificationImportance { get; }
        bool ShowBadge { get; }
        bool EnableVibration { get; }
    }
}

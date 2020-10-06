using System;
using Windows.UI.Notifications;

namespace Plugin.Toast
{
    public sealed partial class ToastId : IEquatable<ToastId>
    {
        public string Tag { get; }
        public string Group { get; }

        public ToastId(string tag, string group)
        {
            Tag = tag ?? "";
            Group = group ?? "";
        }

        public static ToastId FromNotification(ToastNotification toastNotification)
            => new ToastId(
                tag: toastNotification.Tag,
                group: toastNotification.Group
                );

        public static ToastId FromNotification(ScheduledToastNotification toastNotification)
            => new ToastId(
                tag: toastNotification.Tag,
                group: toastNotification.Group
                );

        (string tag, string? group) AsTuple() => (Tag, Group);

        bool PlatformEquals(ToastId? other) => other != null && AsTuple() == other.AsTuple();
        bool PlatformEquals(object? obj) => PlatformEquals(obj as ToastId);
        private int PlatformGetHashCode() => AsTuple().GetHashCode();
        private string PlatformToString() => string.Format("Tag: {0}, Group: {1}", Tag, Group);

        int GetPlatformPersistentHashCode()
            => CombineHashCode(CombineHashCode(KMagicSeed, Tag), Group);
    }
}

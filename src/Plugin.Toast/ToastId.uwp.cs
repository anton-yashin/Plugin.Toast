using System;
using Windows.UI.Notifications;

namespace Plugin.Toast
{
    public sealed partial class ToastId : IEquatable<ToastId>
    {
        public string Tag { get; }
        public string? Group { get; }
        public ToastIdNotificationType NotificationType { get; }

        public ToastId(string tag, string? group, ToastIdNotificationType notificationType)
        {
            Tag = tag;
            Group = group;
            NotificationType = notificationType;
        }

        public static ToastId FromNotification(ToastNotification toastNotification)
            => new ToastId(
                tag: toastNotification.Tag,
                group: string.IsNullOrEmpty(toastNotification.Group) ? null : toastNotification.Group,
                notificationType: ToastIdNotificationType.ToastNotification
                );

        public static ToastId FromNotification(ScheduledToastNotification toastNotification)
            => new ToastId(
                tag: toastNotification.Tag,
                group: string.IsNullOrEmpty(toastNotification.Group) ? null : toastNotification.Group,
                notificationType: ToastIdNotificationType.ScheduledToastNotification
                );

        (string tag, string? group, ToastIdNotificationType notificationType) AsTuple()
            => (Tag, Group, NotificationType);

        bool PlatformEquals(ToastId? other) => other != null && AsTuple() == other.AsTuple();
        bool PlatformEquals(object? obj) => PlatformEquals(obj as ToastId);
        private int PlatformGetHashCode() => AsTuple().GetHashCode();
        private string PlatformToString() => string.Format("Tag: {0}, Group: {1}, NotificationType: {2}", Tag, Group, NotificationType);

        int GetPlatformPersistentHashCode()
            => CombineHashCode(CombineHashCode(CombineHashCode(KMagicSeed, Tag), Group), (int)NotificationType);
    }

    public enum ToastIdNotificationType
    {
        Unknown,
        ToastNotification,
        ScheduledToastNotification,
    }
}

using System;
using Windows.UI.Notifications;

namespace Plugin.Toast
{
    public sealed partial class ToastId
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
                group: toastNotification.Group,
                notificationType: ToastIdNotificationType.ToastNotification
                );

        public static ToastId FromNotification(ScheduledToastNotification toastNotification)
            => new ToastId(
                tag: toastNotification.Tag,
                group: toastNotification.Group,
                notificationType: ToastIdNotificationType.ScheduledToastNotification
                );
    }

    public enum ToastIdNotificationType
    {
        Unknown,
        ToastNotification,
        ScheduledToastNotification,
    }
}

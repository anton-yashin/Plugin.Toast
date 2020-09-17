using UserNotifications;

namespace Plugin.Toast
{
    public abstract partial class ToastImageSource
    {
        internal UNNotificationAttachment Attachment { get; }

        protected internal ToastImageSource(UNNotificationAttachment attachment)
            => Attachment = attachment;
    }

    sealed class SealedToastImageSource : ToastImageSource
    {
        public SealedToastImageSource(UNNotificationAttachment attachment) : base(attachment)
        {
        }
    }
}

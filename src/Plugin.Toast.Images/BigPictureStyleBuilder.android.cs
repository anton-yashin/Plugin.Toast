using AndroidX.Core.App;
using System;

namespace Plugin.Toast.Images
{
    sealed class BigPictureStyleBuilder : IBigPictureStyle
    {
        private readonly NotificationCompat.BigPictureStyle style;

        public BigPictureStyleBuilder() => style = new NotificationCompat.BigPictureStyle();

        public IBigPictureStyle BigLargeIcon(ToastImageSource b)
        {
            style.BigLargeIcon(b.Bitmap);
            return this;
        }

        public IBigPictureStyle BigPicture(ToastImageSource b)
        {
            style.BigPicture(b.Bitmap);
            return this;
        }

        public IBigPictureStyle SetBigContentTitle(string title)
        {
            style.SetBigContentTitle(title);
            return this;
        }

        public IBigPictureStyle SetSummaryText(string cs)
        {
            style.SetSummaryText(cs);
            return this;
        }

        public NotificationCompat.Style Build() => style;

    }
}

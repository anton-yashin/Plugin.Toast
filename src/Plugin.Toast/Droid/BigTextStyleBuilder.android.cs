using AndroidX.Core.App;
using System;

namespace Plugin.Toast.Droid
{
    sealed class BigTextStyleBuilder : IBigTextStyle
    {
        private readonly NotificationCompat.BigTextStyle style;

        public BigTextStyleBuilder() => style = new NotificationCompat.BigTextStyle();


        public IBigTextStyle BigText(string cs)
        {
            style.BigText(cs);
            return this;
        }

        public IBigTextStyle SetBigContentTitle(string title)
        {
            style.SetBigContentTitle(title);
            return this;
        }

        public IBigTextStyle SetSummaryText(string cs)
        {
            style.SetSummaryText(cs);
            return this;
        }

        public NotificationCompat.Style Build() => style;
    }
}

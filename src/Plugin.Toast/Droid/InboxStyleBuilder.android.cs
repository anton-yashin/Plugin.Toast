using AndroidX.Core.App;
using System;

namespace Plugin.Toast.Droid
{
    sealed class InboxStyleBuilder : IInboxStyle
    {
        private readonly NotificationCompat.InboxStyle style;

        public InboxStyleBuilder() => style = new NotificationCompat.InboxStyle();

        public IInboxStyle AddLine(string cs)
        {
            style.AddLine(cs);
            return this;
        }

        public IInboxStyle SetBigContentTitle(string title)
        {
            style.SetBigContentTitle(title);
            return this;
        }

        public IInboxStyle SetSummaryText(string cs)
        {
            style.SetSummaryText(cs);
            return this;
        }

        public NotificationCompat.Style Build() => style;
    }
}

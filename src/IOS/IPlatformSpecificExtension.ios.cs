using Foundation;
using System;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    public interface IPlatformSpecificExtension : IIosNotificationExtension, IBuilderExtension<IPlatformSpecificExtension>
    {
        IPlatformSpecificExtension AddAttachments(UNNotificationAttachment[] attachments);
        IPlatformSpecificExtension AddBadgeNumber(NSNumber number);
        IPlatformSpecificExtension AddSound(UNNotificationSound sound);
        IPlatformSpecificExtension AddSummaryArgumentCount(nuint SummaryArgumentCount);
    }
}

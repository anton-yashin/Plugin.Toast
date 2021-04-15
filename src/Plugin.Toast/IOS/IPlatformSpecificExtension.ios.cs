using Foundation;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    public interface IPlatformSpecificExtension : IIosNotificationExtension, IBuilderExtension<IPlatformSpecificExtension>
    {
        IPlatformSpecificExtension AddAttachments(IEnumerable<UNNotificationAttachment> attachments);
        IPlatformSpecificExtension AddAttachment(UNNotificationAttachment attachment);
        IPlatformSpecificExtension AddBadgeNumber(NSNumber number);
        IPlatformSpecificExtension AddSound(UNNotificationSound sound);
        IPlatformSpecificExtension AddSummaryArgumentCount(nuint SummaryArgumentCount);
    }
}

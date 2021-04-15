using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface IIosNotificationExtension : INotificationBuilderExtension<IIosNotificationExtension>
    {
        IIosNotificationExtension AddBadgeNumber(int number);
        IIosNotificationExtension AddBody(string body);
        IIosNotificationExtension AddCategoryIdentifier(string categoryIdentifier);
        IIosNotificationExtension AddLaunchImageName(string launchImageName);
        IIosNotificationExtension AddSummaryArgument(string summaryArgument);
        IIosNotificationExtension AddSummaryArgumentCount(ulong SummaryArgumentCount);
        IIosNotificationExtension AddTargetContentIdentifier(string targetContentIdentifier);
        IIosNotificationExtension AddThreadIdentifier(string threadIdentifier);
        IIosNotificationExtension WithCustomArg(string key, string value);
        IIosNotificationExtension WithCustomArgs(IEnumerable<(string key, string value)> args);
    }
}

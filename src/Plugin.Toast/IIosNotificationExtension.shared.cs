using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// The interface to the ios notification builder.
    /// <seealso href="https://docs.microsoft.com/en-us/dotnet/api/usernotifications.unmutablenotificationcontent"/>
    /// <seealso href="https://developer.apple.com/documentation/usernotifications/unmutablenotificationcontent"/>
    /// </summary>
    public interface IIosNotificationExtension : INotificationBuilderExtension<IIosNotificationExtension>
    {
        /// <summary>
        /// Sets the number to display in the app's icon badge.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        IIosNotificationExtension AddBadgeNumber(int number);
        /// <summary>
        /// Sets the message that is displayed in the notification alert.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        IIosNotificationExtension AddBody(string body);
        /// <summary>
        /// Sets an application-defined category object identifier.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        IIosNotificationExtension AddCategoryIdentifier(string categoryIdentifier);
        /// <summary>
        /// Gets or sets the name of an image that is stored in the application's bundle to
        /// display when the user launches the application from the notification.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        IIosNotificationExtension AddLaunchImageName(string launchImageName);
        /// <summary>
        /// Sets the notification-specific addition to the category summary string.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        IIosNotificationExtension AddSummaryArgument(string summaryArgument);
        /// <summary>
        /// Sets the number of arguments that the notification adds to the category summary string.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        IIosNotificationExtension AddSummaryArgumentCount(ulong SummaryArgumentCount);
        /// <summary>
        /// Sets application-defined content identifier.
        /// </summary>
        IIosNotificationExtension AddTargetContentIdentifier(string targetContentIdentifier);
        /// <summary>
        /// Sets an application-specific identifier that is used to group notifications.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        IIosNotificationExtension AddThreadIdentifier(string threadIdentifier);
        /// <summary>
        /// Adds a custom data to notification.
        /// </summary>
        IIosNotificationExtension WithCustomArg(string key, string value);
        /// <summary>
        /// Adds a custom data to notification.
        /// </summary>
        IIosNotificationExtension WithCustomArgs(IEnumerable<(string key, string value)> args);
    }
}

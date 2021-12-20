using Foundation;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Versioning;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    /// <summary>
    /// The interface to the ios notification builder. Contains platform dependent methods.
    /// <seealso href="https://docs.microsoft.com/en-us/dotnet/api/usernotifications.unmutablenotificationcontent"/>
    /// <seealso href="https://developer.apple.com/documentation/usernotifications/unmutablenotificationcontent"/>
    /// </summary>
    public interface IPlatformSpecificExtension : IIosNotificationExtension, INotificationBuilderExtension<IPlatformSpecificExtension>
    {
        /// <summary>
        /// Adds a set of <see cref="UserNotifications.UNNotificationAttachment"/> objects
        /// that contains the attachmets for the notification.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        IPlatformSpecificExtension AddAttachments(IEnumerable<UNNotificationAttachment> attachments);
        /// <summary>
        /// Adds a <see cref="UserNotifications.UNNotificationAttachment"/> object
        /// that contain the attachmet for the notification.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        IPlatformSpecificExtension AddAttachment(UNNotificationAttachment attachment);
        /// <summary>
        /// Sets the number to display in the app's icon badge.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        IPlatformSpecificExtension AddBadgeNumber(NSNumber number);
        /// <summary>
        /// Sets the sound that is played when the notification is triggered.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        IPlatformSpecificExtension AddSound(UNNotificationSound sound);
        /// <summary>
        /// Sets the number of arguments that the notification adds to the category summary string.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        [UnsupportedOSPlatform("watchos")]
        [UnsupportedOSPlatform("tvos")]
        [SupportedOSPlatform("ios12.0")]
        [UnsupportedOSPlatform("ios15.0")]
        IPlatformSpecificExtension AddSummaryArgumentCount(nuint SummaryArgumentCount);
    }
}

using System;

namespace Plugin.Toast.UWP
{
    /// <summary>
    /// The interface is proxy for <see cref="Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder"/>
    /// and contains functions specific to UWP.
    /// </summary>
    public interface IPlatformSpecificExtension : IUwpExtension, INotificationBuilderExtension<IPlatformSpecificExtension>
    {
    }
}

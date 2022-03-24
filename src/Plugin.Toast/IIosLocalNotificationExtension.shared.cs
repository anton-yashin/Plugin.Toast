using System;
using System.Collections.Generic;
using System.Runtime.Versioning;

namespace Plugin.Toast
{
    /// <summary>
    /// The interface allow to access to the legacy api using UIApplication
    /// </summary>
    [UnsupportedOSPlatform("tvos")]
    [UnsupportedOSPlatform("ios10.0")]
    public interface IIosLocalNotificationExtension : INotificationBuilderExtension<IIosLocalNotificationExtension>
    {
        /// <summary>
        /// The name of the sound file to play when the alert is displayed.
        /// </summary>
        /// <remarks>
        /// Portions of this page are reproduced from work created and shared by
        /// the <see href="https://github.com/xamarin/apple-api-docs">Xamarin Apple API docs</see>
        /// Project and used according to terms described in the Creative Commons
        /// 4.0 Attribution International License. 
        /// </remarks>
        IIosLocalNotificationExtension AddSoundName(string soundName);
        /// <summary>
        /// Adds a custom data to notification.
        /// </summary>
        IIosLocalNotificationExtension WithCustomArg(string key, string value);
        /// <summary>
        /// Adds a custom data to notification.
        /// </summary>
        IIosLocalNotificationExtension WithCustomArgs(IEnumerable<(string key, string value)> args);
    }
}

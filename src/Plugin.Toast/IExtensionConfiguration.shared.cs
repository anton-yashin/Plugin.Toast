using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Abstractions
{
    /// <summary>
    /// A configuration that applied after a <see cref="IBuilder"/> is created.
    /// Create an implementation and register it in your IoC or use with <see cref="IBuilderExtension{T}.Use(IExtensionConfiguration{T})"/>
    /// </summary>
    /// <typeparam name="T">Type of platform extension. One of <see cref="IDroidNotificationExtension"/>, 
    /// <see cref="ISnackbarExtension"/>, <see cref="IIosNotificationExtension"/>, <see cref="IIosLocalNotificationExtension"/>
    /// <see cref="IUwpExtension"/>, <see cref="Plugin.Toast.Droid.IPlatformSpecificExtension"/>,
    /// <see cref="Plugin.Toast.IOS.IPlatformSpecificExtension"/>, <see cref="Plugin.Toast.UWP.IPlatformSpecificExtension"/></typeparam>
    public interface IExtensionConfiguration<T>
        where T : IBuilderExtension<T>
    {
        /// <summary>
        /// build a notification using <see cref="IBuilderExtension{T}"/>
        /// </summary>
        void Configure(T builderExtension);
    }
}

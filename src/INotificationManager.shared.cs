using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public interface INotificationManager
    {
        /// <summary>
        /// Initialize current platform if need.
        /// </summary>
        /// <exception cref="Exceptions.NotificationException"/>
        Task InitializeAsync();

        /// <summary>
        /// Build a notification using a default platform extensions.
        /// </summary>
        /// <returns>Notification builder</returns>
        IBuilder BuildNotification();

        /// <summary>
        /// Build a notification using a specific platform extension.
        /// </summary>
        /// <typeparam name="T">An An extension type that used if available on current platform</typeparam>
        /// <returns>Notification builder</returns>
        IBuilder BuildNotificationUsing<T>()
            where T : IBuilderExtension<T>;

        /// <summary>
        /// Build a notification using a specific platform extensions. 
        /// </summary>
        /// <typeparam name="T1">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T2">An extension type that used if available on current platform</typeparam>
        /// <returns>Notification builder</returns>
        IBuilder BuildNotificationUsing<T1, T2>()
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>;

        /// <summary>
        /// Build a notification using a specific platform extensions. 
        /// </summary>
        /// <typeparam name="T1">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T2">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T3">An extension type that used if available on current platform</typeparam>
        /// <returns>Notification builder</returns>
        IBuilder BuildNotificationUsing<T1, T2, T3>()
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
            where T3 : IBuilderExtension<T3>;

        /// <summary>
        /// Build a notification using a specific platform extensions. 
        /// </summary>
        /// <typeparam name="T1">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T2">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T3">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T4">An extension type that used if available on current platform</typeparam>
        /// <returns>Notification builder</returns>
        IBuilder BuildNotificationUsing<T1, T2, T3, T4>()
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
            where T3 : IBuilderExtension<T3>
            where T4 : IBuilderExtension<T4>;
    }
}
using Plugin.Toast.Abstractions;
using System;
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
        /// Create a notification builder using a default platform extensions.
        /// </summary>
        /// <returns>Notification builder</returns>
        INotificationBuilder GetBuilder();

        /// <summary>
        /// Create a notification builder using a specific platform extension.
        /// </summary>
        /// <typeparam name="T">An An extension type that used if available on current platform</typeparam>
        /// <returns>Notification builder</returns>
        INotificationBuilder GetBuilder<T>()
            where T : INotificationBuilderExtension<T>;

        /// <summary>
        /// Create a notification builder using a specific platform extensions. 
        /// </summary>
        /// <typeparam name="T1">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T2">An extension type that used if available on current platform</typeparam>
        /// <returns>Notification builder</returns>
        INotificationBuilder GetBuilder<T1, T2>()
            where T1 : INotificationBuilderExtension<T1>
            where T2 : INotificationBuilderExtension<T2>;

        /// <summary>
        /// Create a notification builder using a specific platform extensions. 
        /// </summary>
        /// <typeparam name="T1">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T2">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T3">An extension type that used if available on current platform</typeparam>
        /// <returns>Notification builder</returns>
        INotificationBuilder GetBuilder<T1, T2, T3>()
            where T1 : INotificationBuilderExtension<T1>
            where T2 : INotificationBuilderExtension<T2>
            where T3 : INotificationBuilderExtension<T3>;

        /// <summary>
        /// Create a notification builder using a specific platform extensions. 
        /// </summary>
        /// <typeparam name="T1">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T2">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T3">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T4">An extension type that used if available on current platform</typeparam>
        /// <returns>Notification builder</returns>
        INotificationBuilder GetBuilder<T1, T2, T3, T4>()
            where T1 : INotificationBuilderExtension<T1>
            where T2 : INotificationBuilderExtension<T2>
            where T3 : INotificationBuilderExtension<T3>
            where T4 : INotificationBuilderExtension<T4>;
    }
}
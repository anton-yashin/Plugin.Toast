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
        IBuilder GetBuilder();

        /// <summary>
        /// Create a notification builder using a specific platform extension.
        /// </summary>
        /// <typeparam name="T">An An extension type that used if available on current platform</typeparam>
        /// <returns>Notification builder</returns>
        IBuilder GetBuilder<T>()
            where T : IBuilderExtension<T>;

        /// <summary>
        /// Create a notification builder using a specific platform extensions. 
        /// </summary>
        /// <typeparam name="T1">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T2">An extension type that used if available on current platform</typeparam>
        /// <returns>Notification builder</returns>
        IBuilder GetBuilder<T1, T2>()
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>;

        /// <summary>
        /// Create a notification builder using a specific platform extensions. 
        /// </summary>
        /// <typeparam name="T1">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T2">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T3">An extension type that used if available on current platform</typeparam>
        /// <returns>Notification builder</returns>
        IBuilder GetBuilder<T1, T2, T3>()
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
            where T3 : IBuilderExtension<T3>;

        /// <summary>
        /// Create a notification builder using a specific platform extensions. 
        /// </summary>
        /// <typeparam name="T1">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T2">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T3">An extension type that used if available on current platform</typeparam>
        /// <typeparam name="T4">An extension type that used if available on current platform</typeparam>
        /// <returns>Notification builder</returns>
        IBuilder GetBuilder<T1, T2, T3, T4>()
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
            where T3 : IBuilderExtension<T3>
            where T4 : IBuilderExtension<T4>;
    }

    public static class INotificationManagerExtensions
    {
        /// <summary>
        /// Wrapper for <see cref="INotificationManager.GetBuilder()"/>
        /// </summary>
        [Obsolete("Use GetBuilder")]
        public static IBuilder BuildNotification(this INotificationManager @this)
        {
            return @this.GetBuilder();
        }

        /// <summary>
        /// Wrapper for <see cref="INotificationManager.GetBuilder{T}"/>
        /// </summary>
        [Obsolete("Use GetBuilder<T>")]
        public static IBuilder BuildNotificationUsing<T>(this INotificationManager @this)
            where T : IBuilderExtension<T>
        {
            return @this.GetBuilder<T>();
        }

        /// <summary>
        /// Wrapper for <see cref="INotificationManager.GetBuilder{T1, T2}"/>
        /// </summary>
        [Obsolete("Use GetBuilder<T1, T2>")]
        public static IBuilder BuildNotificationUsing<T1, T2>(this INotificationManager @this)
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
        {
            return @this.GetBuilder<T1, T2>();
        }

        /// <summary>
        /// Wrapper for <see cref="INotificationManager.GetBuilder{T1, T2, T3}"/>
        /// </summary>
        [Obsolete("Use GetBuilder<T1, T2, T3>")]
        public static IBuilder BuildNotificationUsing<T1, T2, T3>(this INotificationManager @this)
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
            where T3 : IBuilderExtension<T3>
        {
            return @this.GetBuilder<T1, T2, T3>();
        }

        /// <summary>
        /// Wrapper for <see cref="INotificationManager.GetBuilder{T1, T2, T3, T4}"/>
        /// </summary>
        [Obsolete("Use GetBuilder<T1, T2, T3>")]
        public static IBuilder BuildNotificationUsing<T1, T2, T3, T4>(this INotificationManager @this)
            where T1 : IBuilderExtension<T1>
            where T2 : IBuilderExtension<T2>
            where T3 : IBuilderExtension<T3>
            where T4 : IBuilderExtension<T4>
        {
            return @this.GetBuilder<T1, T2, T3, T4>();
        }

    }

}
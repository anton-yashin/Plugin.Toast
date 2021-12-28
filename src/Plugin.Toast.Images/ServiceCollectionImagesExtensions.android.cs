using Android.Graphics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plugin.Toast.Abstractions;
using Plugin.Toast.Droid;
using Plugin.Toast.Images;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    /// <summary>
    /// Android specific extensions for <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionImagesExtensions
    {
        /// <summary>
        /// Add the notification manager image support to a service collection
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <remarks>
        /// Usage:
        /// <code>
        /// using Xamarin.Forms.Platform.Android;<br/>
        /// // ...<br/>
        /// serviceCollection.AddNotificationManagerImagesSupport(fn => Resources.GetBitmapAsync(fn))));<br/>
        /// // or <br/>
        /// serviceCollection.AddNotificationManagerImagesSupport(fn => global::Android.App.Application.Context.Resources.GetBitmapAsync(fn));
        /// </code>
        /// <br/>
        /// Following services will be included to collection:<br/>
        /// <seealso cref="IExtensionPlugin{TExtension, T1, T2, T3}"/>,<br/>
        /// <seealso cref="IImageCacher"/>,<br/>
        /// <seealso cref="IToastImageSourceFactory"/>,<br/>
        /// <seealso cref="HttpClientFactoryServiceCollectionExtensions.AddHttpClient(IServiceCollection)"/>.
        /// </remarks>
        public static IServiceCollection AddNotificationManagerImagesSupport(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IExtensionPlugin<IPlatformSpecificExtension, ToastImageSource, Router.Route>, DroidImageRouter>();
            services.TryAddSingleton<IImageCacher, ImageCacher>();
            services.TryAddSingleton<IToastImageSourceFactory, ToastImageSourceFactory>();
            services.TryAddTransient<IBigPictureStyle, BigPictureStyleBuilder>();
            services.TryAddTransient<IMessagingStyle, MessagingStyleBuilder>();
            services.AddHttpClient();
            return services;
        }
    }
}

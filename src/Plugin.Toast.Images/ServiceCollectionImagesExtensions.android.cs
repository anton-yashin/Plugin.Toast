using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plugin.Toast.Droid;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public static class ServiceCollectionImagesExtensions
    {
        /// <summary>
        /// Add the notification manager image support to a service collection
        /// </summary>
        /// <remarks>
        /// Following services will be included to collection:<br/>
        /// <seealso cref="IExtensionPlugin{TExtension, T1, T2, T3}"/>,<br/>
        /// <seealso cref="IImageCacher"/>,<br/>
        /// <seealso cref="IToastImageSourceFactory"/>,<br/>
        /// <seealso cref="HttpClientFactoryServiceCollectionExtensions.AddHttpClient(IServiceCollection)"/>.
        /// </remarks>
        public static IServiceCollection AddNotificationManagerImagesSupport(this IServiceCollection services)
        {
            services.TryAddSingleton<IExtensionPlugin<IPlatformSpecificExtension, ToastImageSource, Router.Route>, DroidImageRouter>();
            services.TryAddSingleton<IImageCacher, ImageCacher>();
            services.TryAddSingleton<IToastImageSourceFactory, ToastImageSourceFactory>();
            services.AddHttpClient();
            return services;
        }
    }
}

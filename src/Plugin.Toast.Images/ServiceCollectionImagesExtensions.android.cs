﻿using Android.Graphics;
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
        /// <remarks>
        /// Following services will be included to collection:<br/>
        /// <seealso cref="IExtensionPlugin{TExtension, T1, T2, T3}"/>,<br/>
        /// <seealso cref="IImageCacher"/>,<br/>
        /// <seealso cref="IToastImageSourceFactory"/>,<br/>
        /// <seealso cref="HttpClientFactoryServiceCollectionExtensions.AddHttpClient(IServiceCollection)"/>.
        /// </remarks>
        public static IServiceCollection AddNotificationManagerImagesSupport(
            this IServiceCollection services,
            Func<string, Task<Bitmap>> resourceNameToBitmap)
        {
            services.TryAddSingleton<IExtensionPlugin<IPlatformSpecificExtension, ToastImageSource, Router.Route>, DroidImageRouter>();
            services.TryAddSingleton<IImageCacher, ImageCacher>();
            services.TryAddSingleton<IToastImageSourceFactory, ToastImageSourceFactory>();
            services.TryAddSingleton<IResourceToBitmap>(sc => new ResourceToBitmap(resourceNameToBitmap));
            services.AddHttpClient();
            return services;
        }
    }
}

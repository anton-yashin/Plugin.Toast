﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plugin.Toast.Abstractions;
using Plugin.Toast.Images;
using Plugin.Toast.UWP;
using System;

namespace Plugin.Toast
{
    /// <summary>
    /// UWP specific extension for <see cref="IServiceCollection"/>
    /// </summary>
    public static class ServiceCollectionImagesExtensions
    {
        /// <summary>
        /// Add the notification manager image support to a service collection
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="getImageDirectory">The function that returns windows image directory</param>
        /// <remarks>
        /// Usage:
        /// <code>
        /// serviceCollection.AddNotificationManagerImagesSupport(Xamarin.Forms.Application.Current.OnThisPlatform().GetImageDirectory)
        /// </code>
        /// Following services will be included to collection:<br/>
        /// <seealso cref="IExtensionPlugin{TExtension, T1, T2, T3}"/>,<br/>
        /// <seealso cref="IImageCacher"/>,<br/>
        /// <seealso cref="IToastImageSourceFactory"/>,<br/>
        /// <seealso cref="IResourceToFileNameStrategy"/>,<br/>
        /// </remarks>
        public static IServiceCollection AddNotificationManagerImagesSupport(
            this IServiceCollection services,
            Func<string?> getImageDirectory)
        {
            services.TryAddSingleton<IExtensionPlugin<IPlatformSpecificExtension, ToastImageSource, Router.Route>, UwpImageRouter>();
            services.TryAddSingleton<IImageCacher, ImageCacher>();
            services.TryAddSingleton<IToastImageSourceFactory, ToastImageSourceFactory>();
            services.TryAddSingleton<IResourceToFileNameStrategy, ResourceToFileNameStrategy>();
            services.TryAddSingleton<IImageDirectoryResolver>(sc => new ImageDirectoryResolver(getImageDirectory));
            return services;
        }

        /// <summary>
        /// Add the notification manager image support to a service collection
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <remarks>
        /// Usage:
        /// <code>
        /// serviceCollection.AddNotificationManagerImagesSupport(Xamarin.Forms.Application.Current.OnThisPlatform().GetImageDirectory)
        /// </code>
        /// Following services will be included to collection:<br/>
        /// <seealso cref="IExtensionPlugin{TExtension, T1, T2, T3}"/>,<br/>
        /// <seealso cref="IImageCacher"/>,<br/>
        /// <seealso cref="IToastImageSourceFactory"/>,<br/>
        /// <seealso cref="IResourceToFileNameStrategy"/>,<br/>
        /// </remarks>
        public static IServiceCollection AddNotificationManagerImagesSupport(
            this IServiceCollection services)
        {
            services.TryAddSingleton<IExtensionPlugin<IPlatformSpecificExtension, ToastImageSource, Router.Route>, UwpImageRouter>();
            services.TryAddSingleton<IImageCacher, ImageCacher>();
            services.TryAddSingleton<IToastImageSourceFactory, ToastImageSourceFactory>();
            services.TryAddSingleton<IResourceToFileNameStrategy, ResourceToFileNameStrategy>();
            services.TryAddSingleton<IImageDirectoryResolver>(sc => new ImageDirectoryResolver(() => ""));
            return services;
        }

    }
}

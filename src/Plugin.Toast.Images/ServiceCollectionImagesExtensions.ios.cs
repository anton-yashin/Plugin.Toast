﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plugin.Toast.IOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public static class ServiceCollectionImagesExtensions
    {
        public static IServiceCollection AddNotificationManagerImagesSupport(this IServiceCollection services)
        {
            services.AddSingleton<IOsImageRouter>();
            services.TryAddSingleton<IExtensionPlugin<IPlatformSpecificExtension, ToastImageSource, Router.Route>>(sp => sp.GetRequiredService<IOsImageRouter>());
            services.TryAddSingleton<IExtensionPlugin<IPlatformSpecificExtension, IEnumerable<ToastImageSource>, Router.Route>>(sp => sp.GetRequiredService<IOsImageRouter>());
            services.TryAddSingleton<IImageCacher, ImageCacher>();
            services.TryAddSingleton<IToastImageSourceFactory, ToastImageSourceFactory>();
            services.TryAddSingleton<IUriToFileNameStrategy, UriToFileNameStrategy>();
            services.TryAddSingleton<IBundleToFileNameStrategy, BundleToFileNameStrategy>();
            services.TryAddSingleton<IResourceToFileNameStrategy, ResourceToFileNameStrategy>();
            services.TryAddSingleton<IMimeDetector, MimeDetector>();
            services.AddHttpClient();
            return services;
        }
    }
}
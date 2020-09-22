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

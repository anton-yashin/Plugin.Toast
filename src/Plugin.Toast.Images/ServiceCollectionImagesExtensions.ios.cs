using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Plugin.Toast.Abstractions;
using Plugin.Toast.IOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// iOS specific extensions for <see cref="IServiceCollection"/>
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
        /// <seealso cref="IUriToFileNameStrategy"/>,<br/>
        /// <seealso cref="IBundleToFileNameStrategy"/>,<br/>
        /// <seealso cref="IResourceToFileNameStrategy"/>,<br/>
        /// <seealso cref="IMimeDetector"/>,<br/>
        /// <seealso cref="HttpClientFactoryServiceCollectionExtensions.AddHttpClient(IServiceCollection)"/>.
        /// </remarks>
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

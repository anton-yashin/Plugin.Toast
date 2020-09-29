﻿using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    /// <summary>
    /// The factory which can create a <see cref="ToastImageSource"/> used by <see cref="BuilderExtensions"/>
    /// </summary>
    public interface IToastImageSourceFactory
    {
        /// <summary>
        /// Creates a image source from <see cref="Uri"/>
        /// </summary>
        /// <param name="uri">a uri to image</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Image source</returns>
        /// <remarks>
        /// On Android and iOS this function uses a <seealso cref="System.Net.Http.IHttpClientFactory"/>
        /// to create a <see cref="System.Net.Http.HttpClient"/> to download image.
        /// <br/>
        /// On Android this function uses a <seealso cref="Xamarin.Forms.ImageSource.FromStream(Func{System.IO.Stream})"/>
        /// <br/>
        /// On iOS this function also uses a <seealso cref="IImageCacher"/> to get file name that can be used to create
        /// UNNotificationAttachment. File name in cache generated by <seealso cref="IUriToFileNameStrategy"/>. There also
        /// <seealso cref="IMimeDetector"/> that used to detect a mime type, that used to provide a UTI hint
        /// <br/>
        /// UWP does have image size restrictions. <see cref="IUwpExtension"/>
        /// </remarks>
        public Task<ToastImageSource> FromUriAsync(Uri uri, CancellationToken cancellationToken = default);
        /// <summary>
        /// Creates a image source from file or bundle image.
        /// </summary>
        /// <param name="filePath">Path to file or name in bundle</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Image source</returns>
        /// <exception cref="System.IO.FileNotFoundException"/>
        /// <remarks>
        /// On Android this function uses <see cref="Xamarin.Forms.ImageSource.FromFile(string)"/>
        /// <br/>
        /// On iOS this function will search a file in file system, if not found it will search it in app bundle.
        /// If file found in bundle then it will be written to cache using <seealso cref="IImageCacher"/>,
        /// with name generated by <see cref="IBundleToFileNameStrategy"/>
        /// </remarks>
        public Task<ToastImageSource> FromFileAsync(string filePath, CancellationToken cancellationToken = default);
        /// <summary>
        /// Creates a image source from embedded resource.
        /// </summary>
        /// <param name="resourcePath">Resource path</param>
        /// <param name="assembly">Assembly that contains resource</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Image source</returns>
        /// <remarks>
        /// On Android this function uses <see cref="Xamarin.Forms.ImageSource.FromResource(string, Assembly)(string)"/>
        /// <br/>
        /// On iOS and UWP this function will write a image to cache using <seealso cref="IImageCacher"/>,
        /// with name generated by <see cref="IResourceToFileNameStrategy"/>
        /// Don't use <see cref="Assembly.GetCallingAssembly()"/> it is not supported by UWP AOT.
        /// <br/>
        /// See also <seealso cref="ToastImageSourceFactoryExtensions.FromResourceAsync(IToastImageSourceFactory, string, Type, CancellationToken)"/>
        /// </remarks>
        public Task<ToastImageSource> FromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken = default);
    }

    public static class ToastImageSourceFactoryExtensions
    {
        /// <summary>
        /// Creates a image source from embedded resource.
        /// </summary>
        /// <param name="resourcePath">Resource path</param>
        /// <param name="resolvingType">A type from the assembly in which to look up the image resource with resource.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Image source</returns>
        /// <remarks>
        /// On Android this function uses <see cref="Xamarin.Forms.ImageSource.FromResource(string, Assembly)(string)"/>
        /// <br/>
        /// On iOS and UWP this function will write a image to cache using <seealso cref="IImageCacher"/>,
        /// with name generated by <see cref="IResourceToFileNameStrategy"/>
        /// </remarks>
        public static Task<ToastImageSource> FromResourceAsync(
            this IToastImageSourceFactory toastImageSourceFactory,
            string resourcePath,
            Type resolvingType,
            CancellationToken cancellationToken = default)
        {
            return toastImageSourceFactory.FromResourceAsync(resourcePath, resolvingType.GetTypeInfo().Assembly, cancellationToken);
        }
    }

}

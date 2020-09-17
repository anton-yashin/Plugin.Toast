using Android.Graphics;
using Plugin.Toast.Exceptions;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

namespace Plugin.Toast
{
    public sealed partial class ToastImageSource
    {
        internal Bitmap Bitmap { get; }

        private ToastImageSource()
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        private ToastImageSource(Bitmap bitmap) => this.Bitmap = bitmap;

        static Task<ToastImageSource> PlatformFromFileAsync(string filePath, CancellationToken cancellationToken)
            => NewAsync(ImageSource.FromFile(filePath), cancellationToken);

        static Task<ToastImageSource> PlatformFromUriAsync(Uri uri, CancellationToken cancellationToken)
            => NewAsync(ImageSource.FromUri(uri), cancellationToken);

        static Task<ToastImageSource> PlatformFromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken)
            => NewAsync(ImageSource.FromResource(resourcePath, assembly), cancellationToken);

        static async Task<ToastImageSource> NewAsync(ImageSource imageSource, CancellationToken cancellationToken)
        {
            var handler = Registrar.Registered.GetHandlerForObject<IImageSourceHandler>(imageSource);
            if (handler != null)
            {
                var bitmap = await handler.LoadImageAsync(imageSource, Android.App.Application.Context, cancellationToken);
                if (bitmap == null)
                    throw new ArgumentException("Image data was invalid", nameof(imageSource));
                return new ToastImageSource(bitmap);
            }
            throw new InvalidOperationException("please init Xamarin Forms before call this function");
        }

        static string GetCacheFolderPath()
            => Android.App.Application.Context.CacheDir?.AbsolutePath
            ?? Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    }
}

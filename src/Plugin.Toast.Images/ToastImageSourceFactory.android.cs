using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

namespace Plugin.Toast
{
    sealed partial class ToastImageSourceFactory : IToastImageSourceFactory
    {
        public ToastImageSourceFactory()
        {
        }

        public Task<ToastImageSource> FromUriAsync(Uri uri, CancellationToken cancellationToken = default)
            => CreateAsync(ImageSource.FromUri(uri), cancellationToken);

        public Task<ToastImageSource> FromFileAsync(string filePath, CancellationToken cancellationToken = default)
            => CreateAsync(ImageSource.FromFile(filePath), cancellationToken);

        public Task<ToastImageSource> FromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken = default)
            => CreateAsync(ImageSource.FromResource(resourcePath, assembly), cancellationToken);

        async Task<ToastImageSource> CreateAsync(ImageSource imageSource, CancellationToken cancellationToken)
        {
            var handler = Registrar.Registered.GetHandlerForObject<IImageSourceHandler>(imageSource)
                ?? throw new InvalidOperationException("please init Xamarin Forms before call this function");
            var bitmap = await handler.LoadImageAsync(imageSource, Android.App.Application.Context, cancellationToken);
            if (bitmap == null)
                throw new ArgumentException("Image data was invalid", nameof(imageSource));
            return new SealedToastImageSource(bitmap);
        }

    }
}

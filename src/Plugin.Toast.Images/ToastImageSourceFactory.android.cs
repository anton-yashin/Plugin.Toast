using System;
using System.Net.Http;
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
        private readonly IHttpClientFactory httpClientFactory;

        public ToastImageSourceFactory(IHttpClientFactory httpClientFactory)
            => this.httpClientFactory = httpClientFactory;

        public async Task<ToastImageSource> FromUriAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            var hc = httpClientFactory.CreateClient(nameof(IToastImageSourceFactory));
            using (var response = await hc.GetAsync(uri))
            {
                using (var src = await response.Content.ReadAsStreamAsync())
                return await CreateAsync(ImageSource.FromStream(() => src), cancellationToken);
            }
        }

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

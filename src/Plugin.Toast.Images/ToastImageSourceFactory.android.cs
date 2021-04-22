using Android.Graphics;
using Plugin.Toast.Images;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    sealed partial class ToastImageSourceFactory : IToastImageSourceFactory
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IResourceToBitmap resourceToBitmap;

        public ToastImageSourceFactory(IHttpClientFactory httpClientFactory, IResourceToBitmap resourceToBitmap)
        {
            this.httpClientFactory = httpClientFactory;
            this.resourceToBitmap = resourceToBitmap;
        }

        public async Task<ToastImageSource> FromUriAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            var hc = httpClientFactory.CreateClient(nameof(IToastImageSourceFactory));
            using (var response = await hc.GetAsync(uri, cancellationToken))
            {
                using (var src = await response.Content.ReadAsStreamAsync())
                {
                    var bitmap = await BitmapFactory.DecodeStreamAsync(src).ConfigureAwait(false)
                        ?? throw new ArgumentException("Image data was invalid", nameof(uri));
                    return new SealedToastImageSource(bitmap);
                }
            }
        }

        public async Task<ToastImageSource> FromFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            var bitmap = (File.Exists(filePath) 
                ? await BitmapFactory.DecodeFileAsync(filePath).ConfigureAwait(false)
                : await resourceToBitmap.GetBitmapAsync(filePath))
                ?? throw new ArgumentException("Image data was invalid", nameof(filePath));
            return new SealedToastImageSource(bitmap);
        }

        public async Task<ToastImageSource> FromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken = default)
        {
            using (var src = assembly.GetManifestResourceStream(resourcePath))
            {
                var bitmap = await BitmapFactory.DecodeStreamAsync(src).ConfigureAwait(false)
                    ?? throw new ArgumentException("Image data was invalid", nameof(assembly));
                return new SealedToastImageSource(bitmap);
            }
        }
    }
}

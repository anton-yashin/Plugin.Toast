using Android.Graphics;
using Plugin.Toast.Droid.Configuration;
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
        private readonly IActivityConfiguration activityConfiguration;
        private readonly IPackageNameConfiguration packageNameConfiguration;

        public ToastImageSourceFactory(
            IHttpClientFactory httpClientFactory,
            IActivityConfiguration activityConfiguration,
            IPackageNameConfiguration packageNameConfiguration)
        {
            this.httpClientFactory = httpClientFactory;
            this.activityConfiguration = activityConfiguration;
            this.packageNameConfiguration = packageNameConfiguration;
        }

        public async Task<ToastImageSource> FromUriAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            var hc = httpClientFactory.CreateClient(nameof(IToastImageSourceFactory));
            using (var response = await hc.GetAsync(uri, cancellationToken))
            {
                using (var src = await response.Content.ReadAsStreamAsync())
                {
                    var bitmap = await BitmapFactory.DecodeStreamAsync(src).ConfigureAwait(false)
                        ?? throw new ArgumentException($"Image data was invalid: [{uri}]", nameof(uri));
                    return new SealedToastImageSource(bitmap);
                }
            }
        }

        public async Task<ToastImageSource> FromFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            var bitmap = await GetBitmapAsync(filePath).ConfigureAwait(false)
                ?? await BitmapFactory.DecodeFileAsync(filePath).ConfigureAwait(false)
                ?? throw new ArgumentException($"Image data was invalid: [{filePath}]", nameof(filePath));
            return new SealedToastImageSource(bitmap);
        }

        Task<Bitmap?> GetBitmapAsync(string fileName)
        {
            var context = activityConfiguration.Activity;
            var title = System.IO.Path.GetFileNameWithoutExtension(fileName).ToLowerInvariant();
            var resId = context.Resources?.GetIdentifier(title, "drawable", packageNameConfiguration.PackageName) ?? 0;
            return resId > 0
                ? BitmapFactory.DecodeResourceAsync(context.Resources, resId)
                : Task.FromResult<Bitmap?>(null);
        }

        public async Task<ToastImageSource> FromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken = default)
        {
            using (var src = assembly.GetManifestResourceStream(resourcePath))
            {
                var bitmap = await BitmapFactory.DecodeStreamAsync(src).ConfigureAwait(false)
                    ?? throw new ArgumentException($"Image data was invalid [{assembly}/{resourcePath}]", nameof(assembly));
                return new SealedToastImageSource(bitmap);
            }
        }
    }
}

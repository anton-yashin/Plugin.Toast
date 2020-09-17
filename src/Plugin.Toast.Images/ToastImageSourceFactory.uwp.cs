using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace Plugin.Toast
{
    sealed partial class ToastImageSourceFactory : IToastImageSourceFactory
    {
        private readonly IImageCacher imageCacher;

        public ToastImageSourceFactory(IImageCacher imageCacher)
            => this.imageCacher = imageCacher;

        public Task<ToastImageSource> FromUriAsync(Uri uri, CancellationToken cancellationToken = default)
            => Task.FromResult<ToastImageSource>(new SealedToastImageSource(uri));

        public Task<ToastImageSource> FromFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            _ = filePath ?? throw new ArgumentNullException(nameof(filePath));
            filePath = CombineWithImageDirectory(filePath);
            var fullPath = Path.GetFullPath(filePath);
            var uri = "file:///" + fullPath.Replace("\\", "/");
            return Task.FromResult<ToastImageSource>(new SealedToastImageSource(new Uri(uri)));
        }

        internal static string CombineWithImageDirectory(string filePath)
        {
            var imageDirectory = Xamarin.Forms.Application.Current.OnThisPlatform().GetImageDirectory();
            if (string.IsNullOrEmpty(imageDirectory) == false)
            {
                var directory = Path.GetDirectoryName(filePath);
                if (string.IsNullOrEmpty(directory) || Path.GetFullPath(directory).Equals(Path.GetFullPath(imageDirectory)) == false)
                    return Path.Combine(imageDirectory, filePath);
            }
            return filePath;
        }

        public async Task<ToastImageSource> FromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken = default)
        {
            var asn = assembly.GetName();
            var fullFn = await imageCacher.CacheAsync(
                Path.Combine("ToastImageSource.FromResource/", asn.Name + "_" + asn.Version + "_" + resourcePath),
                cancellationToken,
                async (stream, ct) =>
                {
                    using (var mrs = assembly.GetManifestResourceStream(resourcePath))
                        await mrs.CopyToAsync(stream, 80 * 1024, ct);
                });
            var result = await FromFileAsync(fullFn, cancellationToken);
            return result;
        }
    }
}

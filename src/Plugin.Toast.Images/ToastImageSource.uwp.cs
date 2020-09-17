using Plugin.Toast.Exceptions;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace Plugin.Toast
{
    public sealed partial class ToastImageSource
    {
        internal Uri ImageUri { get; }

        private ToastImageSource()
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        private ToastImageSource(Uri image) => this.ImageUri = image;

        static Task<ToastImageSource> PlatformFromFileAsync(string filePath, CancellationToken cancellationToken)
        {
            _ = filePath ?? throw new ArgumentNullException(nameof(filePath));
            filePath = CombineWithImageDirectory(filePath);
            var fullPath = Path.GetFullPath(filePath);
            var uri = "file:///" + fullPath.Replace("\\", "/");
            return Task.FromResult(new ToastImageSource(new Uri(uri)));
        }

        static string CombineWithImageDirectory(string filePath)
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

        static Task<ToastImageSource> PlatformFromUriAsync(Uri uri, CancellationToken cancellationToken)
            => Task.FromResult(new ToastImageSource(uri));

        static async Task<ToastImageSource> PlatformFromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken)
        {
            var asn = assembly.GetName();
            var fullFn = await CacheAsync(
                Path.Combine("ToastImageSource.FromResource/", asn.Name + "_" + asn.Version + "_" + resourcePath),
                cancellationToken,
                async (stream, ct) =>
                {
                    using (var mrs = assembly.GetManifestResourceStream(resourcePath))
                        await mrs.CopyToAsync(stream, 80 * 1024, ct);
                });
            var result = await PlatformFromFileAsync(fullFn, cancellationToken);
            return result;
        }

        static string GetCacheFolderPath() => Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path;
    }
}

using Plugin.Toast.Images;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    sealed partial class ToastImageSourceFactory : IToastImageSourceFactory
    {
        private readonly IImageCacher imageCacher;
        private readonly IResourceToFileNameStrategy resourceToFileNameStrategy;
        private readonly IImageDirectoryResolver imageDirectoryResolver;

        public ToastImageSourceFactory(
            IImageCacher imageCacher,
            IResourceToFileNameStrategy resourceToFileNameStrategy,
            IImageDirectoryResolver imageDirectoryResolver)
        {
            this.imageCacher = imageCacher;
            this.resourceToFileNameStrategy = resourceToFileNameStrategy;
            this.imageDirectoryResolver = imageDirectoryResolver;
        }

        public Task<ToastImageSource> FromUriAsync(Uri uri, CancellationToken cancellationToken = default)
            => Task.FromResult<ToastImageSource>(new SealedToastImageSource(uri));

        public Task<ToastImageSource> FromFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            _ = filePath ?? throw new ArgumentNullException(nameof(filePath));
            filePath = CombineWithImageDirectory(imageDirectoryResolver.GetImageDirectory(), filePath);
            var fullPath = Path.GetFullPath(filePath);
            var uri = "file:///" + fullPath.Replace("\\", "/");
            return Task.FromResult<ToastImageSource>(new SealedToastImageSource(new Uri(uri)));
        }

        internal static string CombineWithImageDirectory(string? imageDirectory, string filePath)
        {
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
                resourceToFileNameStrategy.Convert(resourcePath, assembly),
                cancellationToken, () => assembly.GetManifestResourceStream(resourcePath));
            var result = await FromFileAsync(fullFn, cancellationToken);
            return result;
        }
    }
}

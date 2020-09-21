using Foundation;
using MobileCoreServices;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using UserNotifications;

namespace Plugin.Toast
{
    sealed partial class ToastImageSourceFactory : IToastImageSourceFactory
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IImageCacher imageCacher;
        private readonly IUriToFileNameStrategy uriToFileNameStrategy;
        private readonly IResourceToFileNameStrategy resourceToFileNameStrategy;
        private readonly IBundleToFileNameStrategy bundleToFileNameStrategy;
        private readonly IMimeDetector mimeDetector;

        public ToastImageSourceFactory(
            IHttpClientFactory httpClientFactory,
            IImageCacher imageCacher,
            IUriToFileNameStrategy uriToFileNameStrategy,
            IResourceToFileNameStrategy resourceToFileNameStrategy,
            IBundleToFileNameStrategy bundleToFileNameStrategy,
            IMimeDetector mimeDetector)
        {
            this.httpClientFactory = httpClientFactory;
            this.imageCacher = imageCacher;
            this.uriToFileNameStrategy = uriToFileNameStrategy;
            this.resourceToFileNameStrategy = resourceToFileNameStrategy;
            this.bundleToFileNameStrategy = bundleToFileNameStrategy;
            this.mimeDetector = mimeDetector;
        }

        public async Task<ToastImageSource> FromUriAsync(Uri uri, CancellationToken cancellationToken = default)
        {
            string contentType = "";
            var fullFn = await imageCacher.CacheAsync(uriToFileNameStrategy.Convert(uri), cancellationToken, async (stream, ct) =>
            {
                var hc = httpClientFactory.CreateClient(nameof(IToastImageSourceFactory));
                using (var response = await hc.GetAsync(uri))
                {
                    contentType = response.Content.Headers.ContentType.MediaType;
                    using (var src = await response.Content.ReadAsStreamAsync())
                        await src.CopyToAsync(stream, 1024 * 80, cancellationToken);
                }
            });
            if (string.IsNullOrEmpty(contentType))
            {
                using (var fs = File.OpenRead(fullFn))
                    contentType = await mimeDetector.DetectAsync(fs);
            }
            return new SealedToastImageSource(CreateAttachment(uri.ToString(), NSUrl.FromFilename(fullFn),
                UTType.CreatePreferredIdentifier(UTType.TagClassMIMEType, contentType, null)));
        }

        UNNotificationAttachment CreateAttachment(string id, NSUrl url, string? typeHint = null)
        {
            var options = new UNNotificationAttachmentOptions() { TypeHint = typeHint };
            var attachment = UNNotificationAttachment.FromIdentifier(id, url, options, out var error);

            if (error != null)
                throw new InvalidOperationException("got error while creating attachment: " + error.ToString());
            if (attachment == null)
                throw new InvalidOperationException("cant create attachment");
            return attachment;
        }


        public Task<ToastImageSource> FromFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            return File.Exists(filePath)
                ? Task.FromResult<ToastImageSource>(new SealedToastImageSource(CreateAttachment(Guid.NewGuid().ToString(), NSUrl.FromFilename(filePath))))
                : FromBundleAsync(filePath, cancellationToken);
        }

        async Task<ToastImageSource> FromBundleAsync(string bundlePath, CancellationToken cancellationToken)
        {
            var image = UIImage.FromBundle(bundlePath);
            if (image == null)
                throw new FileNotFoundException("file not found", bundlePath);

            var fullFn = await imageCacher.CacheAsync(bundleToFileNameStrategy.Convert(bundlePath), cancellationToken, image.AsPNG().AsStream);
            return new SealedToastImageSource(CreateAttachment(bundlePath, NSUrl.FromFilename(fullFn), "public.png"));
        }

        public async Task<ToastImageSource> FromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken = default)
        {
            var asn = assembly.GetName();
            var fullFn = await imageCacher.CacheAsync(
                resourceToFileNameStrategy.Convert(resourcePath, assembly),
                cancellationToken, () => assembly.GetManifestResourceStream(resourcePath));
            return await FromFileAsync(fullFn, cancellationToken);
        }
    }
}

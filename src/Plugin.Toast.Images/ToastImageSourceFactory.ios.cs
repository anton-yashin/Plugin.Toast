using Foundation;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using UserNotifications;
using MUTType = MobileCoreServices.UTType;
using UUTType = UniformTypeIdentifiers.UTType;

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
                    contentType = response.Content.Headers.ContentType?.MediaType ?? "";
                    using (var src = await response.Content.ReadAsStreamAsync())
                        await src.CopyToAsync(stream, 1024 * 80, cancellationToken);
                }
            });
            if (string.IsNullOrEmpty(contentType))
                contentType = await GetMimeType(fullFn);

            return new SealedToastImageSource(CreateAttachment(
                uri.ToString(),
                NSUrl.FromFilename(fullFn),
                GetTypeHintByMime(contentType)));
        }

        async Task<string> GetMimeType(string fullFileName)
        {
            using (var fs = File.OpenRead(fullFileName))
            {
                var contentType = await mimeDetector.DetectAsync(fs);
                return contentType;
            }
        }

        string GetTypeHintByMime(string mime)
        {
#if NET6_0_OR_GREATER
            if (OperatingSystem.IsIOSVersionAtLeast(14, 0))
#else
            if (UIDevice.CurrentDevice.CheckSystemVersion(14, 0))
#endif
            {
                var utt = UUTType.CreateFromMimeType(mime)
                    ?? throw new InvalidOperationException($"can't create UTType by mime: [{mime}]");
                return utt.Identifier;
            }
            else
            {
                return MUTType.CreatePreferredIdentifier(MUTType.TagClassMIMEType, mime, null);
            }
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


        public async Task<ToastImageSource> FromFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            if (File.Exists(filePath))
            {
                var contentType = await GetMimeType(filePath);
                return new SealedToastImageSource(
                    CreateAttachment(filePath, NSUrl.FromFilename(filePath), GetTypeHintByMime(contentType)));
            }
            else
                return await FromBundleAsync(filePath, cancellationToken);
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
                cancellationToken, () => assembly.GetManifestResourceStream(resourcePath)
                ?? throw new ArgumentException($"Can't read the resource: [{resourcePath}]", nameof(resourcePath)));
            return await FromFileAsync(fullFn, cancellationToken);
        }
    }
}

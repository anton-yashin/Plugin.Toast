using Foundation;
using MobileCoreServices;
using Plugin.Toast.Exceptions;
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
    public sealed partial class ToastImageSource
    {
        internal UNNotificationAttachment Attachment { get; }

        private ToastImageSource()
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        private ToastImageSource(UNNotificationAttachment attachment)
            => Attachment = attachment;

        static Task<ToastImageSource> PlatformFromFileAsync(string filePath, CancellationToken cancellationToken)
        {
            return File.Exists(filePath)
                ? Task.FromResult(new ToastImageSource(CreateAttachment(Guid.NewGuid().ToString(), NSUrl.FromFilename(filePath))))
                : FromBundleAsync(filePath, cancellationToken);
        }

        static UNNotificationAttachment CreateAttachment(string id, NSUrl url, string? typeHint = null)
        {
            var options = new UNNotificationAttachmentOptions() { TypeHint = typeHint };
            var attachment = UNNotificationAttachment.FromIdentifier(id, url, options, out var error);

            if (error != null)
                throw new InvalidOperationException("got error while creating attachment: " + error.ToString());
            if (attachment == null)
                throw new InvalidOperationException("cant create attachment");
            return attachment;
        }

        static async Task<ToastImageSource> FromBundleAsync(string filePath, CancellationToken cancellationToken)
        {
            var image = UIImage.FromBundle(filePath);
            if (image == null)
                throw new FileNotFoundException("file not found", filePath);

            const string KFolder = "ToastImageSouce.FromBundle/";
            var fullFn = await ImageCacher.CacheAsync(Path.Combine(KFolder, filePath), cancellationToken, async (stream, ct) =>
                {
                    using (var src = image.AsPNG().AsStream())
                        await src.CopyToAsync(stream, 1024 * 80, ct);
                });
            return new ToastImageSource(CreateAttachment(filePath, NSUrl.FromFilename(fullFn), "public.png"));
        }

        static async Task<ToastImageSource> PlatformFromUriAsync(Uri uri, CancellationToken cancellationToken)
        {
            const string KFolder = "ToastImageSouce.FromUri/";
            string contentType = "";
            var subfolder = uri.Scheme + "+++" + uri.Host + "/";
            var fn = uri.PathAndQuery;
            foreach (var i in Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()))
            {
                fn = fn.Replace(i.ToString(), "+" + (int)i);
            }
            var fullFn = await ImageCacher.CacheAsync(Path.Combine(KFolder, subfolder, fn), cancellationToken, async (stream, ct) =>
            {
                using (var hc = new HttpClient())
                using (var response = await hc.GetAsync(uri))
                {
                    contentType = response.Content.Headers.ContentType.MediaType;
                    using (var src = await response.Content.ReadAsStreamAsync())
                        await src.CopyToAsync(stream, 1024 * 80, cancellationToken);
                }
            });
            return new ToastImageSource(CreateAttachment(uri.ToString(), NSUrl.FromFilename(fullFn),
                UTType.CreatePreferredIdentifier(UTType.TagClassMIMEType, contentType, null)));
        }

        static async Task<ToastImageSource> PlatformFromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken)
        {
            var asn = assembly.GetName();
            var fullFn = await ImageCacher.CacheAsync(
                Path.Combine("ToastImageSource.FromResource/", asn.Name + "_" + asn.Version + "_" + resourcePath),
                cancellationToken,
                async (stream, ct) =>
                {
                    using (var mrs = assembly.GetManifestResourceStream(resourcePath))
                        await mrs.CopyToAsync(stream, 80 * 1024, ct);
                });
            return await PlatformFromFileAsync(fullFn, cancellationToken);
        }
    }
}

using Foundation;
using MobileCoreServices;
using Plugin.Toast.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
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
            NSUrl url;

            if (File.Exists(filePath))
                url = NSUrl.FromFilename(filePath);
            else
                return FromBundleAsync(filePath, cancellationToken);
            var attachment = UNNotificationAttachment.FromIdentifier(Guid.NewGuid().ToString(), url, new UNNotificationAttachmentOptions(), out var error);
            if (error != null)
            {
                throw new InvalidOperationException("got error while creating attachment: " + error.ToString());
            }
            if (attachment == null)
                throw new InvalidOperationException("cant create attachment");
            return Task.FromResult(new ToastImageSource(attachment));
        }

        static async Task<ToastImageSource> FromBundleAsync(string filePath, CancellationToken cancellationToken)
        {
            var image = UIImage.FromBundle(filePath);
            if (image == null)
                throw new FileNotFoundException("file not found", filePath);

            const string KFolder = "ToastImageSouce.FromBundle/";
            var fullFn = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), KFolder, filePath);
            if (File.Exists(fullFn) == false)
            {
                var newFn = fullFn + ".new";
                try
                {
                    var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), KFolder);
                    if (Directory.Exists(folder) == false)
                        Directory.CreateDirectory(folder);
                    using (var file = File.Create(newFn))
                    {
                        await image.AsPNG().AsStream().CopyToAsync(file, 1024 * 80, cancellationToken);
                        await file.FlushAsync(cancellationToken);
                    }
                    File.Move(newFn, fullFn);
                }
                catch (OperationCanceledException)
                {
                    File.Delete(newFn);
                    throw;
                }
            }
            var attachment = UNNotificationAttachment.FromIdentifier(filePath, NSUrl.FromFilename(fullFn), new UNNotificationAttachmentOptions() { TypeHint = "public.png" }, out var error);
            if (error != null)
            {
                throw new InvalidOperationException("got error while creating attachment: " + error.ToString());
            }
            if (attachment == null)
                throw new InvalidOperationException("cant create attachment");
            return new ToastImageSource(attachment);
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
            var fullFn = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), KFolder, subfolder, fn);
            if (File.Exists(fullFn) == false)
            {
                var newFn = fullFn + ".new";
                try
                {
                    var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), KFolder, subfolder);
                    if (Directory.Exists(folder) == false)
                        Directory.CreateDirectory(folder);
                    using (var file = File.Create(newFn))
                    using (var hc = new HttpClient())
                    using (var response = await hc.GetAsync(uri))
                    {
                        contentType = response.Content.Headers.ContentType.MediaType;
                        using (var src = await response.Content.ReadAsStreamAsync())
                            await src.CopyToAsync(file, 1024 * 80, cancellationToken);
                        await file.FlushAsync(cancellationToken);
                    }
                    File.Move(newFn, fullFn);
                }
                catch (OperationCanceledException)
                {
                    File.Delete(newFn);
                    throw;
                }
            }
            var typeHint = UTType.CreatePreferredIdentifier(UTType.TagClassMIMEType, contentType, null);
            var attachment = UNNotificationAttachment.FromIdentifier(uri.ToString(), NSUrl.FromFilename(fullFn), new UNNotificationAttachmentOptions() { TypeHint = typeHint }, out var error);
            if (error != null)
            {
                throw new InvalidOperationException("got error while creating attachment: " + error.ToString());
            }
            if (attachment == null)
                throw new InvalidOperationException("cant create attachment");
            return new ToastImageSource(attachment);
        }

        static async Task<ToastImageSource> PlatformFromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken)
        {
            const string KFolder = "ToastImageSource.FromResource/";
            var asn = assembly.GetName();
            var fn = asn.Name + "_" + asn.Version + "_" + resourcePath;
            var fullFn = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), KFolder, fn);
            if (File.Exists(fullFn) == false)
            {
                var newFn = fullFn + ".new";
                try
                {
                    var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), KFolder);
                    if (Directory.Exists(folder) == false)
                        Directory.CreateDirectory(folder);
                    using (var file = File.Create(newFn))
                    {
                        var src = assembly.GetManifestResourceStream(resourcePath);
                        await src.CopyToAsync(file, 1024 * 80, cancellationToken);
                        await file.FlushAsync(cancellationToken);
                    }
                    File.Move(newFn, fullFn);
                }
                catch (OperationCanceledException)
                {
                    File.Delete(newFn);
                    throw;
                }
            }
            var result = await PlatformFromFileAsync(fullFn, cancellationToken);
            return result;
        }
    }
}

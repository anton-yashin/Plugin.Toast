using Plugin.Toast.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
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

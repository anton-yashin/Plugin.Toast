using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    sealed partial class ImageCacher : IImageCacher
    {
        public async Task<string> CacheAsync(string relativePath, CancellationToken cancellationToken, Func<Stream, CancellationToken, Task> copyToAsync)
        {
            var fullFn = Path.Combine(GetCacheFolderPath(), relativePath);
            if (File.Exists(fullFn) == false)
            {
                var newFn = fullFn + ".new";
                try
                {
                    var folder = Path.GetDirectoryName(fullFn);
                    if (Directory.Exists(folder) == false)
                        Directory.CreateDirectory(folder);
                    using (var file = File.Create(newFn))
                    {
                        await copyToAsync(file, cancellationToken);
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
            return fullFn;
        }
    }

    internal interface IImageCacher
    {
        Task<string> CacheAsync(string relativePath, CancellationToken cancellationToken, Func<Stream, CancellationToken, Task> copyToAsync);
    }
}

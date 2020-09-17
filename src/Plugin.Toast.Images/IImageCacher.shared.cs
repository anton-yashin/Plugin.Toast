using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public interface IImageCacher
    {
        Task<string> CacheAsync(string relativePath, CancellationToken cancellationToken, Func<Stream, CancellationToken, Task> copyToAsync);
    }
}

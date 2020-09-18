using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public static class IImageCacherExtensions
    {
        public static Task<string> CacheAsync(this IImageCacher @this, string relativePath, Func<Stream, CancellationToken, Task> copyToAsync)
            => @this.CacheAsync(relativePath, default, copyToAsync);

        public static Task<string> CacheAsync(this IImageCacher @this, string relativePath, CancellationToken cancellationToken, Func<CancellationToken, Task<Stream>> getStreamAsync)
        {
            return @this.CacheAsync(relativePath, cancellationToken, async (s, ct) =>
            {
                using (var stream = await getStreamAsync(ct))
                    await stream.CopyToAsync(s, 80 * 1024, ct);
            });
        }

        public static Task<string> CacheAsync(this IImageCacher @this, string relativePath, Func<CancellationToken, Task<Stream>> getStreamAsync)
            => @this.CacheAsync(relativePath, default, getStreamAsync);

        public static Task<string> CacheAsync(this IImageCacher @this, string relativePath, CancellationToken cancellationToken, Func<Stream> getStream)
        {
            return @this.CacheAsync(relativePath, cancellationToken, async (s, ct) =>
            {
                using (var stream = getStream())
                    await stream.CopyToAsync(s, 80 * 1024, ct);
            });
        }

        public static Task<string> CacheAsync(this IImageCacher @this, string relativePath, Func<Stream> getStream)
            => @this.CacheAsync(relativePath, default, getStream);
    }

}

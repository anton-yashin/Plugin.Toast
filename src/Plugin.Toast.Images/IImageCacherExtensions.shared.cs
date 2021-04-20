using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    /// <summary>
    /// Extensions to <see cref="IImageCacher"/>
    /// </summary>
    public static class IImageCacherExtensions
    {
        /// <summary>
        /// Write a data to local cache folder.
        /// </summary>
        /// <param name="this">The image cacher</param>
        /// <param name="relativePath">Path relative to temporary folder to file where a data will be stored</param>
        /// <param name="copyToAsync">Callback which will write to provided file stream</param>
        /// <returns>Full path to file where a cached data is stored</returns>
        /// <remarks>
        /// This function will create a subfolder and a file if not exists in path relative to cache folder. Then
        /// open it and call a copyToAsync callback with a file stream as a parameter.
        /// </remarks>
        public static Task<string> CacheAsync(this IImageCacher @this, string relativePath, Func<Stream, CancellationToken, Task> copyToAsync)
            => @this.CacheAsync(relativePath, default, copyToAsync);

        /// <summary>
        /// Write a data to local cache folder.
        /// </summary>
        /// <param name="this">The image cacher</param>
        /// <param name="relativePath">Path relative to temporary folder to file where a data will be stored</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="getStreamAsync">Provide a stream which must be written to cache</param>
        /// <returns>Full path to file where a cached data is stored</returns>
        /// <remarks>
        /// This function will create a subfolder and a file if not exists in path relative to cache folder. Then
        /// open it and call a getStreamAsync callback to copy an incomming data.
        /// </remarks>
        public static Task<string> CacheAsync(this IImageCacher @this, string relativePath, CancellationToken cancellationToken, Func<CancellationToken, Task<Stream>> getStreamAsync)
        {
            return @this.CacheAsync(relativePath, cancellationToken, async (s, ct) =>
            {
                using (var stream = await getStreamAsync(ct))
                    await stream.CopyToAsync(s, 80 * 1024, ct);
            });
        }

        /// <summary>
        /// Write a data to local cache folder.
        /// </summary>
        /// <param name="this">The image cacher</param>
        /// <param name="relativePath">Path relative to temporary folder to file where a data will be stored</param>
        /// <param name="getStreamAsync">Provide a stream which must be written to cache</param>
        /// <returns>Full path to file where a cached data is stored</returns>
        /// <remarks>
        /// This function will create a subfolder and a file if not exists in path relative to cache folder. Then
        /// open it and call a getStreamAsync callback to copy an incomming data.
        /// </remarks>
        public static Task<string> CacheAsync(this IImageCacher @this, string relativePath, Func<CancellationToken, Task<Stream>> getStreamAsync)
            => @this.CacheAsync(relativePath, default, getStreamAsync);

        /// <summary>
        /// Write a data to local cache folder.
        /// </summary>
        /// <param name="this">The image cacher</param>
        /// <param name="relativePath">Path relative to temporary folder to file where a data will be stored</param>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <param name="getStream">Provide a stream which must be written to cache</param>
        /// <returns>Full path to file where a cached data is stored</returns>
        /// <remarks>
        /// This function will create a subfolder and a file if not exists in path relative to cache folder. Then
        /// open it and call a getStream callback to copy an incomming data.
        /// </remarks>
        public static Task<string> CacheAsync(this IImageCacher @this, string relativePath, CancellationToken cancellationToken, Func<Stream> getStream)
        {
            return @this.CacheAsync(relativePath, cancellationToken, async (s, ct) =>
            {
                using (var stream = getStream())
                    await stream.CopyToAsync(s, 80 * 1024, ct);
            });
        }

        /// <summary>
        /// Write a data to local cache folder.
        /// </summary>
        /// <param name="this">The image cacher</param>
        /// <param name="relativePath">Path relative to temporary folder to file where a data will be stored</param>
        /// <param name="getStream">Provide a stream which must be written to cache</param>
        /// <returns>Full path to file where a cached data is stored</returns>
        /// <remarks>
        /// This function will create a subfolder and a file if not exists in path relative to cache folder. Then
        /// open it and call a getStream callback to copy an incomming data.
        /// </remarks>
        public static Task<string> CacheAsync(this IImageCacher @this, string relativePath, Func<Stream> getStream)
            => @this.CacheAsync(relativePath, default, getStream);
    }

}

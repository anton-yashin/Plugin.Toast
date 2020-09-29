using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    /// <summary>
    /// A caching service, used by <see cref="IToastImageSourceFactory"/> on iOS and UWP platforms
    /// due the system restrictions
    /// </summary>
    public interface IImageCacher
    {
        /// <summary>
        /// Write a data into local cache folder.
        /// </summary>
        /// <param name="relativePath">Path relative to temporary folder to file where a data will be stored</param>
        /// <param name="copyToAsync">Callback which will write to provided file stream</param>
        /// <returns>Full path to file where a cached data is stored</returns>
        /// <remarks>
        /// This function will create a subfolder and a file if not exists in path relative to cache folder. Then
        /// open it and call a copyToAsync callback with a file stream as a parameter.
        /// <seealso cref="IImageCacherExtensions"/>
        /// </remarks>
        Task<string> CacheAsync(string relativePath, CancellationToken cancellationToken, Func<Stream, CancellationToken, Task> copyToAsync);
    }
}

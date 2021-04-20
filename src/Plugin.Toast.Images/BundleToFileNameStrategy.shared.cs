using System;
using System.IO;

namespace Plugin.Toast
{
    sealed class BundleToFileNameStrategy : IBundleToFileNameStrategy
    {
        internal const string KFolder = "ToastImageSource.FromBundle/";

        public string Convert(string bundlePath)
            => Path.Combine(KFolder, bundlePath);
    }

    /// <summary>
    /// Provides conversion from bundle file name to local file name.
    /// You can override default behaviour by registering in 
    /// IoC a custom implementation. Used by iOS implementation of
    /// <see cref="IToastImageSourceFactory.FromFileAsync(string, System.Threading.CancellationToken)"/>
    /// to cache bundle images.
    /// </summary>
    public interface IBundleToFileNameStrategy
    {
        /// <summary>
        /// Provides conversion from bundle file name to local file name.
        /// </summary>
        /// <param name="bundlePath"></param>
        /// <returns></returns>
        string Convert(string bundlePath);
    }
}

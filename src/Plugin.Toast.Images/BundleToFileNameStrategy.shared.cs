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
    /// Provides conversion from bundle file name to file name inside
    /// the application home folder. You can override default behaviour
    /// by registering in IoC a custom implementation.
    /// </summary>
    public interface IBundleToFileNameStrategy
    {
        /// <summary>
        /// Provides conversion from bundle file name to file name inside
        /// the application home folder.
        /// </summary>
        /// <param name="bundlePath">The path to the file inside the bundle.</param>
        /// <returns>The path relative to the application home folder.</returns>
        string Convert(string bundlePath);
    }
}

using System;
using System.IO;

namespace Plugin.Toast
{
    sealed class BundleToFileNameStrategy : IBundleToFileNameStrategy
    {
        internal const string KFolder = "ToastImageSouce.FromBundle/";

        public string Convert(string bundlePath)
            => Path.Combine(KFolder, bundlePath);
    }

    /// <summary>
    /// Provides conversion from bundle file name to local file name.
    /// You can override default behaviour by registering in 
    /// IoC a custom implementation.
    /// </summary>
    public interface IBundleToFileNameStrategy
    {
        string Convert(string bundlePath);
    }
}

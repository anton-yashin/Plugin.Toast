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

    public interface IBundleToFileNameStrategy
    {
        string Convert(string bundlePath);
    }
}

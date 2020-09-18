using System;
using System.IO;

namespace Plugin.Toast
{
    sealed class BundleToFileNameStrategy : IBundleToFileNameStrategy
    {
        public string Convert(string bundlePath)
            => Path.Combine("ToastImageSouce.FromBundle/", bundlePath);
    }

    public interface IBundleToFileNameStrategy
    {
        string Convert(string bundlePath);
    }
}

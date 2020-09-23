using System;

namespace Plugin.Toast
{
    sealed partial class ImageCacher
    {
#if NETSTANDARD1_4
        static string GetCacheFolderPath() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
#else
        static string GetCacheFolderPath() => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
#endif
    }
}

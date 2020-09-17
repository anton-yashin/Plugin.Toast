using Foundation;
using System;
using System.Linq;

namespace Plugin.Toast
{
    sealed partial class ImageCacher
    {
        static string GetCacheFolderPath()
            => NSSearchPath.GetDirectories(NSSearchPathDirectory.CachesDirectory, NSSearchPathDomain.User).FirstOrDefault()
            ?? Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    }
}

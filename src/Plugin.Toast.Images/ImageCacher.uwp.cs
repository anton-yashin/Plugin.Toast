using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    sealed partial class ImageCacher
    {
        string GetCacheFolderPath() => Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path;
    }
}

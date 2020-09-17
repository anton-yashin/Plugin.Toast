using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    sealed partial class ImageCacher
    {
        string GetCacheFolderPath()
            => Android.App.Application.Context.CacheDir?.AbsolutePath
            ?? Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    }
}

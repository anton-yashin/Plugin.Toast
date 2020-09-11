using System;

namespace Plugin.Toast
{
    public sealed partial class ToastImageSource
    {
        public static ToastImageSource FromFilePath(string filePath)
            => PlatformFromFilePath(filePath);

        public static ToastImageSource FromUri(string uri) => PlatformFromUri(uri);

        public static ToastImageSource FromUri(Uri uri) => PlatformFromUri(uri);

        public static ToastImageSource FromEmbeddedResource(string resourcePath)
            => PlatformFromEmbeddedResource(resourcePath);
    }
}

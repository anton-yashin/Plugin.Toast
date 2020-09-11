using Plugin.Toast.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public sealed partial class ToastImageSource
    {
        private ToastImageSource()
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        static ToastImageSource PlatformFromFilePath(string filePath)
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        static ToastImageSource PlatformFromUri(string uri)
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        static ToastImageSource PlatformFromUri(Uri uri)
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        static ToastImageSource PlatformFromEmbeddedResource(string resourcePath)
            => throw ExceptionUtils.NotSupportedOrImplementedException;
    }
}

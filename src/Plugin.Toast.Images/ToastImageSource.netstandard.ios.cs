using Plugin.Toast.Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public sealed partial class ToastImageSource
    {
        private ToastImageSource()
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        static Task<ToastImageSource> PlatformFromFileAsync(string filePath, CancellationToken cancellationToken)
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        static Task<ToastImageSource> PlatformFromUriAsync(Uri uri, CancellationToken cancellationToken)
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        static Task<ToastImageSource> PlatformFromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken)
            => throw ExceptionUtils.NotSupportedOrImplementedException;
    }
}

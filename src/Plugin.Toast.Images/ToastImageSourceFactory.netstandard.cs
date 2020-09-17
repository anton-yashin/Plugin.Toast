using Plugin.Toast.Exceptions;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    sealed partial class ToastImageSourceFactory : IToastImageSourceFactory
    {
        public ToastImageSourceFactory()
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        public Task<ToastImageSource> FromUriAsync(Uri uri, CancellationToken cancellationToken = default)
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        public Task<ToastImageSource> FromFileAsync(string filePath, CancellationToken cancellationToken = default)
            => throw ExceptionUtils.NotSupportedOrImplementedException;

        public Task<ToastImageSource> FromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken = default)
            => throw ExceptionUtils.NotSupportedOrImplementedException;
    }
}

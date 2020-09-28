using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public interface IToastImageSourceFactory
    {
        public Task<ToastImageSource> FromUriAsync(Uri uri, CancellationToken cancellationToken = default);
        public Task<ToastImageSource> FromFileAsync(string filePath, CancellationToken cancellationToken = default);
        public Task<ToastImageSource> FromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken = default);
    }

    public static class ToastImageSourceFactoryExtensions
    {
        public static Task<ToastImageSource> FromResourceAsync(
            this IToastImageSourceFactory toastImageSourceFactory,
            string resourcePath,
            Type resolvingType,
            CancellationToken cancellationToken = default)
        {
            return toastImageSourceFactory.FromResourceAsync(resourcePath, resolvingType.GetTypeInfo().Assembly, cancellationToken);
        }
    }

}

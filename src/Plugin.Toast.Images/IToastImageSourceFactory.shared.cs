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

        public static Task<ToastImageSource> FromResourceAsync(
            this IToastImageSourceFactory toastImageSourceFactory,
            string resourcePath,
            CancellationToken cancellationToken = default)
        {
            Assembly assembly;
#if NETSTANDARD1_4
            var callingAssemblyMethod = GetCallingAssemblyMethod();
            if (callingAssemblyMethod == null)
                throw new ArgumentException("Can not find CallingAssembly, pass resolvingType to " + nameof(FromResourceAsync) + " to ensure proper resolution");
            assembly = (Assembly)callingAssemblyMethod.Invoke(null, new object[0]);
#else
            assembly = Assembly.GetCallingAssembly();
#endif
            return toastImageSourceFactory.FromResourceAsync(resourcePath, assembly, cancellationToken);
        }

#if NETSTANDARD1_4

        static MethodInfo? getCallingAssemblyMethod = null;
        static MethodInfo? GetCallingAssemblyMethod()
            => getCallingAssemblyMethod ?? (getCallingAssemblyMethod = typeof(Assembly).GetTypeInfo().GetDeclaredMethod("GetCallingAssembly"));

#endif
    }

}

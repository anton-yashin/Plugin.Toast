using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Plugin.Toast
{
    public sealed partial class ToastImageSource
    {
        public static Task<ToastImageSource> FromFileAsync(string filePath, CancellationToken cancellationToken = default)
            => PlatformFromFileAsync(filePath, cancellationToken);

        public static Task<ToastImageSource> FromUriAsync(string uri)
            => FromUriAsync(new Uri(uri));

        public static Task<ToastImageSource> FromUriAsync(Uri uri, CancellationToken cancellationToken = default)
            => PlatformFromUriAsync(uri, cancellationToken);

        public static Task<ToastImageSource> FromResourceAsync(string resourcePath, CancellationToken cancellationToken = default)
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
            return PlatformFromResourceAsync(resourcePath, assembly, cancellationToken);
        }

        public static Task<ToastImageSource> FromResourceAsync(string resourcePath, Type resolvingType, CancellationToken cancellationToken = default)
        {
            return PlatformFromResourceAsync(resourcePath, resolvingType.GetTypeInfo().Assembly, cancellationToken);
        }

        public static Task<ToastImageSource> FromResourceAsync(string resourcePath, Assembly assembly, CancellationToken cancellationToken = default)
            => PlatformFromResourceAsync(resourcePath, assembly, cancellationToken);

#if NETSTANDARD1_4

        static MethodInfo? getCallingAssemblyMethod = null;
        static MethodInfo? GetCallingAssemblyMethod()
            => getCallingAssemblyMethod ?? (getCallingAssemblyMethod = typeof(Assembly).GetTypeInfo().GetDeclaredMethod("GetCallingAssembly"));

#endif
    }
}

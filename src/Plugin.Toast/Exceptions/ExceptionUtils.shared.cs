using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Exceptions
{
    internal sealed class ExceptionUtils
    {
#if NETSTANDARD1_0 || NETSTANDARD2_0
        internal static NotImplementedException NotSupportedOrImplementedException =>
            new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
#else
        internal static NotSupportedException NotSupportedOrImplementedException =>
            new NotSupportedException("This API is not supported on " + Platform);
#endif

        static string Platform =>
#if __ANDROID__
            "Android";
#elif __IOS__
            "iOS";
#elif NETFX_CORE
            "UWP";
#else
            "Unknown";
#endif

        internal static InvalidOperationException BuildTwice => new InvalidOperationException("The build method can't be used more than an once");
    }
}

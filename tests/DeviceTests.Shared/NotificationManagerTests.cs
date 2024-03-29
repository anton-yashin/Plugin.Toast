#if NETCORE_APP == false
#nullable enable

using System;
using Plugin.Toast;
using Xunit;
using System.Threading.Tasks;
#if __ANDROID__
using IXPlatformSpecificExtension = Plugin.Toast.Droid.IPlatformSpecificExtension;
#elif __IOS__
using IXPlatformSpecificExtension = Plugin.Toast.IOS.IPlatformSpecificExtension;
#elif NETFX_CORE || MAUI_WINDOWS
using IXPlatformSpecificExtension = Plugin.Toast.UWP.IPlatformSpecificExtension;
#endif


namespace DeviceTests
{
    public class NotificationManagerTests
    {
        [Fact]
        public void CreateNotificationManager()
        {
            var nm = Platform.CreateNotificationManager();
            Assert.NotNull(nm);
        }

        [Fact]
        public Task CreateDefaultBuidler()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                var nm = Platform.CreateNotificationManager();
                var builder = nm.GetBuilder();

                Assert.NotNull(builder);
                Assert.True(builder is IXPlatformSpecificExtension);
#if __ANDROID__
                Assert.True(builder is IDroidNotificationExtension);
                Assert.True(builder is Plugin.Toast.Droid.IPlatformNotificationBuilder);
#elif __IOS__
                Assert.True(builder is IIosNotificationExtension);
#elif NETFX_CORE
                Assert.True(builder is IUwpExtension);
#endif
            });

        [Fact]
        public Task CreateSpecificBuilderOneArg()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                var nm = Platform.CreateNotificationManager();
                var builder = nm.GetBuilder<ISnackbarExtension>();

                Assert.NotNull(builder);

#if __ANDROID__
                Assert.False(builder is IXPlatformSpecificExtension);
                Assert.True(builder is ISnackbarExtension);
                Assert.False(builder is IDroidNotificationExtension);
                Assert.False(builder is Plugin.Toast.Droid.IPlatformNotificationBuilder);
#elif __IOS__
                Assert.True(builder is IXPlatformSpecificExtension);
                Assert.False(builder is IIosLocalNotificationExtension);
                Assert.True(builder is IIosNotificationExtension);
#elif NETFX_CORE
                Assert.True(builder is IXPlatformSpecificExtension);
                Assert.True(builder is IUwpExtension);
#endif
            });

        [Fact]
        public Task CreateSpecificBuilderTwoArgs()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                var nm = Platform.CreateNotificationManager();
                var builder = nm.GetBuilder<ISnackbarExtension, IIosLocalNotificationExtension>();

                Assert.NotNull(builder);

#if __ANDROID__
                Assert.False(builder is IXPlatformSpecificExtension);
                Assert.True(builder is ISnackbarExtension);
                Assert.False(builder is IDroidNotificationExtension);
                Assert.False(builder is Plugin.Toast.Droid.IPlatformNotificationBuilder);
#elif __IOS__
                Assert.False(builder is IXPlatformSpecificExtension);
                Assert.True(builder is IIosLocalNotificationExtension);
                Assert.False(builder is IIosNotificationExtension);
#elif NETFX_CORE
                Assert.True(builder is IXPlatformSpecificExtension);
                Assert.True(builder is IUwpExtension);
#endif
            });

        [Fact]
        public Task CreateSpecificBuilderMoreThreeArgs()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                var nm = Platform.CreateNotificationManager();
                var builder = nm.GetBuilder<ISnackbarExtension, IIosLocalNotificationExtension, IUwpExtension>();

                Assert.NotNull(builder);

#if __ANDROID__
                Assert.False(builder is IXPlatformSpecificExtension);
                Assert.True(builder is ISnackbarExtension);
                Assert.False(builder is IDroidNotificationExtension);
                Assert.False(builder is Plugin.Toast.Droid.IPlatformNotificationBuilder);
#elif __IOS__
                Assert.False(builder is IXPlatformSpecificExtension);
                Assert.True(builder is IIosLocalNotificationExtension);
                Assert.False(builder is IIosNotificationExtension);
#elif NETFX_CORE
                Assert.True(builder is IXPlatformSpecificExtension);
                Assert.True(builder is IUwpExtension);
#endif
            });

        [Fact]
        public Task CreateSpecificBuilderFourArgs()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                var nm = Platform.CreateNotificationManager();
                var builder = nm.GetBuilder<ISnackbarExtension, IIosLocalNotificationExtension, IUwpExtension, IUwpExtension>();

                Assert.NotNull(builder);

#if __ANDROID__
                Assert.False(builder is IXPlatformSpecificExtension);
                Assert.True(builder is ISnackbarExtension);
                Assert.False(builder is IDroidNotificationExtension);
                Assert.False(builder is Plugin.Toast.Droid.IPlatformNotificationBuilder);
#elif __IOS__
                Assert.False(builder is IXPlatformSpecificExtension);
                Assert.True(builder is IIosLocalNotificationExtension);
                Assert.False(builder is IIosNotificationExtension);
#elif NETFX_CORE
                Assert.True(builder is IXPlatformSpecificExtension);
                Assert.True(builder is IUwpExtension);
#endif
            });

    }
}

#endif
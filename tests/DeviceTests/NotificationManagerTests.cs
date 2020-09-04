using Moq;
using System;
using Plugin.Toast;
using Xunit;
using System.Threading.Tasks;
#if __ANDROID__
using IXPlatformSpecificExtension = Plugin.Toast.Droid.IPlatformSpecificExtension;
#elif __IOS__
using IXPlatformSpecificExtension = Plugin.Toast.IOS.IPlatformSpecificExtension;
#elif NETFX_CORE
using IXPlatformSpecificExtension = Plugin.Toast.UWP.IPlatformSpecificExtension;
#endif


namespace DeviceTests
{
    public class NotificationManagerTests
    {
        [Fact]
        public void ConstructOptions()
        {
#if __ANDROID__
            var to = new ToastOptions(Platform.Activity);
#else
            var to = new ToastOptions();
#endif
            Assert.NotNull(to);
        }

#if __ANDROID__
        [Fact]
        public void CreateOptionsFailed()
        {
            Assert.Throws<ArgumentNullException>(() => new ToastOptions(null!));
        }
#endif

        [Fact]
        public void CreateNotificationManager()
        {
            var nm = Platform.CreateNotificationManager();
            Assert.NotNull(nm);
        }

#if __ANDROID__
        [Fact]
        public void CreateNotificationManagerWithIntentManager()
        {
            var mim = new Mock<Plugin.Toast.Droid.IIntentManager>();
            var nm = new NotificationManager(mim.Object, new ToastOptions(Platform.Activity));
            Assert.NotNull(nm);
        }
#endif

        [Fact]
        public Task CreateDefaultBuidler()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                var nm = Platform.CreateNotificationManager();
                var builder = nm.BuildNotification();

                Assert.NotNull(builder);
                Assert.True(builder is IXPlatformSpecificExtension);
    #if __ANDROID__
                Assert.True(builder is IDroidNotificationExtension);
                Assert.True(builder is Plugin.Toast.Droid.INotificationBuilder);
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
                var builder = nm.BuildNotificationUsing<ISnackbarExtension>();

                Assert.NotNull(builder);

#if __ANDROID__
                Assert.False(builder is IXPlatformSpecificExtension);
                Assert.True(builder is ISnackbarExtension);
                Assert.False(builder is IDroidNotificationExtension);
                Assert.False(builder is Plugin.Toast.Droid.INotificationBuilder);
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
                var builder = nm.BuildNotificationUsing<ISnackbarExtension, IIosLocalNotificationExtension>();

                Assert.NotNull(builder);

#if __ANDROID__
                Assert.False(builder is IXPlatformSpecificExtension);
                Assert.True(builder is ISnackbarExtension);
                Assert.False(builder is IDroidNotificationExtension);
                Assert.False(builder is Plugin.Toast.Droid.INotificationBuilder);
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
                var builder = nm.BuildNotificationUsing<ISnackbarExtension, IIosLocalNotificationExtension, IUwpExtension>();

                Assert.NotNull(builder);

#if __ANDROID__
                Assert.False(builder is IXPlatformSpecificExtension);
                Assert.True(builder is ISnackbarExtension);
                Assert.False(builder is IDroidNotificationExtension);
                Assert.False(builder is Plugin.Toast.Droid.INotificationBuilder);
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
                var builder = nm.BuildNotificationUsing<ISnackbarExtension, IIosLocalNotificationExtension, IUwpExtension, IUwpExtension>();

                Assert.NotNull(builder);

#if __ANDROID__
                Assert.False(builder is IXPlatformSpecificExtension);
                Assert.True(builder is ISnackbarExtension);
                Assert.False(builder is IDroidNotificationExtension);
                Assert.False(builder is Plugin.Toast.Droid.INotificationBuilder);
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
        public void NotificationManagerInit()
        {
            Assert.Throws<InvalidOperationException>(() => NotificationManager.Instance);
#if __ANDROID__
            var to = new ToastOptions(Platform.Activity);
#else
            var to = new ToastOptions();
#endif
            NotificationManager.Init(to);

            Assert.NotNull(NotificationManager.Instance);
        }

    }
}

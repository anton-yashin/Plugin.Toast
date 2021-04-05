#if NETCORE_APP == false
#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Toast;
using Xunit;

namespace DeviceTests
{
    public class BuilderTests
    {
        [Fact]
        public Task WhenUsingDefaultExtensions()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                bool platformInvoked = false;
                bool invalidPlatformInvoked = false;
                var nm = Platform.CreateNotificationManager();
                var builder = nm.GetBuilder();

                builder.WhenUsing<IDroidNotificationExtension>(_ => platformInvoked = true)
                    .WhenUsing<ISnackbarExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<IIosNotificationExtension>(_ => platformInvoked = true)
                    .WhenUsing<IIosLocalNotificationExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<IUwpExtension>(_ => platformInvoked = true);

                Assert.False(invalidPlatformInvoked);
                Assert.True(platformInvoked);
            });

        [Fact]
        public Task WhenUsingSpecificExtensions()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                bool platformInvoked = false;
                bool invalidPlatformInvoked = false;
                var nm = Platform.CreateNotificationManager();
                var builder = nm.GetBuilder<ISnackbarExtension, IIosLocalNotificationExtension>();

                builder.WhenUsing<IDroidNotificationExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<ISnackbarExtension>(_ => platformInvoked = true)
                    .WhenUsing<IIosNotificationExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<IIosLocalNotificationExtension>(_ => platformInvoked = true)
                    .WhenUsing<IUwpExtension>(_ => platformInvoked = true);

                Assert.False(invalidPlatformInvoked);
                Assert.True(platformInvoked);
            });

        [Fact]
        public Task BuildTwiceMustFail()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                var nm = Platform.CreateNotificationManager();
                var builder = nm.GetBuilder();
                builder.Build();

                Assert.Throws<InvalidOperationException>(() => builder.Build());
            });

        [Fact]
        public Task BuildTwiceMustFailAlternative()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                var nm = Platform.CreateNotificationManager();
                var builder = nm.GetBuilder<ISnackbarExtension, IIosLocalNotificationExtension>();
                builder.Build();

                Assert.Throws<InvalidOperationException>(() => builder.Build());
            });
    }
}

#endif
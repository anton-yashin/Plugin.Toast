#if NETCORE_APP == false
#nullable enable

using Microsoft.Extensions.DependencyInjection;
using System;
using LightMock;
using LightMock.Generator;
using Plugin.Toast;
using Xunit;
using System.Threading.Tasks;

namespace DeviceTests
{
    public class DependencyInjectionTests
    {
        [Fact]
        public Task CreateDefaultBuidler()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                bool platformInvoked = false;
                bool invalidPlatformInvoked = false;

                using var sp = CreateContainer();
                var builder = sp.GetRequiredService<IBuilder>();

                builder.WhenUsing<IDroidNotificationExtension>(_ => platformInvoked = true)
                    .WhenUsing<ISnackbarExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<IIosNotificationExtension>(_ => platformInvoked = true)
                    .WhenUsing<IIosLocalNotificationExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<IUwpExtension>(_ => platformInvoked = true);

                Assert.False(invalidPlatformInvoked);
                Assert.True(platformInvoked);
            });

        [Fact]
        public Task CreateSpecificBuilderOneArg()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                bool platformInvoked = false;
                bool invalidPlatformInvoked = false;

                using var sp = CreateContainer();
                var builder = sp.GetRequiredService<IBuilder<ISnackbarExtension>>();

                builder.WhenUsing<IDroidNotificationExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<ISnackbarExtension>(_ => platformInvoked = true)
                    .WhenUsing<IIosNotificationExtension>(_ => platformInvoked = true)
                    .WhenUsing<IIosLocalNotificationExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<IUwpExtension>(_ => platformInvoked = true);

                Assert.False(invalidPlatformInvoked);
                Assert.True(platformInvoked);
            });

        [Fact]
        public Task CreateSpecificBuilderTwoArgs()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                bool platformInvoked = false;
                bool invalidPlatformInvoked = false;

                using var sp = CreateContainer();
                var builder = sp.GetRequiredService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>();

                builder.WhenUsing<IDroidNotificationExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<ISnackbarExtension>(_ => platformInvoked = true)
                    .WhenUsing<IIosNotificationExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<IIosLocalNotificationExtension>(_ => platformInvoked = true)
                    .WhenUsing<IUwpExtension>(_ => platformInvoked = true);

                Assert.False(invalidPlatformInvoked);
                Assert.True(platformInvoked);
            });

        [Fact]
        public void CreateSpecificBuilderMoreThreeArgs()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                bool platformInvoked = false;
                bool invalidPlatformInvoked = false;

                using var sp = CreateContainer();
                var builder = sp.GetRequiredService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension, IUwpExtension>>();

                builder.WhenUsing<IDroidNotificationExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<ISnackbarExtension>(_ => platformInvoked = true)
                    .WhenUsing<IIosNotificationExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<IIosLocalNotificationExtension>(_ => platformInvoked = true)
                    .WhenUsing<IUwpExtension>(_ => platformInvoked = true);

                Assert.False(invalidPlatformInvoked);
                Assert.True(platformInvoked);
            });

        [Fact]
        public Task CreateSpecificBuilderFourArgs()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                bool platformInvoked = false;
                bool invalidPlatformInvoked = false;

                using var sp = CreateContainer();
                var builder = sp.GetRequiredService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension, IUwpExtension, IUwpExtension>>();

                builder.WhenUsing<IDroidNotificationExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<ISnackbarExtension>(_ => platformInvoked = true)
                    .WhenUsing<IIosNotificationExtension>(_ => invalidPlatformInvoked = true)
                    .WhenUsing<IIosLocalNotificationExtension>(_ => platformInvoked = true)
                    .WhenUsing<IUwpExtension>(_ => platformInvoked = true);

                Assert.False(invalidPlatformInvoked);
                Assert.True(platformInvoked);
            });

        [Fact]
        public Task InjectConfiguration()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                // prepare
                var z = new Mock<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>();
                var mockDroidConfiguration = new Mock<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>();
                var mockSnackbarConfiguration = new Mock<ISpecificExtensionConfiguration<ISnackbarExtension, int>>();
                var mockIosConfiguration = new Mock<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>();
                var mockIosLocalConfiguration = new Mock<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>();
                var mockUwpConfiguration = new Mock<ISpecificExtensionConfiguration<IUwpExtension, int>>();

                using var sp = CreateContainer(_ => _
                .AddSingleton<IExtensionConfiguration<IDroidNotificationExtension>>(mockDroidConfiguration.Object)
                .AddSingleton<IExtensionConfiguration<ISnackbarExtension>>(mockSnackbarConfiguration.Object)
                .AddSingleton<IExtensionConfiguration<IIosNotificationExtension>>(mockIosConfiguration.Object)
                .AddSingleton<IExtensionConfiguration<IIosLocalNotificationExtension>>(mockIosLocalConfiguration.Object)
                .AddSingleton<IExtensionConfiguration<IUwpExtension>>(mockUwpConfiguration.Object));

                // act
                var builder = sp.GetRequiredService<IBuilder>();

                // verify
#if __ANDROID__
                mockDroidConfiguration.Assert(f => f.Configure(The<IDroidNotificationExtension>.IsAnyValue), Invoked.Once);
                mockSnackbarConfiguration.AssertNoOtherCalls();
                mockIosConfiguration.AssertNoOtherCalls();
                mockIosLocalConfiguration.AssertNoOtherCalls();
                mockUwpConfiguration.AssertNoOtherCalls();
#elif __IOS__
                mockIosConfiguration.Assert(f => f.Configure(The<IIosNotificationExtension>.IsAnyValue), Invoked.Once);
                mockDroidConfiguration.AssertNoOtherCalls();
                mockSnackbarConfiguration.AssertNoOtherCalls();
                mockIosLocalConfiguration.AssertNoOtherCalls();
                mockUwpConfiguration.AssertNoOtherCalls();
#elif NETFX_CORE
                mockUwpConfiguration.Assert(f => f.Configure(The<IUwpExtension>.IsAnyValue), Invoked.Once);
                mockDroidConfiguration.AssertNoOtherCalls();
                mockSnackbarConfiguration.AssertNoOtherCalls();
                mockIosConfiguration.AssertNoOtherCalls();
                mockIosLocalConfiguration.AssertNoOtherCalls();
#endif
            });

        [Fact]
        public Task InjectSnackbarAndLocalConfiguration()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                // prepare
                var mockDroidConfiguration = new Mock<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>();
                var mockSnackbarConfiguration = new Mock<ISpecificExtensionConfiguration<ISnackbarExtension, int>>();
                var mockIosConfiguration = new Mock<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>();
                var mockIosLocalConfiguration = new Mock<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>();
                var mockUwpConfiguration = new Mock<ISpecificExtensionConfiguration<IUwpExtension, int>>();

                using var sp = CreateContainer(_ => _
                .AddSingleton<IExtensionConfiguration<IDroidNotificationExtension>>(mockDroidConfiguration.Object)
                .AddSingleton<IExtensionConfiguration<ISnackbarExtension>>(mockSnackbarConfiguration.Object)
                .AddSingleton<IExtensionConfiguration<IIosNotificationExtension>>(mockIosConfiguration.Object)
                .AddSingleton<IExtensionConfiguration<IIosLocalNotificationExtension>>(mockIosLocalConfiguration.Object)
                .AddSingleton<IExtensionConfiguration<IUwpExtension>>(mockUwpConfiguration.Object));

                // act
                var builder = sp.GetRequiredService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>();

                // verify
#if __ANDROID__
                mockSnackbarConfiguration.Assert(f => f.Configure(The<ISnackbarExtension>.IsAnyValue), Invoked.Once);
                mockDroidConfiguration.AssertNoOtherCalls();
                mockIosConfiguration.AssertNoOtherCalls();
                mockIosLocalConfiguration.AssertNoOtherCalls();
                mockUwpConfiguration.AssertNoOtherCalls();
#elif __IOS__
                mockIosLocalConfiguration.Assert(f => f.Configure(The<IIosLocalNotificationExtension>.IsAnyValue), Invoked.Once);
                mockDroidConfiguration   .AssertNoOtherCalls();
                mockSnackbarConfiguration.AssertNoOtherCalls();
                mockIosConfiguration     .AssertNoOtherCalls();
                mockUwpConfiguration.AssertNoOtherCalls();
#elif NETFX_CORE
                mockUwpConfiguration.Assert(f => f.Configure(The<IUwpExtension>.IsAnyValue), Invoked.Once);
                mockDroidConfiguration.AssertNoOtherCalls();
                mockSnackbarConfiguration.AssertNoOtherCalls();
                mockIosConfiguration.AssertNoOtherCalls();
                mockIosLocalConfiguration.AssertNoOtherCalls();
#endif
            });

        [Fact]
        public Task InjectSpecificConfiguration()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                // prepare
                var mockDroidConfiguration1 = new Mock<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>();
                var mockDroidConfiguration2 = new Mock<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>();
                var mockSnackbarConfiguration1 = new Mock<ISpecificExtensionConfiguration<ISnackbarExtension, int>>();
                var mockSnackbarConfiguration2 = new Mock<ISpecificExtensionConfiguration<ISnackbarExtension, int>>();
                var mockIosConfiguration1 = new Mock<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>();
                var mockIosConfiguration2 = new Mock<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>();
                var mockIosLocalConfiguration1 = new Mock<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>();
                var mockIosLocalConfiguration2 = new Mock<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>();
                var mockUwpConfiguration1 = new Mock<ISpecificExtensionConfiguration<IUwpExtension, int>>();
                var mockUwpConfiguration2 = new Mock<ISpecificExtensionConfiguration<IUwpExtension, int>>();

                mockDroidConfiguration1.Arrange(f => f.Token).Returns(1);
                mockDroidConfiguration2.Arrange(f => f.Token).Returns(2);

                mockSnackbarConfiguration1.Arrange(f => f.Token).Returns(1);
                mockSnackbarConfiguration2.Arrange(f => f.Token).Returns(2);
                mockIosConfiguration1.Arrange(f => f.Token).Returns(1);
                mockIosConfiguration2.Arrange(f => f.Token).Returns(2);
                mockIosLocalConfiguration1.Arrange(f => f.Token).Returns(1);
                mockIosLocalConfiguration2.Arrange(f => f.Token).Returns(2);
                mockUwpConfiguration1.Arrange(f => f.Token).Returns(1);
                mockUwpConfiguration2.Arrange(f => f.Token).Returns(2);

                using var sp = CreateContainer(_ => _
                .AddSingleton<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>(mockDroidConfiguration1.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>(mockDroidConfiguration2.Object)
                .AddSingleton<ISpecificExtensionConfiguration<ISnackbarExtension, int>>(mockSnackbarConfiguration1.Object)
                .AddSingleton<ISpecificExtensionConfiguration<ISnackbarExtension, int>>(mockSnackbarConfiguration2.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>(mockIosConfiguration1.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>(mockIosConfiguration2.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>(mockIosLocalConfiguration1.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>(mockIosLocalConfiguration2.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IUwpExtension, int>>(mockUwpConfiguration1.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IUwpExtension, int>>(mockUwpConfiguration2.Object));

                var builder = sp.GetRequiredService<IBuilder>();
                // act
                builder.UseConfiguration(2);

                // verify
#if __ANDROID__
                mockDroidConfiguration1.Assert(f => f.Configure(The<IDroidNotificationExtension>.IsAnyValue), Invoked.Never);
                mockDroidConfiguration2.Assert(f => f.Configure(The<IDroidNotificationExtension>.IsAnyValue), Invoked.Once);
                mockSnackbarConfiguration1.AssertNoOtherCalls();
                mockSnackbarConfiguration2.AssertNoOtherCalls();
                mockIosConfiguration1.AssertNoOtherCalls();
                mockIosConfiguration2.AssertNoOtherCalls();
                mockIosLocalConfiguration1.AssertNoOtherCalls();
                mockIosLocalConfiguration2.AssertNoOtherCalls();
                mockUwpConfiguration1.AssertNoOtherCalls();
                mockUwpConfiguration2.AssertNoOtherCalls();
#elif __IOS__
                mockDroidConfiguration1.AssertNoOtherCalls();
                mockDroidConfiguration2.AssertNoOtherCalls();
                mockSnackbarConfiguration1.AssertNoOtherCalls();
                mockSnackbarConfiguration2.AssertNoOtherCalls();
                mockIosConfiguration1.Assert(f => f.Configure(The<IIosNotificationExtension>.IsAnyValue), Invoked.Never);
                mockIosConfiguration2.Assert(f => f.Configure(The<IIosNotificationExtension>.IsAnyValue), Invoked.Once);
                mockIosLocalConfiguration1.AssertNoOtherCalls();
                mockIosLocalConfiguration2.AssertNoOtherCalls();
                mockUwpConfiguration1.AssertNoOtherCalls();
                mockUwpConfiguration2.AssertNoOtherCalls();
#elif NETFX_CORE
                mockDroidConfiguration1.AssertNoOtherCalls();
                mockDroidConfiguration2.AssertNoOtherCalls();
                mockSnackbarConfiguration1.AssertNoOtherCalls();
                mockSnackbarConfiguration2.AssertNoOtherCalls();
                mockIosConfiguration1.AssertNoOtherCalls();
                mockIosConfiguration2.AssertNoOtherCalls();
                mockIosLocalConfiguration1.AssertNoOtherCalls();
                mockIosLocalConfiguration2.AssertNoOtherCalls();
                mockUwpConfiguration1.Assert(f => f.Configure(The<IUwpExtension>.IsAnyValue), Invoked.Never);
                mockUwpConfiguration2.Assert(f => f.Configure(The<IUwpExtension>.IsAnyValue), Invoked.Once);
#endif
            });

        [Fact]
        public Task InjectSnackbarAndLocalSpecificConfiguration()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                // prepare
                var mockDroidConfiguration1 = new Mock<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>();
                var mockDroidConfiguration2 = new Mock<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>();
                var mockSnackbarConfiguration1 = new Mock<ISpecificExtensionConfiguration<ISnackbarExtension, int>>();
                var mockSnackbarConfiguration2 = new Mock<ISpecificExtensionConfiguration<ISnackbarExtension, int>>();
                var mockIosConfiguration1 = new Mock<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>();
                var mockIosConfiguration2 = new Mock<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>();
                var mockIosLocalConfiguration1 = new Mock<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>();
                var mockIosLocalConfiguration2 = new Mock<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>();
                var mockUwpConfiguration1 = new Mock<ISpecificExtensionConfiguration<IUwpExtension, int>>();
                var mockUwpConfiguration2 = new Mock<ISpecificExtensionConfiguration<IUwpExtension, int>>();

                mockDroidConfiguration1.Arrange(f => f.Token).Returns(1);
                mockDroidConfiguration2.Arrange(f => f.Token).Returns(2);
                mockSnackbarConfiguration1.Arrange(f => f.Token).Returns(1);
                mockSnackbarConfiguration2.Arrange(f => f.Token).Returns(2);
                mockIosConfiguration1.Arrange(f => f.Token).Returns(1);
                mockIosConfiguration2.Arrange(f => f.Token).Returns(2);
                mockIosLocalConfiguration1.Arrange(f => f.Token).Returns(1);
                mockIosLocalConfiguration2.Arrange(f => f.Token).Returns(2);
                mockUwpConfiguration1.Arrange(f => f.Token).Returns(1);
                mockUwpConfiguration2.Arrange(f => f.Token).Returns(2);

                using var sp = CreateContainer(_ => _
                .AddSingleton<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>(mockDroidConfiguration1.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>(mockDroidConfiguration2.Object)
                .AddSingleton<ISpecificExtensionConfiguration<ISnackbarExtension, int>>(mockSnackbarConfiguration1.Object)
                .AddSingleton<ISpecificExtensionConfiguration<ISnackbarExtension, int>>(mockSnackbarConfiguration2.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>(mockIosConfiguration1.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>(mockIosConfiguration2.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>(mockIosLocalConfiguration1.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>(mockIosLocalConfiguration2.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IUwpExtension, int>>(mockUwpConfiguration1.Object)
                .AddSingleton<ISpecificExtensionConfiguration<IUwpExtension, int>>(mockUwpConfiguration2.Object));

                var builder = sp.GetRequiredService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>();
                // act
                builder.UseConfiguration(2);

                // verify
#if __ANDROID__
                mockDroidConfiguration1.AssertNoOtherCalls();
                mockDroidConfiguration2.AssertNoOtherCalls();
                mockSnackbarConfiguration1.Assert(f => f.Configure(The<ISnackbarExtension>.IsAnyValue), Invoked.Never);
                mockSnackbarConfiguration2.Assert(f => f.Configure(The<ISnackbarExtension>.IsAnyValue), Invoked.Once);
                mockIosConfiguration1.AssertNoOtherCalls();
                mockIosConfiguration2.AssertNoOtherCalls();
                mockIosLocalConfiguration1.AssertNoOtherCalls();
                mockIosLocalConfiguration2.AssertNoOtherCalls();
                mockUwpConfiguration1.AssertNoOtherCalls();
                mockUwpConfiguration2.AssertNoOtherCalls();
#elif __IOS__
                mockDroidConfiguration1.AssertNoOtherCalls();
                mockDroidConfiguration2.AssertNoOtherCalls();
                mockSnackbarConfiguration1.AssertNoOtherCalls();
                mockSnackbarConfiguration2.AssertNoOtherCalls();
                mockIosConfiguration1.AssertNoOtherCalls();
                mockIosConfiguration2.AssertNoOtherCalls();
                mockIosLocalConfiguration1.Assert(f => f.Configure(The<IIosLocalNotificationExtension>.IsAnyValue), Invoked.Never);
                mockIosLocalConfiguration2.Assert(f => f.Configure(The<IIosLocalNotificationExtension>.IsAnyValue), Invoked.Once);
                mockUwpConfiguration1.AssertNoOtherCalls();
                mockUwpConfiguration2.AssertNoOtherCalls();
#elif NETFX_CORE
                mockDroidConfiguration1.AssertNoOtherCalls();
                mockDroidConfiguration2.AssertNoOtherCalls();
                mockSnackbarConfiguration1.AssertNoOtherCalls();
                mockSnackbarConfiguration2.AssertNoOtherCalls();
                mockIosConfiguration1.AssertNoOtherCalls();
                mockIosConfiguration2.AssertNoOtherCalls();
                mockIosLocalConfiguration1.AssertNoOtherCalls();
                mockIosLocalConfiguration2.AssertNoOtherCalls();
                mockUwpConfiguration1.Assert(f => f.Configure(The<IUwpExtension>.IsAnyValue), Invoked.Never);
                mockUwpConfiguration2.Assert(f => f.Configure(The<IUwpExtension>.IsAnyValue), Invoked.Once);
#endif
            });

        ServiceProvider CreateContainer(Action<IServiceCollection>? configuration = null)
        {
            var sc = new ServiceCollection();
            configuration?.Invoke(sc);
#if __ANDROID__
            sc.AddNotificationManager(new ToastOptions(Platform.Activity));
#else
            sc.AddNotificationManager(new ToastOptions());
#endif
            return sc.BuildServiceProvider();
        }
    }
}

#endif
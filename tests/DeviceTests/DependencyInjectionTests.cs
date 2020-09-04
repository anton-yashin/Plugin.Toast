using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using Plugin.Toast;
using Xunit;
using System.Threading.Tasks;
using DeviceTests.Mocks;

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
                var builder = sp.GetService<IBuilder>();

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
                var builder = sp.GetService<IBuilder<ISnackbarExtension>>();

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
                var builder = sp.GetService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>();

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
                var builder = sp.GetService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension, IUwpExtension>>();

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
                var builder = sp.GetService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension, IUwpExtension, IUwpExtension>>();

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
                var mockDroidConfiguration = new MockSpecificExtensionConfiguration<IDroidNotificationExtension, int>();
                var mockSnackbarConfiguration = new MockSpecificExtensionConfiguration<ISnackbarExtension, int>();
                var mockIosConfiguration = new MockSpecificExtensionConfiguration<IIosNotificationExtension, int>();
                var mockIosLocalConfiguration = new MockSpecificExtensionConfiguration<IIosLocalNotificationExtension, int>();
                var mockUwpConfiguration = new MockSpecificExtensionConfiguration<IUwpExtension, int>();

                using var sp = CreateContainer(_ => _
                .AddSingleton<IExtensionConfiguration<IDroidNotificationExtension>>(mockDroidConfiguration)
                .AddSingleton<IExtensionConfiguration<ISnackbarExtension>>(mockSnackbarConfiguration)
                .AddSingleton<IExtensionConfiguration<IIosNotificationExtension>>(mockIosConfiguration)
                .AddSingleton<IExtensionConfiguration<IIosLocalNotificationExtension>>(mockIosLocalConfiguration)
                .AddSingleton<IExtensionConfiguration<IUwpExtension>>(mockUwpConfiguration));

                // act
                var builder = sp.GetService<IBuilder>();

                // verify
#if __ANDROID__
                Assert.Equal(expected: 1, mockDroidConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockSnackbarConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockIosConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockIosLocalConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockUwpConfiguration.ConfigureCallCount);
#elif __IOS__
                Assert.Equal(expected: 0, mockDroidConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockSnackbarConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 1, mockIosConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockIosLocalConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockUwpConfiguration.ConfigureCallCount);
#elif NETFX_CORE
                Assert.Equal(expected: 0, mockDroidConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockSnackbarConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockIosConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockIosLocalConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 1, mockUwpConfiguration.ConfigureCallCount);
#endif
            });

        [Fact]
        public Task InjectSnackbarAndLocalConfiguration()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                // prepare
                var mockDroidConfiguration = new MockSpecificExtensionConfiguration<IDroidNotificationExtension, int>();
                var mockSnackbarConfiguration = new MockSpecificExtensionConfiguration<ISnackbarExtension, int>();
                var mockIosConfiguration = new MockSpecificExtensionConfiguration<IIosNotificationExtension, int>();
                var mockIosLocalConfiguration = new MockSpecificExtensionConfiguration<IIosLocalNotificationExtension, int>();
                var mockUwpConfiguration = new MockSpecificExtensionConfiguration<IUwpExtension, int>();

                using var sp = CreateContainer(_ => _
                .AddSingleton<IExtensionConfiguration<IDroidNotificationExtension>>(mockDroidConfiguration)
                .AddSingleton<IExtensionConfiguration<ISnackbarExtension>>(mockSnackbarConfiguration)
                .AddSingleton<IExtensionConfiguration<IIosNotificationExtension>>(mockIosConfiguration)
                .AddSingleton<IExtensionConfiguration<IIosLocalNotificationExtension>>(mockIosLocalConfiguration)
                .AddSingleton<IExtensionConfiguration<IUwpExtension>>(mockUwpConfiguration));

                // act
                var builder = sp.GetService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>();

                // verify
#if __ANDROID__
                Assert.Equal(expected: 0, mockDroidConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 1, mockSnackbarConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockIosConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockIosLocalConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockUwpConfiguration.ConfigureCallCount);
#elif __IOS__
                Assert.Equal(expected: 0, mockDroidConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockSnackbarConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockIosConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 1, mockIosLocalConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockUwpConfiguration.ConfigureCallCount);
#elif NETFX_CORE
                Assert.Equal(expected: 0, mockDroidConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockSnackbarConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockIosConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 0, mockIosLocalConfiguration.ConfigureCallCount);
                Assert.Equal(expected: 1, mockUwpConfiguration.ConfigureCallCount);
#endif
            });

        [Fact]
        public Task InjectSpecificConfiguration()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                // prepare
                var mockDroidConfiguration1 = new MockSpecificExtensionConfiguration<IDroidNotificationExtension, int>();
                var mockDroidConfiguration2 = new MockSpecificExtensionConfiguration<IDroidNotificationExtension, int>();
                var mockSnackbarConfiguration1 = new MockSpecificExtensionConfiguration<ISnackbarExtension, int>();
                var mockSnackbarConfiguration2 = new MockSpecificExtensionConfiguration<ISnackbarExtension, int>();
                var mockIosConfiguration1 = new MockSpecificExtensionConfiguration<IIosNotificationExtension, int>();
                var mockIosConfiguration2 = new MockSpecificExtensionConfiguration<IIosNotificationExtension, int>();
                var mockIosLocalConfiguration1 = new MockSpecificExtensionConfiguration<IIosLocalNotificationExtension, int>();
                var mockIosLocalConfiguration2 = new MockSpecificExtensionConfiguration<IIosLocalNotificationExtension, int>();
                var mockUwpConfiguration1 = new MockSpecificExtensionConfiguration<IUwpExtension, int>();
                var mockUwpConfiguration2 = new MockSpecificExtensionConfiguration<IUwpExtension, int>();

                mockDroidConfiguration1.OnToken = () => 1;
                mockDroidConfiguration2.OnToken = () => 2;
                mockSnackbarConfiguration1.OnToken = () => 1;
                mockSnackbarConfiguration2.OnToken = () => 2;
                mockIosConfiguration1.OnToken = () => 1;
                mockIosConfiguration2.OnToken = () => 2;
                mockIosLocalConfiguration1.OnToken = () => 1;
                mockIosLocalConfiguration2.OnToken = () => 2;
                mockUwpConfiguration1.OnToken = () => 1;
                mockUwpConfiguration2.OnToken = () => 2;

                using var sp = CreateContainer(_ => _
                .AddSingleton<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>(mockDroidConfiguration1)
                .AddSingleton<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>(mockDroidConfiguration2)
                .AddSingleton<ISpecificExtensionConfiguration<ISnackbarExtension, int>>(mockSnackbarConfiguration1)
                .AddSingleton<ISpecificExtensionConfiguration<ISnackbarExtension, int>>(mockSnackbarConfiguration2)
                .AddSingleton<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>(mockIosConfiguration1)
                .AddSingleton<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>(mockIosConfiguration2)
                .AddSingleton<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>(mockIosLocalConfiguration1)
                .AddSingleton<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>(mockIosLocalConfiguration2)
                .AddSingleton<ISpecificExtensionConfiguration<IUwpExtension, int>>(mockUwpConfiguration1)
                .AddSingleton<ISpecificExtensionConfiguration<IUwpExtension, int>>(mockUwpConfiguration2));

                var builder = sp.GetService<IBuilder>();
                // act
                builder.UseConfiguration(2);

                // verify
#if __ANDROID__
                Assert.Equal(expected: 0, mockDroidConfiguration1.ConfigureCallCount);
                Assert.Equal(expected: 1, mockDroidConfiguration2.ConfigureCallCount);
                mockSnackbarConfiguration1.VerifyNoCalls();
                mockSnackbarConfiguration2.VerifyNoCalls();
                mockIosConfiguration1.VerifyNoCalls();
                mockIosConfiguration2.VerifyNoCalls();
                mockIosLocalConfiguration1.VerifyNoCalls();
                mockIosLocalConfiguration2.VerifyNoCalls();
                mockUwpConfiguration1.VerifyNoCalls();
                mockUwpConfiguration2.VerifyNoCalls();
#elif __IOS__
                mockDroidConfiguration1.VerifyNoCalls();
                mockDroidConfiguration2.VerifyNoCalls();
                mockSnackbarConfiguration1.VerifyNoCalls();
                mockSnackbarConfiguration2.VerifyNoCalls();
                Assert.Equal(expected: 0, mockIosConfiguration1.ConfigureCallCount);
                Assert.Equal(expected: 1, mockIosConfiguration2.ConfigureCallCount);
                mockIosLocalConfiguration1.VerifyNoCalls();
                mockIosLocalConfiguration2.VerifyNoCalls();
                mockUwpConfiguration1.VerifyNoCalls();
                mockUwpConfiguration2.VerifyNoCalls();
#elif NETFX_CORE
                mockDroidConfiguration1.VerifyNoCalls();
                mockDroidConfiguration2.VerifyNoCalls();
                mockSnackbarConfiguration1.VerifyNoCalls();
                mockSnackbarConfiguration2.VerifyNoCalls();
                mockIosConfiguration1.VerifyNoCalls();
                mockIosConfiguration2.VerifyNoCalls();
                mockIosLocalConfiguration1.VerifyNoCalls();
                mockIosLocalConfiguration2.VerifyNoCalls();
                Assert.Equal(expected: 0, mockUwpConfiguration1.ConfigureCallCount);
                Assert.Equal(expected: 1, mockUwpConfiguration2.ConfigureCallCount);
#endif
            });

        [Fact]
        public Task InjectSnackbarAndLocalSpecificConfiguration()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                // prepare
                var mockDroidConfiguration1 = new MockSpecificExtensionConfiguration<IDroidNotificationExtension, int>();
                var mockDroidConfiguration2 = new MockSpecificExtensionConfiguration<IDroidNotificationExtension, int>();
                var mockSnackbarConfiguration1 = new MockSpecificExtensionConfiguration<ISnackbarExtension, int>();
                var mockSnackbarConfiguration2 = new MockSpecificExtensionConfiguration<ISnackbarExtension, int>();
                var mockIosConfiguration1 = new MockSpecificExtensionConfiguration<IIosNotificationExtension, int>();
                var mockIosConfiguration2 = new MockSpecificExtensionConfiguration<IIosNotificationExtension, int>();
                var mockIosLocalConfiguration1 = new MockSpecificExtensionConfiguration<IIosLocalNotificationExtension, int>();
                var mockIosLocalConfiguration2 = new MockSpecificExtensionConfiguration<IIosLocalNotificationExtension, int>();
                var mockUwpConfiguration1 = new MockSpecificExtensionConfiguration<IUwpExtension, int>();
                var mockUwpConfiguration2 = new MockSpecificExtensionConfiguration<IUwpExtension, int>();

                mockDroidConfiguration1.OnToken = () => 1;
                mockDroidConfiguration2.OnToken = () => 2;
                mockSnackbarConfiguration1.OnToken = () => 1;
                mockSnackbarConfiguration2.OnToken = () => 2;
                mockIosConfiguration1.OnToken = () => 1;
                mockIosConfiguration2.OnToken = () => 2;
                mockIosLocalConfiguration1.OnToken = () => 1;
                mockIosLocalConfiguration2.OnToken = () => 2;
                mockUwpConfiguration1.OnToken = () => 1;
                mockUwpConfiguration2.OnToken = () => 2;

                using var sp = CreateContainer(_ => _
                .AddSingleton<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>(mockDroidConfiguration1)
                .AddSingleton<ISpecificExtensionConfiguration<IDroidNotificationExtension, int>>(mockDroidConfiguration2)
                .AddSingleton<ISpecificExtensionConfiguration<ISnackbarExtension, int>>(mockSnackbarConfiguration1)
                .AddSingleton<ISpecificExtensionConfiguration<ISnackbarExtension, int>>(mockSnackbarConfiguration2)
                .AddSingleton<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>(mockIosConfiguration1)
                .AddSingleton<ISpecificExtensionConfiguration<IIosNotificationExtension, int>>(mockIosConfiguration2)
                .AddSingleton<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>(mockIosLocalConfiguration1)
                .AddSingleton<ISpecificExtensionConfiguration<IIosLocalNotificationExtension, int>>(mockIosLocalConfiguration2)
                .AddSingleton<ISpecificExtensionConfiguration<IUwpExtension, int>>(mockUwpConfiguration1)
                .AddSingleton<ISpecificExtensionConfiguration<IUwpExtension, int>>(mockUwpConfiguration2));

                var builder = sp.GetService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>();
                // act
                builder.UseConfiguration(2);

                // verify
#if __ANDROID__
                mockDroidConfiguration1.VerifyNoCalls();
                mockDroidConfiguration2.VerifyNoCalls();
                Assert.Equal(expected: 0, mockSnackbarConfiguration1.ConfigureCallCount);
                Assert.Equal(expected: 1, mockSnackbarConfiguration2.ConfigureCallCount);
                mockIosConfiguration1.VerifyNoCalls();
                mockIosConfiguration2.VerifyNoCalls();
                mockIosLocalConfiguration1.VerifyNoCalls();
                mockIosLocalConfiguration2.VerifyNoCalls();
                mockUwpConfiguration1.VerifyNoCalls();
                mockUwpConfiguration2.VerifyNoCalls();
#elif __IOS__
                mockDroidConfiguration1.VerifyNoCalls();
                mockDroidConfiguration2.VerifyNoCalls();
                mockSnackbarConfiguration1.VerifyNoCalls();
                mockSnackbarConfiguration2.VerifyNoCalls();
                mockIosConfiguration1.VerifyNoCalls();
                mockIosConfiguration2.VerifyNoCalls();
                Assert.Equal(expected: 0, mockIosLocalConfiguration1.ConfigureCallCount);
                Assert.Equal(expected: 1, mockIosLocalConfiguration2.ConfigureCallCount);
                mockUwpConfiguration1.VerifyNoCalls();
                mockUwpConfiguration2.VerifyNoCalls();
#elif NETFX_CORE
                mockDroidConfiguration1.VerifyNoCalls();
                mockDroidConfiguration2.VerifyNoCalls();
                mockSnackbarConfiguration1.VerifyNoCalls();
                mockSnackbarConfiguration2.VerifyNoCalls();
                mockIosConfiguration1.VerifyNoCalls();
                mockIosConfiguration2.VerifyNoCalls();
                mockIosLocalConfiguration1.VerifyNoCalls();
                mockIosLocalConfiguration2.VerifyNoCalls();
                Assert.Equal(expected: 0, mockUwpConfiguration1.ConfigureCallCount);
                Assert.Equal(expected: 1, mockUwpConfiguration2.ConfigureCallCount);
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

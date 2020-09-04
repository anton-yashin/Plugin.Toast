using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
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
                var mockDroidConfiguration = new Mock<IExtensionConfiguration<IDroidNotificationExtension>>();
                var mockSnackbarConfiguration = new Mock<IExtensionConfiguration<ISnackbarExtension>>();
                var mockIosConfiguration = new Mock<IExtensionConfiguration<IIosNotificationExtension>>();
                var mockIosLocalConfiguration = new Mock<IExtensionConfiguration<IIosLocalNotificationExtension>>();
                var mockUwpConfiguration = new Mock<IExtensionConfiguration<IUwpExtension>>();

                using var sp = CreateContainer(_ => _
                .AddSingleton(mockDroidConfiguration.Object)
                .AddSingleton(mockSnackbarConfiguration.Object)
                .AddSingleton(mockIosConfiguration.Object)
                .AddSingleton(mockIosLocalConfiguration.Object)
                .AddSingleton(mockUwpConfiguration.Object));

                // act
                var builder = sp.GetService<IBuilder>();

                // verify
#if __ANDROID__
                mockDroidConfiguration.Verify(_ => _.Configure(It.IsAny<IDroidNotificationExtension>()), Times.Once);
                mockSnackbarConfiguration.VerifyNoOtherCalls();
                mockIosConfiguration.VerifyNoOtherCalls();
                mockIosLocalConfiguration.VerifyNoOtherCalls();
                mockUwpConfiguration.VerifyNoOtherCalls();
#elif __IOS__
                mockDroidConfiguration.VerifyNoOtherCalls();
                mockSnackbarConfiguration.VerifyNoOtherCalls();
                mockIosConfiguration.Verify(_ => _.Configure(It.IsAny<IIosNotificationExtension>()), Times.Once);
                mockIosLocalConfiguration.VerifyNoOtherCalls();
                mockUwpConfiguration.VerifyNoOtherCalls();
#elif NETFX_CORE
                mockDroidConfiguration.VerifyNoOtherCalls();
                mockSnackbarConfiguration.VerifyNoOtherCalls();
                mockIosConfiguration.VerifyNoOtherCalls();
                mockIosLocalConfiguration.VerifyNoOtherCalls();
                mockUwpConfiguration.Verify(_ => _.Configure(It.IsAny<IUwpExtension>()), Times.Once);
#endif
            });

        [Fact]
        public Task InjectSnackbarAndLocalConfiguration()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
            {
                // prepare
                var mockDroidConfiguration = new Mock<IExtensionConfiguration<IDroidNotificationExtension>>();
                var mockSnackbarConfiguration = new Mock<IExtensionConfiguration<ISnackbarExtension>>();
                var mockIosConfiguration = new Mock<IExtensionConfiguration<IIosNotificationExtension>>();
                var mockIosLocalConfiguration = new Mock<IExtensionConfiguration<IIosLocalNotificationExtension>>();
                var mockUwpConfiguration = new Mock<IExtensionConfiguration<IUwpExtension>>();

                using var sp = CreateContainer(_ => _
                .AddSingleton(mockDroidConfiguration.Object)
                .AddSingleton(mockSnackbarConfiguration.Object)
                .AddSingleton(mockIosConfiguration.Object)
                .AddSingleton(mockIosLocalConfiguration.Object)
                .AddSingleton(mockUwpConfiguration.Object));

                // act
                var builder = sp.GetService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>();

                // verify
#if __ANDROID__
                mockDroidConfiguration.VerifyNoOtherCalls();
                mockSnackbarConfiguration.Verify(_ => _.Configure(It.IsAny<ISnackbarExtension>()), Times.Once);
                mockIosConfiguration.VerifyNoOtherCalls();
                mockIosLocalConfiguration.VerifyNoOtherCalls();
                mockUwpConfiguration.VerifyNoOtherCalls();
#elif __IOS__
                mockDroidConfiguration.VerifyNoOtherCalls();
                mockSnackbarConfiguration.VerifyNoOtherCalls();
                mockIosConfiguration.VerifyNoOtherCalls();
                mockIosLocalConfiguration.Verify(_ => _.Configure(It.IsAny<IIosLocalNotificationExtension>()), Times.Once);
                mockUwpConfiguration.VerifyNoOtherCalls();
#elif NETFX_CORE
                mockDroidConfiguration.VerifyNoOtherCalls();
                mockSnackbarConfiguration.VerifyNoOtherCalls();
                mockIosConfiguration.VerifyNoOtherCalls();
                mockIosLocalConfiguration.VerifyNoOtherCalls();
                mockUwpConfiguration.Verify(_ => _.Configure(It.IsAny<IUwpExtension>()), Times.Once);
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

                mockDroidConfiguration1.Setup(_ => _.Token).Returns(1);
                mockDroidConfiguration2.Setup(_ => _.Token).Returns(2);
                mockSnackbarConfiguration1.Setup(_ => _.Token).Returns(1);
                mockSnackbarConfiguration2.Setup(_ => _.Token).Returns(2);
                mockIosConfiguration1.Setup(_ => _.Token).Returns(1);
                mockIosConfiguration2.Setup(_ => _.Token).Returns(2);
                mockIosLocalConfiguration1.Setup(_ => _.Token).Returns(1);
                mockIosLocalConfiguration2.Setup(_ => _.Token).Returns(2);
                mockUwpConfiguration1.Setup(_ => _.Token).Returns(1);
                mockUwpConfiguration2.Setup(_ => _.Token).Returns(2);

                using var sp = CreateContainer(_ => _
                .AddSingleton(mockDroidConfiguration1.Object)
                .AddSingleton(mockDroidConfiguration2.Object)
                .AddSingleton(mockSnackbarConfiguration1.Object)
                .AddSingleton(mockSnackbarConfiguration2.Object)
                .AddSingleton(mockIosConfiguration1.Object)
                .AddSingleton(mockIosConfiguration2.Object)
                .AddSingleton(mockIosLocalConfiguration1.Object)
                .AddSingleton(mockIosLocalConfiguration2.Object)
                .AddSingleton(mockUwpConfiguration1.Object)
                .AddSingleton(mockUwpConfiguration2.Object));

                var builder = sp.GetService<IBuilder>();
                // act
                builder.UseConfiguration(2);

                // verify
#if __ANDROID__
                mockDroidConfiguration1.Verify(_ => _.Configure(It.IsAny<IDroidNotificationExtension>()), Times.Never);
                mockDroidConfiguration2.Verify(_ => _.Configure(It.IsAny<IDroidNotificationExtension>()), Times.Once);
                mockSnackbarConfiguration1.VerifyNoOtherCalls();
                mockSnackbarConfiguration2.VerifyNoOtherCalls();
                mockIosConfiguration1.VerifyNoOtherCalls();
                mockIosConfiguration2.VerifyNoOtherCalls();
                mockIosLocalConfiguration1.VerifyNoOtherCalls();
                mockIosLocalConfiguration2.VerifyNoOtherCalls();
                mockUwpConfiguration1.VerifyNoOtherCalls();
                mockUwpConfiguration2.VerifyNoOtherCalls();
#elif __IOS__
                mockDroidConfiguration1.VerifyNoOtherCalls();
                mockDroidConfiguration2.VerifyNoOtherCalls();
                mockSnackbarConfiguration1.VerifyNoOtherCalls();
                mockSnackbarConfiguration2.VerifyNoOtherCalls();
                mockIosConfiguration1.Verify(_ => _.Configure(It.IsAny<IIosNotificationExtension>()), Times.Never);
                mockIosConfiguration2.Verify(_ => _.Configure(It.IsAny<IIosNotificationExtension>()), Times.Once);
                mockIosLocalConfiguration1.VerifyNoOtherCalls();
                mockIosLocalConfiguration2.VerifyNoOtherCalls();
                mockUwpConfiguration1.VerifyNoOtherCalls();
                mockUwpConfiguration2.VerifyNoOtherCalls();
#elif NETFX_CORE
                mockDroidConfiguration1.VerifyNoOtherCalls();
                mockDroidConfiguration2.VerifyNoOtherCalls();
                mockSnackbarConfiguration1.VerifyNoOtherCalls();
                mockSnackbarConfiguration2.VerifyNoOtherCalls();
                mockIosConfiguration1.VerifyNoOtherCalls();
                mockIosConfiguration2.VerifyNoOtherCalls();
                mockIosLocalConfiguration1.VerifyNoOtherCalls();
                mockIosLocalConfiguration2.VerifyNoOtherCalls();
                mockUwpConfiguration1.Verify(_ => _.Configure(It.IsAny<IUwpExtension>()), Times.Never);
                mockUwpConfiguration2.Verify(_ => _.Configure(It.IsAny<IUwpExtension>()), Times.Once);
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

                mockDroidConfiguration1.Setup(_ => _.Token).Returns(1);
                mockDroidConfiguration2.Setup(_ => _.Token).Returns(2);
                mockSnackbarConfiguration1.Setup(_ => _.Token).Returns(1);
                mockSnackbarConfiguration2.Setup(_ => _.Token).Returns(2);
                mockIosConfiguration1.Setup(_ => _.Token).Returns(1);
                mockIosConfiguration2.Setup(_ => _.Token).Returns(2);
                mockIosLocalConfiguration1.Setup(_ => _.Token).Returns(1);
                mockIosLocalConfiguration2.Setup(_ => _.Token).Returns(2);
                mockUwpConfiguration1.Setup(_ => _.Token).Returns(1);
                mockUwpConfiguration2.Setup(_ => _.Token).Returns(2);

                using var sp = CreateContainer(_ => _
                .AddSingleton(mockDroidConfiguration1.Object)
                .AddSingleton(mockDroidConfiguration2.Object)
                .AddSingleton(mockSnackbarConfiguration1.Object)
                .AddSingleton(mockSnackbarConfiguration2.Object)
                .AddSingleton(mockIosConfiguration1.Object)
                .AddSingleton(mockIosConfiguration2.Object)
                .AddSingleton(mockIosLocalConfiguration1.Object)
                .AddSingleton(mockIosLocalConfiguration2.Object)
                .AddSingleton(mockUwpConfiguration1.Object)
                .AddSingleton(mockUwpConfiguration2.Object));

                var builder = sp.GetService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>();
                // act
                builder.UseConfiguration(2);

                // verify
#if __ANDROID__
                mockDroidConfiguration1.VerifyNoOtherCalls();
                mockDroidConfiguration2.VerifyNoOtherCalls();
                mockSnackbarConfiguration1.Verify(_ => _.Configure(It.IsAny<ISnackbarExtension>()), Times.Never);
                mockSnackbarConfiguration2.Verify(_ => _.Configure(It.IsAny<ISnackbarExtension>()), Times.Once);
                mockIosConfiguration1.VerifyNoOtherCalls();
                mockIosConfiguration2.VerifyNoOtherCalls();
                mockIosLocalConfiguration1.VerifyNoOtherCalls();
                mockIosLocalConfiguration2.VerifyNoOtherCalls();
                mockUwpConfiguration1.VerifyNoOtherCalls();
                mockUwpConfiguration2.VerifyNoOtherCalls();
#elif __IOS__
                mockDroidConfiguration1.VerifyNoOtherCalls();
                mockDroidConfiguration2.VerifyNoOtherCalls();
                mockSnackbarConfiguration1.VerifyNoOtherCalls();
                mockSnackbarConfiguration2.VerifyNoOtherCalls();
                mockIosConfiguration1.VerifyNoOtherCalls();
                mockIosConfiguration2.VerifyNoOtherCalls();
                mockIosLocalConfiguration1.Verify(_ => _.Configure(It.IsAny<IIosLocalNotificationExtension>()), Times.Never);
                mockIosLocalConfiguration2.Verify(_ => _.Configure(It.IsAny<IIosLocalNotificationExtension>()), Times.Once);
                mockUwpConfiguration1.VerifyNoOtherCalls();
                mockUwpConfiguration2.VerifyNoOtherCalls();
#elif NETFX_CORE
                mockDroidConfiguration1.VerifyNoOtherCalls();
                mockDroidConfiguration2.VerifyNoOtherCalls();
                mockSnackbarConfiguration1.VerifyNoOtherCalls();
                mockSnackbarConfiguration2.VerifyNoOtherCalls();
                mockIosConfiguration1.VerifyNoOtherCalls();
                mockIosConfiguration2.VerifyNoOtherCalls();
                mockIosLocalConfiguration1.VerifyNoOtherCalls();
                mockIosLocalConfiguration2.VerifyNoOtherCalls();
                mockUwpConfiguration1.Verify(_ => _.Configure(It.IsAny<IUwpExtension>()), Times.Never);
                mockUwpConfiguration2.Verify(_ => _.Configure(It.IsAny<IUwpExtension>()), Times.Once);
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

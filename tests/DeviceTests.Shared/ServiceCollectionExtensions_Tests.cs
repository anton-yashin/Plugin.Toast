﻿#if NETCORE_APP == false
#nullable enable

using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

#if __ANDROID__
using Plugin.Toast.Droid.Configuration;
#endif


namespace DeviceTests
{
    public class ServiceCollectionExtensions_Tests
    {
#if __ANDROID__

        [Fact]
        public void AddNotificationManager_Default()
        {
            // prepare && act
            var sc = new ServiceCollection();
            sc.AddNotificationManager(b => b.WithActivity(Platform.Activity));
            using var sp = sc.BuildServiceProvider();

            // verify
            CheckPlatform(sp, configurationExists: false, snackbarConfigurationExists: false);
        }

        [Fact]
        public void ShouldThrowException_WhenNoActivity()
        {
            // prepare 
            var sc = new ServiceCollection();
            // act && verify
            Assert.Throws<InvalidOperationException>(() => sc.AddNotificationManager(b => { }));
        }

        [Fact]
        public void AddNotificationManager_WithPlatformConfiguration_WithSnackbarConfiguration()
        {
            // prepare && act
            var sc = new ServiceCollection();
            sc.AddNotificationManager(b => b.WithActivity(Platform.Activity), ConfigurePlatform, ConfigureSnackbar);
            using var sp = sc.BuildServiceProvider();

            // verify
            CheckPlatform(sp, configurationExists: true, snackbarConfigurationExists: true);
        }

        [Fact]
        public void AddNotificationManager_WithPlatformConfiguration()
        {
            // prepare && act
            var sc = new ServiceCollection();
            sc.AddNotificationManager(b => b.WithActivity(Platform.Activity), ConfigurePlatform);
            using var sp = sc.BuildServiceProvider();

            // verify
            CheckPlatform(sp, configurationExists: true, snackbarConfigurationExists: false);
        }

        [Fact]
        public void AddNotificationManager_WithSnackbarConfiguration()
        {
            // prepare && act
            var sc = new ServiceCollection();
            sc.AddNotificationManager(b => b.WithActivity(Platform.Activity), ConfigureSnackbar);
            using var sp = sc.BuildServiceProvider();

            // verify
            CheckPlatform(sp, configurationExists: false, snackbarConfigurationExists: true);
        }

        static void ConfigurePlatform(Plugin.Toast.Droid.IPlatformSpecificExtension e) { }
        static void ConfigureSnackbar(ISnackbarExtension e) { }

        static void CheckPlatform(IServiceProvider sp, bool configurationExists, bool snackbarConfigurationExists)
        {
            CheckBase(sp);

            Assert.NotNull(sp.GetService<IActivityConfiguration>());
            Assert.NotNull(sp.GetService<INotificationStyleConfiguration>());
            Assert.NotNull(sp.GetService<Plugin.Toast.Droid.IIntentManager>());
            Assert.NotNull(sp.GetService<ISnackbarExtension>());
            Assert.NotNull(sp.GetService<IDroidNotificationExtension>());
            Assert.NotNull(sp.GetService<Plugin.Toast.Droid.IAndroidNotificationManager>());
            Assert.NotNull(sp.GetService<IBigTextStyle>());
            Assert.NotNull(sp.GetService<IInboxStyle>());

            if (configurationExists)
                Assert.NotNull(sp.GetService<IExtensionConfiguration<Plugin.Toast.Droid.IPlatformSpecificExtension>>());
            else
                Assert.Null(sp.GetService<IExtensionConfiguration<Plugin.Toast.Droid.IPlatformSpecificExtension>>());

            if (snackbarConfigurationExists)
                Assert.NotNull(sp.GetService<IExtensionConfiguration<ISnackbarExtension>>());
            else
                Assert.Null(sp.GetService<IExtensionConfiguration<ISnackbarExtension>>());
        }

#elif __IOS__

        [Fact]
        public void AddNotificationManager_Default()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
        {
            // prepare && act
            var sc = new ServiceCollection();
            sc.AddNotificationManager();
            using var sp = sc.BuildServiceProvider();

            // verify
            CheckPlatform(sp, configurationExists: false, legacyConfigurationExists: false);
        });

        [Fact]
        public void AddNotificationManager_WithPlatformConfiguration_WithLegacyConfiguration()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
        {
            // prepare && act
            var sc = new ServiceCollection();
            sc.AddNotificationManager(PlatformConfiguration, LegacyConfiguration);
            using var sp = sc.BuildServiceProvider();

            // verify
            CheckPlatform(sp, configurationExists: false, legacyConfigurationExists: false);
        });

        [Fact]
        public void AddNotificationManager_WithPlatformConfiguration()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
        {
            // prepare && act
            var sc = new ServiceCollection();
            sc.AddNotificationManager(PlatformConfiguration);
            using var sp = sc.BuildServiceProvider();

            // verify
            CheckPlatform(sp, configurationExists: false, legacyConfigurationExists: false);
        });

        [Fact]
        public void AddNotificationManager_WithLegacyConfiguration()
            => Platform.iOS_InvokeOnMainThreadAsync(() =>
        {
            // prepare && act
            var sc = new ServiceCollection();
            sc.AddNotificationManager(LegacyConfiguration);
            using var sp = sc.BuildServiceProvider();

            // verify
            CheckPlatform(sp, configurationExists: false, legacyConfigurationExists: false);
        });

        static void PlatformConfiguration(Plugin.Toast.IOS.IPlatformSpecificExtension e) { }
        static void LegacyConfiguration(IIosLocalNotificationExtension e) { }

        static void CheckPlatform(IServiceProvider sp, bool configurationExists, bool legacyConfigurationExists)
        {
            CheckBase(sp);
            Assert.NotNull(sp.GetService<Plugin.Toast.IOS.INotificationReceiver>());
            Assert.NotNull(sp.GetService<IIosNotificationExtension>());
            Assert.NotNull(sp.GetService<IIosLocalNotificationExtension>());
            Assert.NotNull(sp.GetService<Plugin.Toast.IOS.IPermission>());
            Assert.NotNull(sp.GetService<IInitialization>());

            if (configurationExists)
                Assert.NotNull(sp.GetService<IExtensionConfiguration<Plugin.Toast.IOS.IPlatformSpecificExtension>>());
            else
                Assert.Null(sp.GetService<IExtensionConfiguration<Plugin.Toast.IOS.IPlatformSpecificExtension>>());
            if (legacyConfigurationExists)
                Assert.NotNull(sp.GetService<IExtensionConfiguration<IIosLocalNotificationExtension>>());
            else
                Assert.Null(sp.GetService<IExtensionConfiguration<IIosLocalNotificationExtension>>());
        }

#elif NETFX_CORE

        [Fact]
        public void AddNotificationManager_Default()
        {
            // prepare && act
            var sc = new ServiceCollection();
            sc.AddNotificationManager();
            using var sp = sc.BuildServiceProvider();
            
            // verify
            CheckPlatform(sp, exists: false);
        }

        [Fact]
        public void AddNotificationManager_WithConfiguration()
        {
            // prepare && act
            var sc = new ServiceCollection();
            sc.AddNotificationManager(ex => { });
            using var sp = sc.BuildServiceProvider();

            // verify
            CheckPlatform(sp, exists: true);
        }

        static void CheckPlatform(IServiceProvider sp, bool exists)
        {
            CheckBase(sp);

            if (exists)
                Assert.NotNull(sp.GetService<IExtensionConfiguration<Plugin.Toast.UWP.IPlatformSpecificExtension>>());
            else
                Assert.Null(sp.GetService<IExtensionConfiguration<Plugin.Toast.UWP.IPlatformSpecificExtension>>());
        }
#endif

        static void CheckBase(IServiceProvider sp)
        {
            Assert.NotNull(sp.GetService<IInitialization>());
            Assert.NotNull(sp.GetService<INotificationManager>());
            Assert.NotNull(sp.GetService<ISystemEventSource>());
            Assert.NotNull(sp.GetService<INotificationEventSource>());
            Assert.NotNull(sp.GetService<IHistory>());
            Assert.NotNull(sp.GetService<INotificationBuilder<IUwpExtension, IUwpExtension, IUwpExtension, IUwpExtension>>());
            Assert.NotNull(sp.GetService<INotificationBuilder<IUwpExtension, IUwpExtension, IUwpExtension>>());
            Assert.NotNull(sp.GetService<INotificationBuilder<IUwpExtension, IUwpExtension>>());
            Assert.NotNull(sp.GetService<INotificationBuilder<IUwpExtension>>());
            Assert.NotNull(sp.GetService<INotificationBuilder>());
        }
    }
}

#endif
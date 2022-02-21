#if NETCORE_APP == false
#nullable enable

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Plugin.Toast;

#if NET6_0_OR_GREATER
using XDevice = Microsoft.Maui.Controls.Device;
#else
using XDevice = Xamarin.Forms.Device;
#endif

namespace DeviceTests
{
    public static class Platform

    {
#if __ANDROID__
        public static global::Android.App.Activity Activity { get; set; } = null!;
#elif __IOS__
        static Plugin.Toast.IOS.IPermission permission = new Plugin.Toast.IOS.Permission();
#elif NETFX_CORE
#endif

        public static INotificationManager CreateNotificationManager()
            => CreateServiceCollection()
            .BuildServiceProvider()
            .GetRequiredService<INotificationManager>();

        public static ServiceCollection CreateServiceCollection()
        {
            var sc = new ServiceCollection();
#if __IOS__
            sc.AddSingleton(permission);
#endif
#if __ANDROID__
            sc.AddNotificationManager(b => b.WithActivity(Activity));
#else
            sc.AddNotificationManager();
#endif
            return sc;
        }

        public static Task iOS_InvokeOnMainThreadAsync(Action action)
        {
            _ = action ?? throw new ArgumentNullException(nameof(action));
#if __ANDROID__ || NETFX_CORE || MAUI_WINDOWS
            action();
            return Task.CompletedTask;
#elif __IOS__
            var tcs = new TaskCompletionSource<object?>();
            XDevice.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    action();
                    tcs.TrySetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            });
            return tcs.Task;
#else
            throw new PlatformNotSupportedException();
#endif
        }

        public static Task iOS_InvokeOnMainThreadAsync(Func<Task> func)
        {
            _ = func ?? throw new ArgumentNullException(nameof(func));
#if __ANDROID__ || NETFX_CORE || MAUI_WINDOWS
            return func();
#elif __IOS__
            var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
            XDevice.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    await func();
                    tcs.TrySetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }
            });
            return tcs.Task;
#else
            throw new PlatformNotSupportedException();
#endif
        }
    }
}

#endif
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Toast;

namespace DeviceTests
{
    public static class Platform

    {
#if __ANDROID__
        public static global::Android.App.Activity Activity { get; set; } = null!;
#elif __IOS__
        static ToastOptions options = new ToastOptions();

        static Plugin.Toast.IOS.IPermission permission = new Plugin.Toast.IOS.Permission(options);
#elif NETFX_CORE
#endif

        public static INotificationManager CreateNotificationManager()
        {
#if __ANDROID__
            return new NotificationManager(new ToastOptions(Activity));
#elif NETFX_CORE
            return new NotificationManager(new ToastOptions());
#elif __IOS__
            return new NotificationManager(options, permission);
#endif
        }

        public static Task iOS_InvokeOnMainThreadAsync(Action action)
        {
            _ = action ?? throw new ArgumentNullException(nameof(action));
#if __ANDROID__ || NETFX_CORE
            action();
            return Task.CompletedTask;
#elif __IOS__
            var tcs = new TaskCompletionSource<object?>();
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
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
#error platform is not supported
#endif
        }

        public static Task iOS_InvokeOnMainThreadAsync(Func<Task> func)
        {
            _ = func ?? throw new ArgumentNullException(nameof(func));
#if __ANDROID__ || NETFX_CORE
            return func();
#elif __IOS__
            var tcs = new TaskCompletionSource<object?>();
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
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
#error platform is not supported
#endif
        }
    }
}

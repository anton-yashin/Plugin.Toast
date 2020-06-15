using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Toast;

namespace DeviceTests
{
    public static class Platform
    {
#if __ANDROID__
        public static global::Android.App.Activity Activity { get; set; } = null!;
#elif __IOS__
#elif NETFX_CORE
#endif

        public static INotificationManager CreateNotificationManager()
        {
#if __ANDROID__
            return new NotificationManager(new ToastOptions(Activity));
#else
            return new NotificationManager(new ToastOptions());
#endif
        }
    }
}

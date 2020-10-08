using System;
using System.Collections.Generic;
using Foundation;
using Plugin.Toast.IOS;
using UIKit;
using UserNotifications;

namespace Plugin.Toast
{
    public static partial class Platform
    {
        /// <summary>
        /// You must call this function if you want to receive an event received at application startup.
        /// Place call inside FinishedLaunching function.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        public static void OnActivated(UIApplication app, NSDictionary options)
        {
            UNUserNotificationCenter.Current.Delegate = new TemproraryNotificationCenterDelegate(AddPendingEvent);
        }
    }
}

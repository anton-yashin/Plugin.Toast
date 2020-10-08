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
        public static void OnActivated(UIApplication app, NSDictionary options)
        {
            UNUserNotificationCenter.Current.Delegate = new TemproraryNotificationCenterDelegate(AddPendingEvent);
        }
    }
}

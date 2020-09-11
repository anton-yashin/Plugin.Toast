using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.Droid
{
    static class Platform
    {
        public static bool IsOreo => global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O;
        public static bool IsLollipop => global::Android.OS.Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.Lollipop;
    }
}

using Microsoft.Maui.Devices;
using System;

namespace ManualTests.Maui.Services
{
    sealed class RuntimePlatform : IRuntimePlatform
    {
        public string RuntimePlatfrom => DeviceInfo.Platform.ToString();

        public bool IsIos => DeviceInfo.Platform == DevicePlatform.iOS;

        public bool IsAndroid => DeviceInfo.Platform == DevicePlatform.Android;

        public bool IsWindows => DeviceInfo.Platform == DevicePlatform.WinUI;

        public bool IsMaui => true;

        public bool IsXamarin => false;
    }
}

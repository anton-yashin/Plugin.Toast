using System;
using Xamarin.Forms;

namespace ManualTests.Xamarin.Services
{
    sealed class RuntimePlatform : IRuntimePlatform
    {
        public string RuntimePlatfrom => Device.RuntimePlatform;

        public bool IsIos => Device.RuntimePlatform == Device.iOS;

        public bool IsAndroid => Device.RuntimePlatform == Device.Android;

        public bool IsWindows => Device.RuntimePlatform == Device.UWP;

        public bool IsMaui => false;

        public bool IsXamarin => true;
    }
}

using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ManualTests.Maui.Services
{
    sealed class RuntimePlatform : IRuntimePlatform
    {
        public string RuntimePlatfrom => Device.RuntimePlatform;

        public bool IsIos => Device.RuntimePlatform == Device.iOS;

        public bool IsAndroid => Device.RuntimePlatform == Device.Android;

        public bool IsWindows => Device.RuntimePlatform == Device.UWP;

        public bool IsMaui => true;

        public bool IsXamarin => false;
    }
}

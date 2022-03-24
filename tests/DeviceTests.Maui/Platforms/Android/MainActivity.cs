using Android.OS;
using System;

namespace DeviceTests.Maui
{
    partial class MainActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            Platform.Activity = this;
            base.OnCreate(savedInstanceState);
        }
    }
}

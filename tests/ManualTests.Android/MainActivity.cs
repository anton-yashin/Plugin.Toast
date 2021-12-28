#nullable enable
using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Toast;
using Android.Content;
using Android.Util;
using Xamarin.Forms.Platform.Android;

namespace ManualTests.Droid
{
    [Activity(Label = "com.ManualTests.MainActivity", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            
            global::Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Plugin.Toast.Platform.OnActivated(this);
            LoadApplication(new App(_
                => _.AddNotificationManager(b => b.WithActivity(this))
                .AddNotificationManagerImagesSupport(fn => Resources.GetBitmapAsync(fn))));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            global::Xamarin.Essentials.Platform.OnRequestPermissionsResult(
                requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
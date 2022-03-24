#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Xamarin.Forms;
using Plugin.Toast;
using Plugin.Toast.UWP;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace ManualTests.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new ManualTests.App(_
                => _.AddNotificationManager().AddNotificationManagerImagesSupport(
                    global::Xamarin.Forms.Application.Current.OnThisPlatform().GetImageDirectory)));
        }

        protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
    }
}

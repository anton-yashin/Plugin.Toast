using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Plugin.Toast;
using UIKit;

namespace ManualTests.Maui
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            Platform.OnActivated(application, launchOptions);
            return base.FinishedLaunching(application, launchOptions);
        }
    }
}
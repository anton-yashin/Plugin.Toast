using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.UI.Xaml;
using Microsoft.Windows.AppLifecycle;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.System;
using LAppInstance = Microsoft.Windows.AppLifecycle.AppInstance;
using MauiWindow = Microsoft.Maui.Controls.Window;
using NativeWindow = Microsoft.UI.Xaml.Window;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ManualTests.Maui.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            await SingleInstanceLaunch();
            ProcessActivationArguments(LAppInstance.GetCurrent().GetActivatedEventArgs());
            base.OnLaunched(args);
            Microsoft.Maui.Essentials.Platform.OnLaunched(args);
            LAppInstance.GetCurrent().Activated += OnAppActivated;
        }

        bool ProcessActivationArguments(AppActivationArguments e)
        {
            if (e.Data is Windows.ApplicationModel.Activation.ToastNotificationActivatedEventArgs tnaea)
            {
                Plugin.Toast.Platform.OnActivated(tnaea);
                return true;
            }
            return false;
        }

        #region Single instance app support

        async Task SingleInstanceLaunch()
        {
            var main = LAppInstance.FindOrRegisterForKey("main");
            if (main.IsCurrent == false)
            {
                await main.RedirectActivationToAsync(LAppInstance.GetCurrent().GetActivatedEventArgs());
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                return;
            }
        }

        private void OnAppActivated(object? sender, AppActivationArguments e)
        {
            if (ProcessActivationArguments(e))
            {
                var window = GetMainWindow();
                if (window != null)
                    MoveWindowToForeground(window);
            }
        }

        NativeWindow? GetMainWindow()
            => (from i in Application.Windows
                let mw = i as MauiWindow
                where mw != null
                let nw = mw.Handler.MauiContext?.Services.GetService<NativeWindow>()
                where nw != null
                select nw).FirstOrDefault();

        static void MoveWindowToForeground(NativeWindow window)
        {
            if (window == null)
                throw new ArgumentNullException(nameof(window));
            // Bring the window to the foreground... first get the window handle...
            var hwnd = (Windows.Win32.Foundation.HWND)WinRT.Interop.WindowNative.GetWindowHandle(window);

            // Restore window if minimized... requires Microsoft.Windows.CsWin32 NuGet package and a NativeMethods.txt file with ShowWindow method
            Windows.Win32.PInvoke.ShowWindow(hwnd, Windows.Win32.UI.WindowsAndMessaging.SHOW_WINDOW_CMD.SW_RESTORE);

            // And call SetForegroundWindow... requires Microsoft.Windows.CsWin32 NuGet package and a NativeMethods.txt file with SetForegroundWindow method
            Windows.Win32.PInvoke.SetForegroundWindow(hwnd);
        }

        #endregion
    }
}

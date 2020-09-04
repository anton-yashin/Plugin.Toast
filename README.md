# Notification plugin for Xamarin

Show local notifications using Xamarin for Android, iOS or UWP. Beta version.

## Target frameworks

### netstandard1.4, netstandard2.0

### Xamarin.iOS10 
Available implementations:
* [IIosNotificationExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/IIosNotificationExtension.shared.cs), using UNUserNotificationCenter
* [IIosLocalNotificationExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/IIosLocalNotificationExtension.shared.cs), using UILocalNotification

### MonoAndroid80, MonoAndroid81, MonoAndroid90, MonoAndroid10.0
* [IDroidNotificationExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/IDroidNotificationExtension.shared.cs), using Android.Support.V4.App.NotificationCompat
* [ISnackbarExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/ISnackbarExtension.shared.cs), using Android.Support.Design.Widget.Snackbar

### uap10.0.16299
* [IUwpExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/IUwpExtension.shared.cs), using Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder

## Usage

### Setup

* Available on NuGet: https://www.nuget.org/packages/Xamarin.Plugin.Toast/
* Ensure that you install Plugin.Toast into all your projects.

### Initialization

Initialize plugin using NotificationManager.Init()

You can also use Microsoft Dependency Injection and add a notification manager
in you service collection using ServiceCollectionExtensions.AddNotificationManager()

If you are using Android you must pass activity to the notification manager. 
You can also use ToastOptions class to set some defaults.

### Show notifications

```csharp
// using NotificationManager
NotificationResult result;
result = await NotificationManager.Instance.BuildNotification()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync();

// hide notification using cancellation token
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
result = await NotificationManager.Instance.BuildNotification()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync(cts.Token);

// use snackbar on android and local notifications on ios
result = await NotificationManager.Instance
.BuildNotificationUsing<ISnackbarExtension, IIosLocalNotificationExtension>()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync();

// pass platform specific parameters to builder
result = await NotificationManager.Instance.BuildNotification().AddTitle("title")
	.WhenUsing<IDroidNotificationExtension>(_ => _.AddDescription("droid description").SetColor(droidColor))
	.WhenUsing<IIosNotificationExtension>(_ => _.AddDescription("ios description"))
	.WhenUsing<IUwpExtension>(_ => _.AddDescription("uwp description").AddHeroImage(new Uri("ms-appx:///hero-image.png")))
	.Build().ShowAsync();


// using DI
result = await serviceProvider.GetService<IBuilder>()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync();

// hide notification using cancellation token
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
result = await serviceProvider.GetService<IBuilder>()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync(cts.Token);

// use snackbar on android and local notifications on ios
result = await serviceProvider.GetService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync();

// pass platform specific parameters to builder
result = await serviceProvider.GetService<IBuilder>().AddTitle("title")
	.WhenUsing<IDroidNotificationExtension>(_ => _.AddDescription("droid description").SetColor(droidColor))
	.WhenUsing<IIosNotificationExtension>(_ => _.AddDescription("ios description"))
	.WhenUsing<IUwpExtension>(_ => _.AddDescription("uwp description").AddHeroImage(new Uri("ms-appx:///hero-image.png")))
	.Build().ShowAsync();

switch (result)
{
	case NotificationResult.Activated:
		// The user activated the toast notification
		break;
	case NotificationResult.UserCanceled:
		// The user dismissed the toast notification.
		break;
	case NotificationResult.ApplicationHidden:
		// The app explicitly hid the toast notification.
		break;
	case TimedOut:
		// The toast notification had been shown for the maximum allowed time and was faded out.
		break;
}

// You can schedule notification using NotificationManger
var hideToken = NotificationManager.Instance.BuildNotification().AddDescription("description").AddTitle("title")
	.Build().ScheduleTo(deliveryTime);
await Task.Delay(someTime);
hideToken.Dispose(); // hide notification

// You can schedule notification using DI container
hideToken = serviceProvider.GetService<IBuilder>().AddDescription("description").AddTitle("title")
	.Build().ScheduleTo(deliveryTime);
await Task.Delay(someTime);
hideToken.Dispose(); // hide notification

```
See also: [Droid.IPlatformSpecificExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Droid/IPlatformSpecificExtension.android.cs), [IOS.IPlatformSpecificExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/IOS/IPlatformSpecificExtension.ios.cs)

### Advanced usage
You can encapsulate common configurations using [IExtensionConfiguration](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/IExtensionConfiguration.shared.cs) or [ISpecificExtensionConfiguration](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/ISpecificExtensionConfiguration.shared.cs) and add
to your service collection or use with specific extensions.

See also: [IBuilder.UseConfiguration<T>(T)](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/IBuilder.shared.cs), [IBuilderExtension<T>.Use(IExtensionConfiguration<T>)](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/IBuilderExtension.shared.cs)

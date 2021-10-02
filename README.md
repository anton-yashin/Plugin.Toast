# Notification plugin for Xamarin

Show local notifications using Xamarin for Android, iOS or UWP. Beta version.

## Target frameworks

### netstandard1.4, netstandard2.0

### Xamarin.iOS10 
Available implementations:
* [IIosNotificationExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/IIosNotificationExtension.shared.cs),
uses [UNUserNotificationCenter](https://docs.microsoft.com/en-us/dotnet/api/usernotifications.unusernotificationcenterdelegate)
* [IIosLocalNotificationExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/IIosLocalNotificationExtension.shared.cs),
uses [UILocalNotification](https://docs.microsoft.com/en-us/dotnet/api/uikit.uilocalnotification)

### MonoAndroid90, MonoAndroid10.0, MonoAndroid11.0
* [IDroidNotificationExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/IDroidNotificationExtension.shared.cs),
uses [AndroidX.Core.App.NotificationCompat](https://developer.android.com/reference/androidx/core/app/NotificationCompat)
* [ISnackbarExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/ISnackbarExtension.shared.cs),
uses [Google.Android.Material.Snackbar.Snackbar](https://developer.android.com/reference/com/google/android/material/snackbar/Snackbar)

### uap10.0.16299
* [IUwpExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/IUwpExtension.shared.cs),
uses [Microsoft.Toolkit.Uwp.Notifications.ToastContentBuilder](https://docs.microsoft.com/en-us/dotnet/api/microsoft.toolkit.uwp.notifications.toastcontentbuilder)

## Usage

### Setup

* Available on NuGet:
* Basic support https://www.nuget.org/packages/Xamarin.Plugin.Toast/
* Images support https://www.nuget.org/packages/Xamarin.Plugin.Toast.Images/
* Ensure that you install Plugin.Toast into all your projects.

### Initialization

Initialize plugin using [Microsoft Dependency Injection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection)
and add a notification manager to your service collection using ServiceCollectionExtensions.AddNotificationManager().
If you want a images support, you must add call ServiceCollectionImagesExtensions.AddNotificationManagerImagesSupport()
to add it to your service collection.

If you are using Android then you must pass activity to the notification manager. 
You can also use ToastOptions class or a builder action parameter to set some defaults.

### Show notifications

```csharp
// register in IoC at android
serviceCollection.AddNotificationManager(yourActivity);

// for image support at android
serviceCollection.AddNotificationManagerImagesSupport(fn => yourActivity.Resources.GetBitmapAsync(fn))));

// register in IoC at iOs
serviceCollection.AddNotificationManager();

// for image support at iOs
serviceCollection.AddNotificationManagerImagesSupport();

// register in IoC at UWP
serviceCollection.AddNotificationManager();

// for image support at UWP
serviceCollection.AddNotificationManagerImagesSupport(Xamarin.Forms.Application.Current.OnThisPlatform().GetImageDirectory);

// use NotificationManager via IoC
var notificationManager = serviceProvider.GetRequiredService<INotificationManager>()

NotificationResult result;
result = await notificationManager.GetBuilder()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync();

// hide notification using cancellation token
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
result = await notificationManager.GetBuilder()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync(cts.Token);

// use snackbar on android and local notifications on ios
result = await notificationManager.GetBuilder<ISnackbarExtension, IIosLocalNotificationExtension>()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync();

// pass platform specific parameters to builder
result = await notificationManager.GetBuilder().AddTitle("title")
	.WhenUsing<IDroidNotificationExtension>(_ => _.AddDescription("droid description").SetColor(droidColor))
	.WhenUsing<IIosNotificationExtension>(_ => _.AddDescription("ios description"))
	.WhenUsing<IUwpExtension>(_ => _.AddDescription("uwp description").AddHeroImage(new Uri("ms-appx:///hero-image.png")))
	.Build().ShowAsync();

// get builder for IoC
result = await serviceProvider.GetService<INotificationBuilder>()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync();
	
// show images
var toastImageSourceFactory = serviceProvider.GetRequiredService<IToastImageSourceFactory>();
var fromFile = await toastImageSourceFactory.FromFileAsync(someFileName);
var fromResource = await toastImageSourceFactory.FromResourceAsync(someResourcePath, typeof(SomeTypeInYourAssembly));
var fromUri = await toastImageSourceFactory.FromUriAsync(new Uri("https://www.yoursite.com/yourimage.jpg"));
result = await serviceProvider.GetService<INotificationBuilder>()
	.AddDescription("description").AddTitle("title")
	.AddImage(fromFile)
	.Build().ShowAsync();


// hide notification using cancellation token
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
result = await notificationManager.GetBuilder()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync(cts.Token);

// use snackbar on android and local notifications on ios
result = await serviceProvider.GetService<IBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync();

// pass platform specific parameters to builder
result = await serviceProvider.GetService<INotificationBuilder>().AddTitle("title")
	.WhenUsing<IDroidNotificationExtension>(_ => _.AddDescription("droid description").SetColor(droidColor))
	.WhenUsing<IIosNotificationExtension>(_ => _.AddDescription("ios description"))
	.WhenUsing<IUwpExtension>(_ => _.AddDescription("uwp description").AddHeroImage(fromUri))
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

// You can schedule a notification using NotificationManger
var cancellationToken = notificationManager.GetBuilder().AddDescription("description").AddTitle("title")
	.Build().ScheduleTo(deliveryTime);
await Task.Delay(someTime);
cancellationToken.Dispose(); // remove from schedule

// You can schedule a notification using IoC container
cancellationToken = serviceProvider.GetService<INotificationBuilder>().AddDescription("description").AddTitle("title")
	.Build().ScheduleTo(deliveryTime);
await Task.Delay(someTime);
cancellationToken.Dispose(); // remove from schedule

```
See also: [Droid.IPlatformSpecificExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/Droid/IPlatformSpecificExtension.android.cs),
[IOS.IPlatformSpecificExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/IOS/IPlatformSpecificExtension.ios.cs)

### Work with notification history

```csharp
// get history instance
var history = serviceProvider.GetService<IHistory>();

// check if notification has been delivered
var isDelivered = await history.IsDeliveredAsync(notificationId);
// check if notification has been scheduled
var isScheduled = await history.IsScheduledAsync(notificationId);
// remove delivered notification from notification center
history.RemoveDelivered(notificationId);
// remove a notification from the schedule
history.RemoveScheduled(notificationId);
// remove all notification of the current application from notification center
history.RemoveAllDelivered();
```

### Watch for notifications

To capture notifications at application startup you must place call to Platform.OnActivated
on you overrides: [Activity.OnCreate](https://github.com/anton-yashin/Plugin.Toast/blob/master/tests/ManualTests.Android/MainActivity.cs)
at android, [UIApplicationDelegate.FinishedLaunching](https://github.com/anton-yashin/Plugin.Toast/blob/master/tests/ManualTests.iOS/AppDelegate.cs)
at ios and [Application.OnActivated](https://github.com/anton-yashin/Plugin.Toast/blob/master/tests/ManualTests.UWP/App.xaml.cs) override at uwp.
If the notification contains a valid identifier, you will receive it.

```csharp
// get event source instance
var eventSource = serviceProvider.GetService<INotificationEventSource>();

// watch for events
eventSource.NotificationReceived += OnNotificationReceived;
// send events that were captured on application startup
eventSource.SendPendingEvents();

```
See also: [ISystemEventSource](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/ISystemEventSource.shared.cs),
[INotificationEventObserver](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/INotificationEventObserver.shared.cs).

### Advanced usage
You can encapsulate common configurations using [IExtensionConfiguration](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/Abstractions/IExtensionConfiguration.shared.cs)
or [ISpecificExtensionConfiguration](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/Abstractions/ISpecificExtensionConfiguration.shared.cs)
and add to your service collection or use with specific extensions.

You can create a plugin to route your data into a platform specific implementation using
[IExtensionPlugin](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/Abstractions/IExtensionPlugin.shared.cs).
Create implementations, add to your service collection, then use [IBuilder.Add<T, ...>(T data, ...)](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/Abstractions/IBuilder.shared.cs).
See the [Images](https://github.com/anton-yashin/Plugin.Toast/tree/master/src/Plugin.Toast.Images)
plugin code as a working example.

See also: [IBuilder.UseConfiguration<T>(T)](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/Abstractions/IBuilder.shared.cs),
[IBuilderExtension<T>.Use(IExtensionConfiguration<T>)](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/Abstractions/IBuilderExtension.shared.cs)

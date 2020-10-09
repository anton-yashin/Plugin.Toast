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

* Available on NuGet:
* Basic support https://www.nuget.org/packages/Xamarin.Plugin.Toast/
* Images support https://www.nuget.org/packages/Xamarin.Plugin.Toast.Images/ (IoC required)
* Ensure that you install Plugin.Toast into all your projects.

### Initialization

Initialize plugin using NotificationManager.Init()

You can also use Microsoft Dependency Injection and add a notification manager
in you service collection using ServiceCollectionExtensions.AddNotificationManager().
If you want a image support, you must add call ServiceCollectionImagesExtensions.AddNotificationManagerImagesSupport()
to add it to your service collection.

If you are using Android you must pass activity to the notification manager. 
You can also use ToastOptions class or a builder action parameter to set some defaults.

### Show notifications

```csharp
// using NotificationManager
NotificationResult result;
result = await NotificationManager.Instance.GetBuilder()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync();

// hide notification using cancellation token
using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(1));
result = await NotificationManager.Instance.GetBuilder()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync(cts.Token);

// use snackbar on android and local notifications on ios
result = await NotificationManager.Instance.GetBuilder<ISnackbarExtension, IIosLocalNotificationExtension>()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync();

// pass platform specific parameters to builder
result = await NotificationManager.Instance.GetBuilder().AddTitle("title")
	.WhenUsing<IDroidNotificationExtension>(_ => _.AddDescription("droid description").SetColor(droidColor))
	.WhenUsing<IIosNotificationExtension>(_ => _.AddDescription("ios description"))
	.WhenUsing<IUwpExtension>(_ => _.AddDescription("uwp description").AddHeroImage(new Uri("ms-appx:///hero-image.png")))
	.Build().ShowAsync();

// register @ IoC
serviceCollection.AddNotificationManager();
// for image support
serviceCollection.AddNotificationManagerImagesSupport();

// using DI
result = await serviceProvider.GetService<IBuilder>()
	.AddDescription("description").AddTitle("title")
	.Build().ShowAsync();
	
// show images
var notificationManager = serviceProvider.GetRequiredService<INotificationManager>()
var toastImageSourceFactory = serviceProvider.GetRequiredService<IToastImageSourceFactory>();
var fromFile = await toastImageSourceFactory.FromFileAsync(someFileName);
var fromResource = await toastImageSourceFactory.FromResourceAsync(someResourcePath, typeof(SomeTypeInYourAssembly));
var fromUri = await toastImageSourceFactory.FromUriAsync(new Uri("https://www.yoursite.com/yourimage.jpg"));
result = await serviceProvider.GetService<IBuilder>()
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
result = await serviceProvider.GetService<IBuilder>().AddTitle("title")
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
var cancellationToken = NotificationManager.Instance.GetBuilder().AddDescription("description").AddTitle("title")
	.Build().ScheduleTo(deliveryTime);
await Task.Delay(someTime);
cancellationToken.Dispose(); // remove from schedule

// You can schedule a notification using DI container
cancellationToken = serviceProvider.GetService<IBuilder>().AddDescription("description").AddTitle("title")
	.Build().ScheduleTo(deliveryTime);
await Task.Delay(someTime);
cancellationToken.Dispose(); // remove from schedule

```
See also: [Droid.IPlatformSpecificExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Droid/IPlatformSpecificExtension.android.cs), [IOS.IPlatformSpecificExtension](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/IOS/IPlatformSpecificExtension.ios.cs)

### Work with notification history

```csharp
// get history instance
IHistory history;
history = NotificationManager.History;
history = serviceProvider.GetService<IHistory>();

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
on you overrides: Activity.OnCreate @ android, UIApplicationDelegate.FinishedLaunching @ ios
and Application.OnActivated override @ uwp. If the notification contains a valid identifier, you
will receive it.

```csharp
// get event source instance
INotificationEventSource eventSource;
eventSource = NotificationManager.GetNotificationEventSource();
eventSource = serviceProvider.GetService<INotificationEventSource>();

// watch for events
eventSource.NotificationReceived += OnNotificationReceived;
// send events that captured on application startup
eventSource.SendPendingEvents();

```
See also: [ISystemEventSource](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/ISystemEventSource.shared.cs),
[INotificationEventObserver](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/INotificationEventObserver.shared.cs).

### Advanced usage
You can encapsulate common configurations using [IExtensionConfiguration](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/IExtensionConfiguration.shared.cs) or [ISpecificExtensionConfiguration](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/ISpecificExtensionConfiguration.shared.cs) and add
to your service collection or use with specific extensions.

You can create a plugin to route your data into a platform specific implementation using [IExtensionPlugin](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/IExtensionPlugin.shared.cs). Create implementations, add to your service collection, then use [IBuilder.Add<T, ...>(T data, ...)](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/Plugin.Toast/IBuilder.shared.cs). You can see the [Images](https://github.com/anton-yashin/Plugin.Toast/tree/master/src/Plugin.Toast.Images) plugin code as a working example.

See also: [IBuilder.UseConfiguration<T>(T)](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/IBuilder.shared.cs), [IBuilderExtension<T>.Use(IExtensionConfiguration<T>)](https://github.com/anton-yashin/Plugin.Toast/blob/master/src/IBuilderExtension.shared.cs)

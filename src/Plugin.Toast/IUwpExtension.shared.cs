using Microsoft.Toolkit.Uwp.Notifications;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public interface IUwpExtension : IBuilderExtension<IUwpExtension>
    {
        /// <summary>
        /// Override the app logo with custom image of choice that will be displayed on the toast.
        /// </summary>
        /// <param name="uri">The URI of the image. Can be from your application package, application data, or the internet. Internet images must be less than 200 KB in size.</param>
        /// <param name="hintCrop">Specify how the image should be cropped.</param>
        /// <param name="alternateText">A description of the image, for users of assistive technologies.</param>
        /// <param name="addImageQuery">A value whether Windows is allowed to append a query string to the image URI supplied in the Tile notification.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddAppLogoOverride(Uri uri, ToastGenericAppLogoCrop? hintCrop = null, string? alternateText = null, bool? addImageQuery = null);

        /// <summary>
        /// Add an Attribution Text to be displayed on the toast.
        /// </summary>
        /// <param name="text">Text to be displayed as Attribution Text</param>
        /// <param name="language">The target locale of the XML payload, specified as a BCP-47 language tags such as "en-US" or "fr-FR".</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddAttributionText(string text, string? language = null);
        /// <summary>
        /// Set custom audio to go along with the toast.
        /// </summary>
        /// <param name="src">Source to the media that will be played when the toast is pop</param>
        /// <param name="loop">Indicating whether sound should repeat as long as the Toast is shown; false to play only once (default).</param>
        /// <param name="silent">Indicating whether sound is muted; false to allow the Toast notification sound to play (default).</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddAudio(Uri src, bool? loop = null, bool? silent = null);
        /// <summary>
        /// Add a button to the current toast.
        /// </summary>
        /// <param name="content">Text to display on the button.</param>
        /// <param name="activationType">Type of activation this button will use when clicked. Defaults to Foreground.</param>
        /// <param name="arguments">App-defined string of arguments that the app can later retrieve once it is activated when the user clicks the button.</param>
        /// <param name="imageUri">Optional image icon for the button to display (required for buttons adjacent to inputs like quick reply).</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddButton(string content, ToastActivationType activationType, string arguments, Uri? imageUri = null);
        /// <summary>
        /// Add an button to the toast that will be display to the right of the input text box, achieving a quick reply scenario.
        /// </summary>
        /// <param name="textBoxId">ID of an existing <see cref="ToastTextBox"/> in order to have this button display to the right of the input, achieving a quick reply scenario.</param>
        /// <param name="content">Text to display on the button.</param>
        /// <param name="activationType">Type of activation this button will use when clicked. Defaults to Foreground.</param>
        /// <param name="arguments">App-defined string of arguments that the app can later retrieve once it is activated when the user clicks the button.</param>
        /// <param name="imageUri">An optional image icon for the button to display (required for buttons adjacent to inputs like quick reply)</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddButton(string textBoxId, string content, ToastActivationType activationType, string arguments, Uri? imageUri = null);
        /// <summary>
        /// Add a button to the current toast.
        /// </summary>
        /// <param name="button">An instance of class that implement <see cref="IToastButton"/> for the button that will be used on the toast.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddButton(IToastButton button);
        /// <summary>
        /// Add a combo box / drop-down menu that contain options for user to select.
        /// </summary>
        /// <param name="id">Required ID property used so that developers can retrieve user input once the app is activated.</param>
        /// <param name="title">Title text to display above the Combo Box.</param>
        /// <param name="defaultSelectionBoxItemId">Sets which item is selected by default, and refers to the Id property of <see cref="ToastSelectionBoxItem"/>. If you do not provide this or null, the default selection will be empty (user sees nothing).</param>
        /// <param name="choices">List of choices that will be available for user to select.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddComboBox(string id, string title, string defaultSelectionBoxItemId, (string comboBoxItemId, string comboBoxItemContent)[] choices);
        /// <summary>
        /// Add a combo box / drop-down menu that contain options for user to select.
        /// </summary>
        /// <param name="id">Required ID property used so that developers can retrieve user input once the app is activated.</param>
        /// <param name="defaultSelectionBoxItemId">Sets which item is selected by default, and refers to the Id property of <see cref="ToastSelectionBoxItem"/>. If you do not provide this or null, the default selection will be empty (user sees nothing).</param>
        /// <param name="choices">List of choices that will be available for user to select.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddComboBox(string id, string defaultSelectionBoxItemId, params (string comboBoxItemId, string comboBoxItemContent)[] choices);
        /// <summary>
        /// Add a combo box / drop-down menu that contain options for user to select.
        /// </summary>
        /// <param name="id">Required ID property used so that developers can retrieve user input once the app is activated.</param>
        /// <param name="choices">List of choices that will be available for user to select.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddComboBox(string id, params (string comboBoxItemId, string comboBoxItemContent)[] choices);
        /// <summary>
        /// Add a combo box / drop-down menu that contain options for user to select.
        /// </summary>
        /// <param name="id">Required ID property used so that developers can retrieve user input once the app is activated.</param>
        /// <param name="title">Title text to display above the Combo Box.</param>
        /// <param name="defaultSelectionBoxItemId">Sets which item is selected by default, and refers to the Id property of <see cref="ToastSelectionBoxItem"/>. If you do not provide this or null, the default selection will be empty (user sees nothing).</param>
        /// <param name="choices">List of choices that will be available for user to select.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddComboBox(string id, string title, string defaultSelectionBoxItemId, IEnumerable<(string comboBoxItemId, string comboBoxItemContent)> choices);
        /// <summary>
        /// Add custom time stamp on the toast to override the time display on the toast.
        /// </summary>
        /// <param name="dateTime">Custom Time to be displayed on the toast</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddCustomTimeStamp(DateTime dateTime);
        /// <summary>
        /// Add a header to a toast.
        /// </summary>
        /// <param name="id">A developer-created identifier that uniquely identifies this header. If two notifications have the same header id, they will be displayed underneath the same header in Action Center.</param>
        /// <param name="title">A title for the header.</param>
        /// <param name="arguments">A developer-defined string of arguments that is returned to the app when the user clicks this header.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        /// <remarks>More info about toast header: https://docs.microsoft.com/en-us/windows/uwp/design/shell/tiles-and-notifications/toast-headers </remarks>
        IUwpExtension AddHeader(string id, string title, string arguments);
        /// <summary>
        /// Add a hero image to the toast.
        /// </summary>
        /// <param name="uri">The URI of the image. Can be from your application package, application data, or the internet. Internet images must be less than 200 KB in size.</param>
        /// <param name="alternateText">A description of the image, for users of assistive technologies.</param>
        /// <param name="addImageQuery">A value whether Windows is allowed to append a query string to the image URI supplied in the Tile notification.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddHeroImage(Uri uri, string? alternateText = null, bool? addImageQuery = null);
        /// <summary>
        /// Add an image inline with other toast content.
        /// </summary>
        /// <param name="uri">The URI of the image. Can be from your application package, application data, or the internet. Internet images must be less than 200 KB in size.</param>
        /// <param name="alternateText">A description of the image, for users of assistive technologies.</param>
        /// <param name="addImageQuery">A value whether Windows is allowed to append a query string to the image URI supplied in the Tile notification.</param>
        /// <param name="hintCrop">A value whether a margin is removed. images have an 8px margin around them.</param>
        /// <param name="hintRemoveMargin">the horizontal alignment of the image.This is only supported when inside an <see cref="AdaptiveSubgroup"/>.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddInlineImage(Uri uri, string? alternateText = null, bool? addImageQuery = null, AdaptiveImageCrop? hintCrop = null, bool? hintRemoveMargin = null);
        /// <summary>
        /// Add an input text box that the user can type into.
        /// </summary>
        /// <param name="id">Required ID property so that developers can retrieve user input once the app is activated.</param>
        /// <param name="placeHolderContent">Placeholder text to be displayed on the text box when the user hasn't typed any text yet.</param>
        /// <param name="title">Title text to display above the text box.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddInputTextBox(string id, string? placeHolderContent = null, string? title = null);
        /// <summary>
        /// Add a progress bar to the toast.
        /// </summary>
        /// <param name="title">Title of the progress bar.</param>
        /// <param name="value">Value of the progress bar. Default is 0</param>
        /// <param name="isIndeterminate">Determine if the progress bar value should be indeterminate. Default to false.</param>
        /// <param name="valueStringOverride">An optional string to be displayed instead of the default percentage string. If this isn't provided, something like "70%" will be displayed.</param>
        /// <param name="status">A status string which is displayed underneath the progress bar. This string should reflect the status of the operation, like "Downloading..." or "Installing...". Default to empty.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        /// <remarks>More info at: https://docs.microsoft.com/en-us/windows/uwp/design/shell/tiles-and-notifications/toast-progress-bar </remarks>
        IUwpExtension AddProgressBar(string? title = null, double? value = null, bool isIndeterminate = false, string? valueStringOverride = null, string? status = null);
        /// <summary>
        /// Add text to the toast.
        /// </summary>
        /// <param name="text">Custom text to display on the tile.</param>
        /// <param name="hintStyle">Style that controls the text's font size, weight, and opacity.</param>
        /// <param name="hintWrap">Indicating whether text wrapping is enabled. For Tiles, this is false by default.</param>
        /// <param name="hintMaxLines">The maximum number of lines the text element is allowed to display. For Tiles, this is infinity by default</param>
        /// <param name="hintMinLines">The minimum number of lines the text element must display.</param>
        /// <param name="hintAlign">The horizontal alignment of the text</param>
        /// <param name="language">
        /// The target locale of the XML payload, specified as a BCP-47 language tags such as "en-US" or "fr-FR". The locale specified here overrides any other specified locale, such as that in binding or visual.
        /// </param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        /// <exception cref="InvalidOperationException">Throws when attempting to add/reserve more than 4 lines on a single toast. </exception>
        /// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="hintMaxLines"/> value is larger than 2. </exception>
        /// <remarks>More info at: https://docs.microsoft.com/en-us/windows/uwp/design/shell/tiles-and-notifications/adaptive-interactive-toasts#text-elements</remarks>
        IUwpExtension AddText(string text, AdaptiveTextStyle? hintStyle = null, bool? hintWrap = null, int? hintMaxLines = null, int? hintMinLines = null, AdaptiveTextAlign? hintAlign = null, string? language = null);
        /// <summary>
        /// Add info that can be used by the application when the app was activated/launched by the toast.
        /// </summary>
        /// <param name="launchArgs">Custom app-defined launch arguments to be passed along on toast activation</param>
        /// <param name="activationType">Set the activation type that will be used when the user click on this toast</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddToastActivationInfo(string launchArgs, ToastActivationType activationType);
        /// <summary>
        /// Add an input option to the Toast.
        /// </summary>
        /// <param name="input">An instance of a class that implement <see cref="IToastInput"/> that will be used on the toast.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddToastInput(IToastInput input);
        /// <summary>
        /// Add a visual element to the toast.
        /// </summary>
        /// <param name="child">An instance of a class that implement <see cref="IToastBindingGenericChild"/>.</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension AddVisualChild(IToastBindingGenericChild child);
        /// <summary>
        /// Sets the amount of time the Toast should display. You typically should use the
        /// Scenario attribute instead, which impacts how long a Toast stays on screen.
        /// </summary>
        /// <param name="duration">Duration of the toast</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension SetToastDuration(ToastDuration duration);
        /// <summary>
        ///  Sets the scenario, to make the Toast behave like an alarm, reminder, or more.
        /// </summary>
        /// <param name="scenario">Scenario to be used for the toast's behavior</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension SetToastScenario(ToastScenario scenario);

        /// <summary>
        /// Sets a string that uniquely identifies a toast notification inside a group.
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension SetTag(string tag);
        /// <summary>
        /// Sets the group identifier for the notification.
        /// </summary>
        /// <param name="group">Group identifier</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension SetGroup(string group);
        /// <summary>
        /// Sets a remote id for the notification that enables the system to correlate
        /// this notification with another one generated on another device.
        /// </summary>
        /// <param name="remoteId">Remote id</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension SetRemoteId(string remoteId);
        /// <summary>
        /// Setup a snoze interval and maximum snooze count. Works with <see cref="INotification.ScheduleTo(DateTimeOffset)"/>
        /// </summary>
        /// <param name="snoozeInterval">
        /// The amount of time between occurrences of the notification.<br/>
        /// The time between occurrences of the notification. This value will be between
        /// 60 seconds and 60 minutes, inclusive.
        /// </param>
        /// <param name="maximumSnoozeCount">
        /// The maximum number of times to display this notification.<br/>
        /// This will be a value between 1 and 5, inclusive.
        /// </param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension SetupSnooze(TimeSpan snoozeInterval, uint maximumSnoozeCount);
        /// <summary>
        /// Sets whether a toast's pop-up UI is displayed on the user's screen.
        /// Works with <see cref="INotification.ScheduleTo(DateTimeOffset)"/>
        /// </summary>
        /// <param name="suppressPopup">True if notification must be placed silently in notification center</param>
        /// <returns>The current instance of <see cref="IUwpExtension"/></returns>
        IUwpExtension SetSuppressPopup(bool suppressPopup);
    }
}

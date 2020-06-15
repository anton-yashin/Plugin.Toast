using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;

namespace Plugin.Toast.UWP
{
    sealed class NotificationBuilder : IPlatformSpecificExtension, IUwpExtension, IBuilder
    {
        readonly ToastContentBuilder tbc;
        private readonly IServiceProvider? serviceProvider;
        bool buildCompleted;

        public NotificationBuilder(IServiceProvider? serviceProvider)
        {
            tbc = new ToastContentBuilder();
            this.UseConfigurationFrom<IUwpExtension>(serviceProvider);
            this.UseConfigurationFrom<IPlatformSpecificExtension>(serviceProvider);
            this.serviceProvider = serviceProvider;
        }

        NotificationBuilder AddTitle(string title)
        {
            tbc.AddText(title, AdaptiveTextStyle.Title);
            return this;
        }

        NotificationBuilder AddDescription(string description)
        {
            tbc.AddText(description, AdaptiveTextStyle.Default);
            return this;
        }

        #region IBuilder implementation

        public INotification Build()
        {
            if (buildCompleted == true)
                throw Exceptions.ExceptionUtils.BuildTwice;
            buildCompleted = true;
            return new Notification(tbc.Content);
        }

        IBuilder IBuilder.AddTitle(string title) => AddTitle(title);

        IBuilder IBuilder.AddDescription(string description) => AddDescription(description);

        public IBuilder WhenUsing<T>(Action<T> buildAction) where T : IBuilderExtension<T>
        {
            if (this is T t)
                buildAction(t);
            return this;
        }

        public IBuilder UseConfiguration<T>(T token)
        {
            this.UseConfigurationFrom<IUwpExtension, T>(serviceProvider, token);
            this.UseConfigurationFrom<IPlatformSpecificExtension, T>(serviceProvider, token);
            return this;
        }

        #endregion

        #region IUwpExtension implementation

        public IUwpExtension AddAppLogoOverride(Uri uri, ToastGenericAppLogoCrop? hintCrop = null, string? alternateText = null, bool? addImageQuery = null)
        {
            tbc.AddAppLogoOverride(uri, hintCrop, alternateText, addImageQuery);
            return this;
        }

        public IUwpExtension AddAttributionText(string text, string? language = null)
        {
            tbc.AddAttributionText(text, language);
            return this;
        }

        public IUwpExtension AddAudio(Uri src, bool? loop = null, bool? silent = null)
        {
            tbc.AddAudio(src, loop, silent);
            return this;
        }

        public IUwpExtension AddButton(string content, ToastActivationType activationType, string arguments, Uri? imageUri = null)
        {
            tbc.AddButton(content, activationType, arguments, imageUri);
            return this;
        }

        public IUwpExtension AddButton(string textBoxId, string content, ToastActivationType activationType, string arguments, Uri? imageUri = null)
        {
            tbc.AddButton(textBoxId, content, activationType, arguments, imageUri);
            return this;
        }

        public IUwpExtension AddButton(IToastButton button)
        {
            tbc.AddButton(button);
            return this;
        }

        public IUwpExtension AddComboBox(string id, string title, string defaultSelectionBoxItemId, (string comboBoxItemId, string comboBoxItemContent)[] choices)
        {
            tbc.AddComboBox(id, title, defaultSelectionBoxItemId, choices);
            return this;
        }

        public IUwpExtension AddComboBox(string id, string defaultSelectionBoxItemId, params (string comboBoxItemId, string comboBoxItemContent)[] choices)
        {
            tbc.AddComboBox(id, defaultSelectionBoxItemId, choices);
            return this;
        }

        public IUwpExtension AddComboBox(string id, params (string comboBoxItemId, string comboBoxItemContent)[] choices)
        {
            tbc.AddComboBox(id, choices);
            return this;
        }

        public IUwpExtension AddComboBox(string id, string title, string defaultSelectionBoxItemId, IEnumerable<(string comboBoxItemId, string comboBoxItemContent)> choices)
        {
            tbc.AddComboBox(id, title, defaultSelectionBoxItemId, choices);
            return this;
        }

        public IUwpExtension AddCustomTimeStamp(DateTime dateTime)
        {
            tbc.AddCustomTimeStamp(dateTime);
            return this;
        }

        public IUwpExtension AddHeader(string id, string title, string arguments)
        {
            tbc.AddHeader(id, title, arguments);
            return this;
        }

        public IUwpExtension AddHeroImage(Uri uri, string? alternateText = null, bool? addImageQuery = null)
        {
            tbc.AddHeroImage(uri, alternateText, addImageQuery);
            return this;
        }

        public IUwpExtension AddInlineImage(Uri uri, string? alternateText = null, bool? addImageQuery = null, AdaptiveImageCrop? hintCrop = null, bool? hintRemoveMargin = null)
        {
            tbc.AddInlineImage(uri, alternateText, addImageQuery, hintCrop, hintRemoveMargin);
            return this;
        }

        public IUwpExtension AddInputTextBox(string id, string? placeHolderContent = null, string? title = null)
        {
            tbc.AddInputTextBox(id, placeHolderContent , title );
            return this;
        }

        public IUwpExtension AddProgressBar(string? title = null, double? value = null, bool isIndeterminate = false, string? valueStringOverride = null, string? status = null)
        {
            tbc.AddProgressBar(title , value , isIndeterminate , valueStringOverride , status );
            return this;
        }

        public IUwpExtension AddText(string text, AdaptiveTextStyle? hintStyle = null, bool? hintWrap = null, int? hintMaxLines = null, int? hintMinLines = null, AdaptiveTextAlign? hintAlign = null, string? language = null)
        {
            tbc.AddText(text, hintStyle, hintWrap, hintMaxLines, hintMinLines, hintAlign, language);
            return this;
        }

        public IUwpExtension AddToastActivationInfo(string launchArgs, ToastActivationType activationType)
        {
            tbc.AddToastActivationInfo(launchArgs, activationType);
            return this;
        }

        public IUwpExtension AddToastInput(IToastInput input)
        {
            tbc.AddToastInput(input);
            return this;
        }

        public IUwpExtension AddVisualChild(IToastBindingGenericChild child)
        {
            tbc.AddVisualChild(child);
            return this;
        }

        public IUwpExtension SetToastDuration(ToastDuration duration)
        {
            tbc.SetToastDuration(duration);
            return this;
        }

        public IUwpExtension SetToastScenario(ToastScenario scenario)
        {
            tbc.SetToastScenario(scenario);
            return this;
        }

        public IUwpExtension Use(IExtensionConfiguration<IUwpExtension> visitor)
        {
            visitor.Configure(this);
            return this;
        }

        #endregion

        #region IPlatformSpecificExtension implementation

        public IPlatformSpecificExtension Use(IExtensionConfiguration<IPlatformSpecificExtension> visitor)
        {
            visitor.Configure(this);
            return this;
        }

        #endregion

        #region IBuilderExtension implementation

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .AddTitle(string title) => AddTitle(title);

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .AddDescription(string description) => AddDescription(description);

        IUwpExtension IBuilderExtension<IUwpExtension>
            .AddTitle(string title) => AddTitle(title);

        IUwpExtension IBuilderExtension<IUwpExtension>
            .AddDescription(string description) => AddDescription(description);

        #endregion
    }
}

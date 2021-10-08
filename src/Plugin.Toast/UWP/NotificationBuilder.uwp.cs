using Microsoft.Toolkit.Uwp.Notifications;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace Plugin.Toast.UWP
{
    sealed class NotificationBuilder : IPlatformSpecificExtension, IUwpExtension, INotificationBuilder, IPlatformNotificationBuilder
    {
        readonly ToastContentBuilder tbc;
        private readonly IServiceProvider? serviceProvider;
        bool buildCompleted;
        string? launchArgs;
        ToastActivationType activationType;

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
            if (string.IsNullOrEmpty(Tag))
                Tag = Guid.NewGuid().ToString();
            if (string.IsNullOrEmpty(launchArgs) == false)
            {
                switch (activationType)
                {
                    case ToastActivationType.Foreground:
                        launchArgs += "&" + TagAndGroupActivationArgs();
                        break;
                    case ToastActivationType.Background:
                    case ToastActivationType.Protocol:
                        break;
                    default:
                        throw new InvalidOperationException("unknown activation type");
                }
            }
            else
            {
                launchArgs = TagAndGroupActivationArgs();
            }
            tbc.AddToastActivationInfo(launchArgs, activationType);
            return new Notification(tbc.Content, this);
        }

        string TagAndGroupActivationArgs()
        {
            var result = HttpUtility.UrlEncode(UwpConstants.KTag) + "=" + HttpUtility.UrlEncode(Tag);
            if (string.IsNullOrEmpty(Group) == false)
                result += "&" + HttpUtility.UrlEncode(UwpConstants.KGroup) + "=" + HttpUtility.UrlEncode(Group);
            return result;
        }

        INotificationBuilder INotificationBuilder.AddTitle(string title) => AddTitle(title);

        INotificationBuilder INotificationBuilder.AddDescription(string description) => AddDescription(description);

        public IBuilder WhenUsing<T>(Action<T> buildAction) where T : IBuilderExtension<T>
        {
            if (this is T t)
                buildAction(t);
            return this;
        }

        public async Task<IBuilder> WhenUsing<T>(Func<T, Task> buildAction) where T : IBuilderExtension<T>
        {
            if (this is T t)
                await buildAction(t);
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
            this.launchArgs = launchArgs;
            this.activationType = activationType;
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

        public IUwpExtension SetTag(string tag)
        {
            Tag = tag;
            return this;
        }

        public IUwpExtension SetGroup(string group)
        {
            Group = group;
            return this;
        }

        public IUwpExtension SetRemoteId(string remoteId)
        {
            RemoteId = remoteId;
            return this;
        }

        public IUwpExtension SetupSnooze(TimeSpan snoozeInterval, uint maximumSnoozeCount)
        {
            SnoozeInterval = snoozeInterval;
            MaximumSnoozeCount = maximumSnoozeCount;
            return this;
        }

        public IUwpExtension SetSuppressPopup(bool suppressPopup)
        {
            SuppressPopup = suppressPopup;
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

        IPlatformSpecificExtension INotificationBuilderExtension<IPlatformSpecificExtension>
            .AddTitle(string title) => AddTitle(title);

        IPlatformSpecificExtension INotificationBuilderExtension<IPlatformSpecificExtension>
            .AddDescription(string description) => AddDescription(description);

        IUwpExtension INotificationBuilderExtension<IUwpExtension>
            .AddTitle(string title) => AddTitle(title);

        IUwpExtension INotificationBuilderExtension<IUwpExtension>
            .AddDescription(string description) => AddDescription(description);

        #endregion

        #region IExtensionPlugin support

        NotificationBuilder Add<T1>(T1 a1)
        {
            this.UsePlugin<IUwpExtension, T1>(serviceProvider, a1);
            this.UsePlugin<IPlatformSpecificExtension, T1>(serviceProvider, a1);
            return this;
        }

        NotificationBuilder Add<T1, T2>(T1 a1, T2 a2)
        {
            this.UsePlugin<IUwpExtension, T1, T2>(serviceProvider, a1, a2);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2>(serviceProvider, a1, a2);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
        {
            this.UsePlugin<IUwpExtension, T1, T2, T3>(serviceProvider, a1, a2, a3);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3>(serviceProvider, a1, a2, a3);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
        {
            this.UsePlugin<IUwpExtension, T1, T2, T3, T4>(serviceProvider, a1, a2, a3, a4);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4>(serviceProvider, a1, a2, a3, a4);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
        {
            this.UsePlugin<IUwpExtension, T1, T2, T3, T4, T5>(serviceProvider, a1, a2, a3, a4, a5);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5>(serviceProvider, a1, a2, a3, a4, a5);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
        {
            this.UsePlugin<IUwpExtension, T1, T2, T3, T4, T5, T6>(serviceProvider, a1, a2, a3, a4, a5, a6);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5, T6>(serviceProvider, a1, a2, a3, a4, a5, a6);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
        {
            this.UsePlugin<IUwpExtension, T1, T2, T3, T4, T5, T6, T7>(serviceProvider, a1, a2, a3, a4, a5, a6, a7);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5, T6, T7>(serviceProvider, a1, a2, a3, a4, a5, a6, a7);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
        {
            this.UsePlugin<IUwpExtension, T1, T2, T3, T4, T5, T6, T7, T8>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5, T6, T7, T8>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
        {
            this.UsePlugin<IUwpExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8, a9);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8, a9);
            return this;
        }

        IBuilder IBuilder.Add<T1>(T1 a1) => Add(a1);
        IBuilder IBuilder.Add<T1, T2>(T1 a1, T2 a2) => Add(a1, a2);
        IBuilder IBuilder.Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3) => Add(a1, a2, a3);
        IBuilder IBuilder.Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => Add(a1, a2, a3, a4);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => Add(a1, a2, a3, a4, a5);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => Add(a1, a2, a3, a4, a5, a6);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => Add(a1, a2, a3, a4, a5, a6, a7);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8, a9);

        IUwpExtension IBuilderExtension<IUwpExtension>
            .Add<T1>(T1 a1) => Add(a1);
        IUwpExtension IBuilderExtension<IUwpExtension>
            .Add<T1, T2>(T1 a1, T2 a2) => Add(a1, a2);
        IUwpExtension IBuilderExtension<IUwpExtension>
            .Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3) => Add(a1, a2, a3);
        IUwpExtension IBuilderExtension<IUwpExtension>
            .Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => Add(a1, a2, a3, a4);
        IUwpExtension IBuilderExtension<IUwpExtension>
            .Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => Add(a1, a2, a3, a4, a5);
        IUwpExtension IBuilderExtension<IUwpExtension>
            .Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => Add(a1, a2, a3, a4, a5, a6);
        IUwpExtension IBuilderExtension<IUwpExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => Add(a1, a2, a3, a4, a5, a6, a7);
        IUwpExtension IBuilderExtension<IUwpExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8);
        IUwpExtension IBuilderExtension<IUwpExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8, a9);

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1>(T1 a1) => Add(a1);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2>(T1 a1, T2 a2) => Add(a1, a2);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3) => Add(a1, a2, a3);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => Add(a1, a2, a3, a4);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => Add(a1, a2, a3, a4, a5);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => Add(a1, a2, a3, a4, a5, a6);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => Add(a1, a2, a3, a4, a5, a6, a7);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8, a9);

        #endregion

        #region INotificationBuilder implementation

        public string? Tag { get; private set; }
        public string? Group { get; private set; }
        public string? RemoteId { get; private set; }
        public TimeSpan? SnoozeInterval { get; private set; }
        public uint MaximumSnoozeCount { get; private set; }
        public bool SuppressPopup { get; private set; }

        #endregion
    }
}

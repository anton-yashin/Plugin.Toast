#nullable enable
using LightMock;
using Microsoft.Toolkit.Uwp.Notifications;
using Plugin.Toast;
using Plugin.Toast.UWP;
using System;
using System.Collections.Generic;

namespace DeviceTests.UWP.Mocks
{
    public sealed class MockUwpExtension : IPlatformSpecificExtension
    {
        private readonly IInvocationContext<IUwpExtension> context;

        public MockUwpExtension(IInvocationContext<IUwpExtension> context)
            => this.context = context;

        public IUwpExtension Add<T1>(T1 a1) => context.Invoke(_ => _.Add(a1));

        public IUwpExtension Add<T1, T2>(T1 a1, T2 a2)
            => context.Invoke(_ => _.Add(a1, a2));

        public IUwpExtension Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
            => context.Invoke(_ => _.Add(a1, a2, a3));

        public IUwpExtension Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4));

        public IUwpExtension Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5));

        public IUwpExtension Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6));

        public IUwpExtension Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6, a7));

        public IUwpExtension Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6, a7, a8));

        public IUwpExtension Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9));

        public IUwpExtension AddAppLogoOverride(Uri uri, ToastGenericAppLogoCrop? hintCrop = null, string? alternateText = null, bool? addImageQuery = null)
            => context.Invoke(_ => _.AddAppLogoOverride(uri, hintCrop, alternateText, addImageQuery));

        public IUwpExtension AddAttributionText(string text, string? language = null)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddAudio(Uri src, bool? loop = null, bool? silent = null)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddButton(string content, ToastActivationType activationType, string arguments, Uri? imageUri = null)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddButton(string textBoxId, string content, ToastActivationType activationType, string arguments, Uri? imageUri = null)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddButton(IToastButton button)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddComboBox(string id, string title, string defaultSelectionBoxItemId, (string comboBoxItemId, string comboBoxItemContent)[] choices)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddComboBox(string id, string defaultSelectionBoxItemId, params (string comboBoxItemId, string comboBoxItemContent)[] choices)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddComboBox(string id, params (string comboBoxItemId, string comboBoxItemContent)[] choices)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddComboBox(string id, string title, string defaultSelectionBoxItemId, IEnumerable<(string comboBoxItemId, string comboBoxItemContent)> choices)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddCustomTimeStamp(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddDescription(string description)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddHeader(string id, string title, string arguments)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddHeroImage(Uri uri, string? alternateText = null, bool? addImageQuery = null)
            => context.Invoke(_ => _.AddHeroImage(uri, alternateText, addImageQuery));

        public IUwpExtension AddInlineImage(Uri uri, string? alternateText = null, bool? addImageQuery = null, AdaptiveImageCrop? hintCrop = null, bool? hintRemoveMargin = null)
            => context.Invoke(_ => _.AddInlineImage(uri, alternateText, addImageQuery, hintCrop, hintRemoveMargin));

        public IUwpExtension AddInputTextBox(string id, string? placeHolderContent = null, string? title = null)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddProgressBar(string? title = null, double? value = null, bool isIndeterminate = false, string? valueStringOverride = null, string? status = null)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddText(string text, AdaptiveTextStyle? hintStyle = null, bool? hintWrap = null, int? hintMaxLines = null, int? hintMinLines = null, AdaptiveTextAlign? hintAlign = null, string? language = null)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddToastActivationInfo(string launchArgs, ToastActivationType activationType)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddToastInput(IToastInput input)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension AddVisualChild(IToastBindingGenericChild child)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension SetGroup(string group)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension SetRemoteId(string remoteId)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension SetSuppressPopup(bool suppressPopup)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension SetTag(string tag)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension SetToastDuration(ToastDuration duration)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension SetToastScenario(ToastScenario scenario)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension SetupSnooze(TimeSpan snoozeInterval, uint maximumSnoozeCount)
        {
            throw new NotImplementedException();
        }

        public IUwpExtension Use(IExtensionConfiguration<IUwpExtension> visitor)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension Use(IExtensionConfiguration<IPlatformSpecificExtension> visitor)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1>(T1 a1)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2>(T1 a1, T2 a2)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.AddDescription(string description)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.AddTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}

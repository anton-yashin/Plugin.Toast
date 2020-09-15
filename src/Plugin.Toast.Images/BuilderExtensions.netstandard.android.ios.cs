using Microsoft.Toolkit.Uwp.Notifications;
using Plugin.Toast.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public static partial class BuilderExtensions
    {
        static IUwpExtension PlatformAddAppLogoOverride(this IUwpExtension extension, ToastImageSource imageSource, ToastGenericAppLogoCrop? hintCrop, string? alternateText, bool? addImageQuery)
            => throw ExceptionUtils.NotSupportedOrImplementedException;
        static IUwpExtension PlatformAddHeroImage(this IUwpExtension extension, ToastImageSource imageSource, string? alternateText, bool? addImageQuery)
            => throw ExceptionUtils.NotSupportedOrImplementedException;
        static IUwpExtension PlatformAddInlineImage(this IUwpExtension extension, ToastImageSource imageSource, string? alternateText, bool? addImageQuery, AdaptiveImageCrop? hintCrop, bool? hintRemoveMargin)
            => throw ExceptionUtils.NotSupportedOrImplementedException;

    }
}

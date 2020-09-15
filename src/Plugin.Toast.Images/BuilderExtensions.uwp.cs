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
            => extension.AddAppLogoOverride(imageSource.ImageUri, hintCrop, alternateText, addImageQuery);

        static IUwpExtension PlatformAddHeroImage(this IUwpExtension extension, ToastImageSource imageSource, string? alternateText, bool? addImageQuery)
            => extension.AddHeroImage(imageSource.ImageUri, alternateText, addImageQuery);
        static IUwpExtension PlatformAddInlineImage(this IUwpExtension extension, ToastImageSource imageSource, string? alternateText, bool? addImageQuery, AdaptiveImageCrop? hintCrop, bool? hintRemoveMargin)
            => extension.AddInlineImage(imageSource.ImageUri, alternateText, addImageQuery, hintCrop, hintRemoveMargin);

    }
}

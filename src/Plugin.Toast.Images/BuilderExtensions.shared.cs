using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public static partial class BuilderExtensions
    {
        public static IBuilder AddImage(this IBuilder builder, ToastImageSource imageSource)
            => builder.Add(imageSource, Router.Route.Default);

        public static T AddImage<T>(this T builder, ToastImageSource imageSource)
            where T : IBuilderExtension<T>
        {
            return builder.Add(imageSource, Router.Route.Default);
        }

        public static T AddLargeIcon<T>(this T extension, ToastImageSource imageSource)
            where T : IBuilderExtension<T>, IDroidNotificationExtension
        {
            return extension.PrivateAddLargeIcon(imageSource);
        }

        static T PrivateAddLargeIcon<T>(this T extension, ToastImageSource imageSource)
            where T : IBuilderExtension<T>
        {
            return extension.Add(imageSource, Router.Route.DroidLargeIcon);
        }

        public static T AddAttachments<T>(this T extension, IEnumerable<ToastImageSource> imageSources)
            where T : IBuilderExtension<T>, IIosNotificationExtension
        {
            return extension.PrivateAddAttachments(imageSources);
        }

        static T PrivateAddAttachments<T>(this T extension, IEnumerable<ToastImageSource> imageSources)
            where T : IBuilderExtension<T>
        {
            return extension.Add(imageSources, Router.Route.IosMultipleAttachments);
        }

        public static T AddAttachment<T>(this T extension, ToastImageSource imageSource)
            where T : IBuilderExtension<T>, IIosNotificationExtension
        {
            return extension.PrivateAddAttachment(imageSource);
        }

        static T PrivateAddAttachment<T>(this T extension, ToastImageSource imageSource)
            where T : IBuilderExtension<T>
        {
            return extension.Add(imageSource, Router.Route.IosSingleAttachment);
        }

        public static IUwpExtension AddAppLogoOverride(this IUwpExtension extension, ToastImageSource imageSource, ToastGenericAppLogoCrop? hintCrop = null, string? alternateText = null, bool? addImageQuery = null)
            => PlatformAddAppLogoOverride(extension, imageSource, hintCrop, alternateText, addImageQuery);

        public static IUwpExtension AddHeroImage(this IUwpExtension extension, ToastImageSource imageSource, string? alternateText = null, bool? addImageQuery = null)
            => PlatformAddHeroImage(extension, imageSource, alternateText, addImageQuery);

        public static IUwpExtension AddInlineImage(this IUwpExtension extension, ToastImageSource imageSource, string? alternateText = null, bool? addImageQuery = null, AdaptiveImageCrop? hintCrop = null, bool? hintRemoveMargin = null)
            => PlatformAddInlineImage(extension, imageSource, alternateText, addImageQuery, hintCrop, hintRemoveMargin);

    }
}

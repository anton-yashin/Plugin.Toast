using Microsoft.Toolkit.Uwp.Notifications;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    /// <summary>
    /// Extension methods <see cref="IBuilder"/>
    /// </summary>
    public static partial class BuilderExtensions
    {
        /// <summary>
        /// Add a image to builder.
        /// </summary>
        /// <remarks>
        /// Image routed to <see cref="Plugin.Toast.Droid.IPlatformSpecificExtension.SetLargeIcon(Android.Graphics.Bitmap)"/>
        /// or <see cref="Plugin.Toast.IOS.IPlatformSpecificExtension.AddAttachment(UserNotifications.UNNotificationAttachment)"/>
        /// or <see cref="IUwpExtension.AddAppLogoOverride(Uri, ToastGenericAppLogoCrop?, string?, bool?)"/>. If there a
        /// <see cref="ISnackbarExtension"/> or <see cref="IIosLocalNotificationExtension"/> then no action will performed.
        /// <seealso cref="IToastImageSourceFactory"/>
        /// </remarks>
        public static IBuilder AddImage(this IBuilder builder, ToastImageSource imageSource)
            => builder.Add(imageSource, Router.Route.Default);

        /// <summary>
        /// Add a image to builder.
        /// </summary>
        /// <remarks>
        /// Image routed to <see cref="Plugin.Toast.Droid.IPlatformSpecificExtension.SetLargeIcon(Android.Graphics.Bitmap)"/>
        /// or <see cref="Plugin.Toast.IOS.IPlatformSpecificExtension.AddAttachment(UserNotifications.UNNotificationAttachment)"/>
        /// or <see cref="IUwpExtension.AddAppLogoOverride(Uri, ToastGenericAppLogoCrop?, string?, bool?)"/>. If there a
        /// <see cref="ISnackbarExtension"/> or <see cref="IIosLocalNotificationExtension"/> then no action will performed.
        /// <seealso cref="IToastImageSourceFactory"/>
        /// </remarks>
        public static T AddImage<T>(this T builder, ToastImageSource imageSource)
            where T : IBuilderExtension<T>
        {
            return builder.Add(imageSource, Router.Route.Default);
        }

        /// <summary>
        /// Add a image to builder using <see cref="Plugin.Toast.Droid.IPlatformSpecificExtension.SetLargeIcon(Android.Graphics.Bitmap)"/>
        /// <seealso cref="IToastImageSourceFactory"/>
        /// </summary>
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

        /// <summary>
        /// Add images to builder using <see cref="Plugin.Toast.IOS.IPlatformSpecificExtension.AddAttachments(IEnumerable{UserNotifications.UNNotificationAttachment})"/>
        /// <seealso cref="IToastImageSourceFactory"/>
        /// </summary>
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

        /// <summary>
        /// Add image to builder using <see cref="Plugin.Toast.IOS.IPlatformSpecificExtension.AddAttachment(UserNotifications.UNNotificationAttachment)"/>
        /// <seealso cref="IToastImageSourceFactory"/>
        /// </summary>
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

        /// <summary>
        /// Override the app logo with custom image of choice that will be displayed on the toast.
        /// <seealso cref="IUwpExtension.AddAppLogoOverride(Uri, ToastGenericAppLogoCrop?, string?, bool?)"/>
        /// <seealso cref="IToastImageSourceFactory"/>
        /// </summary>
        /// <param name="extension">Target extension.</param>
        /// <param name="imageSource">Image to add.</param>
        /// <param name="hintCrop">Specify how the image should be cropped.</param>
        /// <param name="alternateText">A description of the image, for users of assistive technologies.</param>
        /// <param name="addImageQuery">A value whether Windows is allowed to append a query string to the image URI supplied in the Tile notification.</param>
        public static IUwpExtension AddAppLogoOverride(this IUwpExtension extension, ToastImageSource imageSource, ToastGenericAppLogoCrop? hintCrop = null, string? alternateText = null, bool? addImageQuery = null)
            => PlatformAddAppLogoOverride(extension, imageSource, hintCrop, alternateText, addImageQuery);

        /// <summary>
        /// Add a hero image to the toast. <seealso cref="IUwpExtension.AddHeroImage(Uri, string?, bool?)"/>
        /// <seealso cref="IToastImageSourceFactory"/>
        /// </summary>
        /// <param name="extension">Target extension.</param>
        /// <param name="imageSource">Image to add.</param>
        /// <param name="alternateText">A description of the image, for users of assistive technologies.</param>
        /// <param name="addImageQuery">A value whether Windows is allowed to append a query string to the image URI supplied in the Tile notification.</param>
        /// <returns></returns>
        public static IUwpExtension AddHeroImage(this IUwpExtension extension, ToastImageSource imageSource, string? alternateText = null, bool? addImageQuery = null)
            => PlatformAddHeroImage(extension, imageSource, alternateText, addImageQuery);

        /// <summary>
        /// Add an image inline with other toast content. <seealso cref="IUwpExtension.AddInlineImage(Uri, string?, bool?, AdaptiveImageCrop?, bool?)"/>
        /// <seealso cref="IToastImageSourceFactory"/>
        /// </summary>
        /// <param name="extension">Target extension.</param>
        /// <param name="imageSource">Image to add.</param>
        /// <param name="alternateText">A description of the image, for users of assistive technologies.</param>
        /// <param name="addImageQuery">A value whether Windows is allowed to append a query string to the image URI supplied in the Tile notification.</param>
        /// <param name="hintCrop">A value whether a margin is removed. images have an 8px margin around them.</param>
        /// <param name="hintRemoveMargin">the horizontal alignment of the image.This is only supported when inside an <see cref="AdaptiveSubgroup"/>.</param>
        public static IUwpExtension AddInlineImage(this IUwpExtension extension, ToastImageSource imageSource, string? alternateText = null, bool? addImageQuery = null, AdaptiveImageCrop? hintCrop = null, bool? hintRemoveMargin = null)
            => PlatformAddInlineImage(extension, imageSource, alternateText, addImageQuery, hintCrop, hintRemoveMargin);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Toast.IOS;
using UserNotifications;

namespace Plugin.Toast
{
    sealed class IOsImageRouter :
        IExtensionPlugin<IPlatformSpecificExtension, ToastImageSource, Router.Route>,
        IExtensionPlugin<IPlatformSpecificExtension, IEnumerable<ToastImageSource>, Router.Route>
    {
        public IOsImageRouter()
        {
        }

        public void Configure(IPlatformSpecificExtension extension, ToastImageSource imageSource, Router.Route route)
        {
            switch (route)
            {
                case Router.Route.Default:
                case Router.Route.IosSingleAttachment:
                    extension.AddAttachment(imageSource.Attachment);
                    break;
                default:
                    throw Router.Exception;
            }
        }

        public void Configure(IPlatformSpecificExtension extension, IEnumerable<ToastImageSource> imageSources, Router.Route route)
        {
            switch (route)
            {
                case Router.Route.IosMultipleAttachments:
                    extension.AddAttachments(imageSources.Select(i => i.Attachment));
                    break;
                default:
                    throw Router.Exception;
            }
        }
    }
}
    
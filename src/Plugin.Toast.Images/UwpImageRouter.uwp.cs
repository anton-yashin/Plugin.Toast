using Plugin.Toast.UWP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    sealed class UwpImageRouter : IExtensionPlugin<IPlatformSpecificExtension, ToastImageSource, Router.Route>
    {
        public void Configure(IPlatformSpecificExtension extension, ToastImageSource imageSource, Router.Route route)
        {
            if (route != Router.Route.Default)
                throw Router.Exception;
            extension.AddAppLogoOverride(imageSource.ImageUri);
        }
    }
}

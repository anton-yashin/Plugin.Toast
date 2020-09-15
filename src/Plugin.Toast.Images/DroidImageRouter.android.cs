using Plugin.Toast.Droid;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    sealed class DroidImageRouter : IExtensionPlugin<IPlatformSpecificExtension, ToastImageSource, Router.Route>
    {
        public void Configure(IPlatformSpecificExtension extension, ToastImageSource imageSource, Router.Route route)
        {
            switch (route)
            {
                case Router.Route.Default:
                case Router.Route.DroidLargeIcon:
                    extension.SetLargeIcon(imageSource.Bitmap);
                    break;
                default:
                    throw Router.Exception;
            }
        }
    }
}

using System;

namespace Plugin.Toast
{
    static class Router
    {
        internal enum Route
        {
            Default,
            IosMultipleAttachments,
            IosSingleAttachment,
            DroidLargeIcon,
        }

        internal static Exception Exception => new InvalidOperationException("data forwarding error");
    }
}

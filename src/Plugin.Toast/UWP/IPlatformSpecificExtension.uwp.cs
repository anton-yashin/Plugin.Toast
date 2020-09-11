using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast.UWP
{
    public interface IPlatformSpecificExtension : IUwpExtension, IBuilderExtension<IPlatformSpecificExtension>
    {
    }
}

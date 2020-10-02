using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public partial class ToastId
    {
        int GetPlatformPersistentHashCode() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
    }
}

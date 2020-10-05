using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Toast
{
    public partial class ToastId
    {
        int GetPlatformPersistentHashCode() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        bool PlatformEquals(ToastId? other) => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        bool PlatformEquals(object? obj) => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        private int PlatformGetHashCode() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
        private string PlatformToString() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;
    }
}

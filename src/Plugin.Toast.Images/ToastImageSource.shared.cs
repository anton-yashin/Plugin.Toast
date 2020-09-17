using Plugin.Toast.Exceptions;
using System;

namespace Plugin.Toast
{
    public abstract partial class ToastImageSource
    {
        private ToastImageSource()
            => throw ExceptionUtils.NotSupportedOrImplementedException;
    }
}

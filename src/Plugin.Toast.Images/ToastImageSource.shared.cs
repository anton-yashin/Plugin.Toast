using Plugin.Toast.Exceptions;
using System;

namespace Plugin.Toast
{
    /// <summary>
    /// Abstract class whose implementors wraps platform dependent images
    /// </summary>
    public abstract partial class ToastImageSource
    {
#nullable disable
        /// <summary>
        /// This constructor allow you to create mocks using Moq.
        /// </summary>
        [Obsolete("for Moq only", true)]
        protected ToastImageSource() { }
#nullable restore
    }
}

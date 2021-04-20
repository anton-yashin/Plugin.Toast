using System;

namespace Plugin.Toast
{
    public abstract partial class ToastImageSource
    {
        internal Uri ImageUri { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToastImageSource"/> class.
        /// </summary>
        protected internal ToastImageSource(Uri image) => this.ImageUri = image;
    }

    sealed class SealedToastImageSource : ToastImageSource
    {
        public SealedToastImageSource(Uri image) : base(image)
        {
        }
    }
}

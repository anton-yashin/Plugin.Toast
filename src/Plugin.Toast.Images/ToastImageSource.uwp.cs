using System;

namespace Plugin.Toast
{
    public abstract partial class ToastImageSource
    {
        internal Uri ImageUri { get; }

        protected internal ToastImageSource(Uri image) => this.ImageUri = image;
    }

    sealed class SealedToastImageSource : ToastImageSource
    {
        public SealedToastImageSource(Uri image) : base(image)
        {
        }
    }
}

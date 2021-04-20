using Android.Graphics;
using System;

namespace Plugin.Toast
{
    public abstract partial class ToastImageSource
    {
        internal Bitmap Bitmap { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToastImageSource"/> class.
        /// </summary>
        protected internal ToastImageSource(Bitmap bitmap) => this.Bitmap = bitmap;
    }

    sealed class SealedToastImageSource : ToastImageSource
    {
        public SealedToastImageSource(Bitmap bitmap) : base(bitmap)
        {
        }
    }

}

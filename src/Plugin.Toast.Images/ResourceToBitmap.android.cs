using Android.Graphics;
using System;
using System.Threading.Tasks;

namespace Plugin.Toast.Images
{
    sealed class ResourceToBitmap : IResourceToBitmap
    {
        private readonly Func<string, Task<Bitmap>> converter;

        public ResourceToBitmap(Func<string, Task<Bitmap>> converter)
            => this.converter = converter;

        public Task<Bitmap> GetBitmapAsync(string name)
            => converter(name);
    }

    interface IResourceToBitmap
    {
        Task<Bitmap> GetBitmapAsync(string name);
    }
}

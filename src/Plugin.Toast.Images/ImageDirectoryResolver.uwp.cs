using System;

namespace Plugin.Toast.Images
{
    sealed class ImageDirectoryResolver : IImageDirectoryResolver
    {
        private readonly Func<string?> getImageDirectory;

        public ImageDirectoryResolver(Func<string?> getImageDirectory)
            => this.getImageDirectory = getImageDirectory;

        public string? GetImageDirectory()
            => getImageDirectory();
    }

    interface IImageDirectoryResolver
    {
        string? GetImageDirectory();
    }
}

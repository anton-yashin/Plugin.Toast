using AndroidX.Core.App;
using AndroidX.Core.Graphics.Drawable;
using System;

namespace Plugin.Toast.Images
{
    sealed class DroidPersonBuilder : IDroidPersonBuilder
    {
        private readonly Person.Builder builder;

        public DroidPersonBuilder() => builder = new Person.Builder();

        public Person Build()
        {
            return builder.Build();
        }

        public IDroidPersonBuilder SetBot(bool bot)
        {
            builder.SetBot(bot);
            return this;
            throw new NotImplementedException();
        }

        public IDroidPersonBuilder SetIcon(ToastImageSource icon)
        {
            builder.SetIcon(IconCompat.CreateWithBitmap(icon.Bitmap));
            return this;
        }

        public IDroidPersonBuilder SetImportant(bool important)
        {
            builder.SetImportant(important);
            return this;
        }

        public IDroidPersonBuilder SetKey(string key)
        {
            builder.SetKey(key);
            return this;
        }

        public IDroidPersonBuilder SetName(string name)
        {
            builder.SetName(name);
            return this;
        }

        public IDroidPersonBuilder SetUri(string uri)
        {
            builder.SetUri(uri);
            return this;
        }
    }
}

using Android.Support.Design.Widget;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Plugin.Toast.Droid
{
    sealed class SnackbarBuilder : IBuilder, ISnackbarExtension, ISnackbarBuilder
    {
        private readonly IToastOptions options;
        private readonly IServiceProvider? serviceProvider;
        string? title;
        string? description;
        private bool buildCompleted;

        public int SnackbarDuration { get; private set; } = Snackbar.LengthShort;
        public int? ActionTextColor { get; private set; }
        public string? ActionText { get; private set; }
        public string Text
        {
            get
            {
                if (title != null && description != null)
                    return title + "\n" + description;
                if (title != null)
                    return title;
                if (description != null)
                    return description;
                return "";
            }
        }

        public SnackbarBuilder(IToastOptions options, IServiceProvider? serviceProvider)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.serviceProvider = serviceProvider;
            this.UseConfigurationFrom<ISnackbarExtension>(serviceProvider);
        }

        SnackbarBuilder AddTitle(string title)
        {
            this.title = title;
            return this;
        }

        SnackbarBuilder AddDescription(string description)
        {
            this.description = description;
            return this;
        }

        public INotification Build()
        {
            if (buildCompleted == true)
                throw Exceptions.ExceptionUtils.BuildTwice;
            buildCompleted = true;
            return new SnackbarNotification(options: options, snackbarBuilder: this);
        }

        public IBuilder WhenUsing<T>(Action<T> buildAction) where T : IBuilderExtension<T>
        {
            if (this is T tb)
                buildAction(tb);
            return this;
        }

        IBuilder IBuilder.AddTitle(string title)
            => AddTitle(title);

        IBuilder IBuilder.AddDescription(string description)
            => AddDescription(description);

        public IBuilder UseConfiguration<T>(T token)
        {
            this.UseConfigurationFrom<ISnackbarExtension, T>(serviceProvider, token);
            return this;
        }

        public ISnackbarExtension WithAction(string actionText)
        {
            this.ActionText = actionText;
            return this;
        }

        public ISnackbarExtension WithAction(string actionText, int colorResource)
        {
            ActionTextColor = colorResource;
            return WithAction(actionText);
        }

        public ISnackbarExtension WithDuration(int duration)
        {
            this.SnackbarDuration = duration;
            return this;
        }

        public ISnackbarExtension WithDuration(SnackbarDuration duration) => WithDuration((int)duration);

        public ISnackbarExtension Use(IExtensionConfiguration<ISnackbarExtension> visitor)
        {
            visitor.Configure(this);
            return this;
        }

        ISnackbarExtension IBuilderExtension<ISnackbarExtension>.AddTitle(string title)
            => AddTitle(title);

        ISnackbarExtension IBuilderExtension<ISnackbarExtension>.AddDescription(string description)
            => AddDescription(description);
    }
}

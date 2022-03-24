using Google.Android.Material.Snackbar;
using Plugin.Toast.Abstractions;
using Plugin.Toast.Droid.Configuration;
using System;
using System.Threading.Tasks;

namespace Plugin.Toast.Droid
{
    sealed class SnackbarBuilder : INotificationBuilder, ISnackbarExtension, ISnackbarBuilder
    {
        private readonly IActivityConfiguration activityConfiguration;
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

        public SnackbarBuilder(IActivityConfiguration activityConfiguration, IServiceProvider? serviceProvider)
        {
            this.activityConfiguration = activityConfiguration;
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
            return new SnackbarNotification(activityConfiguration: activityConfiguration, snackbarBuilder: this);
        }

        public IBuilder WhenUsing<T>(Action<T> buildAction) where T : IBuilderExtension<T>
        {
            if (this is T tb)
                buildAction(tb);
            return this;
        }

        public async Task<IBuilder> WhenUsingAsync<T>(Func<T, Task> buildAction) where T : IBuilderExtension<T>
        {
            if (this is T tb)
                await buildAction(tb);
            return this;
        }

        INotificationBuilder INotificationBuilder.AddTitle(string title)
            => AddTitle(title);

        INotificationBuilder INotificationBuilder.AddDescription(string description)
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

        public ISnackbarExtension WithDuration(TimeSpan duration)
        {
            this.SnackbarDuration = checked((int)(duration.Ticks / TimeSpan.TicksPerMillisecond));
            return this;
        }

        public ISnackbarExtension WithDuration(SnackbarDuration duration) => WithDuration((int)duration);

        public ISnackbarExtension Use(IExtensionConfiguration<ISnackbarExtension> visitor)
        {
            visitor.Configure(this);
            return this;
        }

        ISnackbarExtension INotificationBuilderExtension<ISnackbarExtension>.AddTitle(string title)
            => AddTitle(title);

        ISnackbarExtension INotificationBuilderExtension<ISnackbarExtension>.AddDescription(string description)
            => AddDescription(description);

        #region IExtensionPlugin support

        SnackbarBuilder Add<T1>(T1 a1)
        {
            this.UsePlugin<ISnackbarExtension, T1>(serviceProvider, a1);
            return this;
        }

        SnackbarBuilder Add<T1, T2>(T1 a1, T2 a2)
        {
            this.UsePlugin<ISnackbarExtension, T1, T2>(serviceProvider, a1, a2);
            return this;
        }

        SnackbarBuilder Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
        {
            this.UsePlugin<ISnackbarExtension, T1, T2, T3>(serviceProvider, a1, a2, a3);
            return this;
        }

        SnackbarBuilder Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
        {
            this.UsePlugin<ISnackbarExtension, T1, T2, T3, T4>(serviceProvider, a1, a2, a3, a4);
            return this;
        }

        SnackbarBuilder Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
        {
            this.UsePlugin<ISnackbarExtension, T1, T2, T3, T4, T5>(serviceProvider, a1, a2, a3, a4, a5);
            return this;
        }

        SnackbarBuilder Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
        {
            this.UsePlugin<ISnackbarExtension, T1, T2, T3, T4, T5, T6>(serviceProvider, a1, a2, a3, a4, a5, a6);
            return this;
        }

        SnackbarBuilder Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
        {
            this.UsePlugin<ISnackbarExtension, T1, T2, T3, T4, T5, T6, T7>(serviceProvider, a1, a2, a3, a4, a5, a6, a7);
            return this;
        }

        SnackbarBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
        {
            this.UsePlugin<ISnackbarExtension, T1, T2, T3, T4, T5, T6, T7, T8>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8);
            return this;
        }

        SnackbarBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
        {
            this.UsePlugin<ISnackbarExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8, a9);
            return this;
        }

        IBuilder IBuilder.Add<T1>(T1 a1) => Add(a1);
        IBuilder IBuilder.Add<T1, T2>(T1 a1, T2 a2) => Add(a1, a2);
        IBuilder IBuilder.Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3) => Add(a1, a2, a3);
        IBuilder IBuilder.Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => Add(a1, a2, a3, a4);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => Add(a1, a2, a3, a4, a5);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => Add(a1, a2, a3, a4, a5, a6);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => Add(a1, a2, a3, a4, a5, a6, a7);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8);
        IBuilder IBuilder.Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8, a9);

        ISnackbarExtension IBuilderExtension<ISnackbarExtension>
            .Add<T1>(T1 a1) => Add(a1);
        ISnackbarExtension IBuilderExtension<ISnackbarExtension>
            .Add<T1, T2>(T1 a1, T2 a2) => Add(a1, a2);
        ISnackbarExtension IBuilderExtension<ISnackbarExtension>
            .Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3) => Add(a1, a2, a3);
        ISnackbarExtension IBuilderExtension<ISnackbarExtension>
            .Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => Add(a1, a2, a3, a4);
        ISnackbarExtension IBuilderExtension<ISnackbarExtension>
            .Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => Add(a1, a2, a3, a4, a5);
        ISnackbarExtension IBuilderExtension<ISnackbarExtension>
            .Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => Add(a1, a2, a3, a4, a5, a6);
        ISnackbarExtension IBuilderExtension<ISnackbarExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => Add(a1, a2, a3, a4, a5, a6, a7);
        ISnackbarExtension IBuilderExtension<ISnackbarExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8);
        ISnackbarExtension IBuilderExtension<ISnackbarExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8, a9);

        #endregion

    }
}

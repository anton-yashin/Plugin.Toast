using Foundation;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace Plugin.Toast.IOS
{
    sealed class LocalNotificationBuilder : INotificationBuilder, ILocalNotificationBuilder, IIosLocalNotificationExtension
    {
        NSMutableDictionary? customArgs;
        bool build;
        private readonly IServiceProvider? serviceProvider;

        public LocalNotificationBuilder(IServiceProvider? serviceProvider)
        {
            Notification = new UILocalNotification();
            this.UseConfigurationFrom<IIosLocalNotificationExtension>(serviceProvider);
            this.serviceProvider = serviceProvider;
        }

        LocalNotificationBuilder AddTitle(string title)
        {
            Notification.AlertTitle = title;
            return this;
        }

        LocalNotificationBuilder AddDescription(string description)
        {
            Notification.AlertBody = description;
            return this;
        }

        public UILocalNotification Notification { get; private set; }

        #region IBuilder implementation

        public INotification Build()
        {
            if (build == true)
                throw Exceptions.ExceptionUtils.BuildTwice;
            build = true;
            return new LocalNotification(this);
        }

        INotificationBuilder INotificationBuilder.AddTitle(string title) => AddTitle(title);

        INotificationBuilder INotificationBuilder.AddDescription(string description) => AddDescription(description);

        public IBuilder WhenUsing<T>(Action<T> buildAction) where T : IBuilderExtension<T>
        {
            if (this is T t)
                buildAction(t);
            return this;
        }

        public async Task<IBuilder> WhenUsing<T>(Func<T, Task> buildAction) where T : IBuilderExtension<T>
        {
            if (this is T t)
                await buildAction(t);
            return this;
        }


        public IBuilder UseConfiguration<T>(T token)
        {
            this.UseConfigurationFrom<IIosLocalNotificationExtension, T>(serviceProvider, token);
            return this;
        }

        #endregion

        #region IIosLocalNotificationExtension implementation

        public IIosLocalNotificationExtension AddSoundName(string soundName)
        {
            Notification.SoundName = soundName;
            return this;
        }

        public IIosLocalNotificationExtension WithCustomArg(string key, string value)
        {
            if (customArgs == null)
                Notification.UserInfo = customArgs = new NSMutableDictionary();
            customArgs[key] = NSObject.FromObject(value);
            return this;
        }

        public IIosLocalNotificationExtension WithCustomArgs(IEnumerable<(string key, string value)> args)
        {
            if (customArgs == null)
                Notification.UserInfo = customArgs = new NSMutableDictionary();
            foreach (var (key, value) in args)
                customArgs[key] = NSObject.FromObject(value);
            return this;
        }

        public IIosLocalNotificationExtension Use(IExtensionConfiguration<IIosLocalNotificationExtension> visitor)
        {
            visitor.Configure(this);
            return this;
        }

        #endregion

        #region IBuilderExtension implementation

        IIosLocalNotificationExtension INotificationBuilderExtension<IIosLocalNotificationExtension>
            .AddTitle(string title) => AddTitle(title);

        IIosLocalNotificationExtension INotificationBuilderExtension<IIosLocalNotificationExtension>
            .AddDescription(string description) => AddDescription(description);

        #endregion

        #region IExtensionPlugin support

        LocalNotificationBuilder Add<T1>(T1 a1)
        {
            this.UsePlugin<IIosLocalNotificationExtension, T1>(serviceProvider, a1);
            return this;
        }

        LocalNotificationBuilder Add<T1, T2>(T1 a1, T2 a2)
        {
            this.UsePlugin<IIosLocalNotificationExtension, T1, T2>(serviceProvider, a1, a2);
            return this;
        }

        LocalNotificationBuilder Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
        {
            this.UsePlugin<IIosLocalNotificationExtension, T1, T2, T3>(serviceProvider, a1, a2, a3);
            return this;
        }

        LocalNotificationBuilder Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
        {
            this.UsePlugin<IIosLocalNotificationExtension, T1, T2, T3, T4>(serviceProvider, a1, a2, a3, a4);
            return this;
        }

        LocalNotificationBuilder Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
        {
            this.UsePlugin<IIosLocalNotificationExtension, T1, T2, T3, T4, T5>(serviceProvider, a1, a2, a3, a4, a5);
            return this;
        }

        LocalNotificationBuilder Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
        {
            this.UsePlugin<IIosLocalNotificationExtension, T1, T2, T3, T4, T5, T6>(serviceProvider, a1, a2, a3, a4, a5, a6);
            return this;
        }

        LocalNotificationBuilder Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
        {
            this.UsePlugin<IIosLocalNotificationExtension, T1, T2, T3, T4, T5, T6, T7>(serviceProvider, a1, a2, a3, a4, a5, a6, a7);
            return this;
        }

        LocalNotificationBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
        {
            this.UsePlugin<IIosLocalNotificationExtension, T1, T2, T3, T4, T5, T6, T7, T8>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8);
            return this;
        }

        LocalNotificationBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
        {
            this.UsePlugin<IIosLocalNotificationExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8, a9);
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

        IIosLocalNotificationExtension IBuilderExtension<IIosLocalNotificationExtension>
            .Add<T1>(T1 a1) => Add(a1);
        IIosLocalNotificationExtension IBuilderExtension<IIosLocalNotificationExtension>
            .Add<T1, T2>(T1 a1, T2 a2) => Add(a1, a2);
        IIosLocalNotificationExtension IBuilderExtension<IIosLocalNotificationExtension>
            .Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3) => Add(a1, a2, a3);
        IIosLocalNotificationExtension IBuilderExtension<IIosLocalNotificationExtension>
            .Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => Add(a1, a2, a3, a4);
        IIosLocalNotificationExtension IBuilderExtension<IIosLocalNotificationExtension>
            .Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => Add(a1, a2, a3, a4, a5);
        IIosLocalNotificationExtension IBuilderExtension<IIosLocalNotificationExtension>
            .Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => Add(a1, a2, a3, a4, a5, a6);
        IIosLocalNotificationExtension IBuilderExtension<IIosLocalNotificationExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => Add(a1, a2, a3, a4, a5, a6, a7);
        IIosLocalNotificationExtension IBuilderExtension<IIosLocalNotificationExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8);
        IIosLocalNotificationExtension IBuilderExtension<IIosLocalNotificationExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8, a9);

        #endregion

    }
}

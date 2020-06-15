using Foundation;
using System;
using System.Collections.Generic;
using UIKit;

namespace Plugin.Toast.IOS
{
    sealed class LocalNotificationBuilder : IBuilder, ILocalNotificationBuilder, IIosLocalNotificationExtension
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

        IBuilder IBuilder.AddTitle(string title) => AddTitle(title);

        IBuilder IBuilder.AddDescription(string description) => AddDescription(description);

        public IBuilder WhenUsing<T>(Action<T> buildAction) where T : IBuilderExtension<T>
        {
            if (this is T t)
                buildAction(t);
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

        IIosLocalNotificationExtension IBuilderExtension<IIosLocalNotificationExtension>
            .AddTitle(string title) => AddTitle(title);

        IIosLocalNotificationExtension IBuilderExtension<IIosLocalNotificationExtension>
            .AddDescription(string description) => AddDescription(description);

        #endregion
    }
}

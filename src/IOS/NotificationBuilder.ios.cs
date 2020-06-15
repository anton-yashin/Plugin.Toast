using Foundation;
using System;
using System.Collections.Generic;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    sealed class NotificationBuilder : IBuilder, INotificationBuilder, IIosNotificationExtension, IPlatformSpecificExtension
    {
        NSMutableDictionary? customArgs;
        bool build;
        private readonly IToastOptions options;
        private readonly INotificationReceiver notificationReceiver;
        private readonly IPermission permission;
        private readonly IServiceProvider? serviceProvider;

        public NotificationBuilder(IToastOptions options, INotificationReceiver notificationReceiver, IPermission permission, IServiceProvider? serviceProvider)
        {
            this.options = options;
            this.notificationReceiver = notificationReceiver;
            this.permission = permission;
            this.serviceProvider = serviceProvider;
            this.Notification = new UNMutableNotificationContent();
            if (options.Sound != null)
                Notification.Sound = options.Sound;

            this.UseConfigurationFrom<IIosNotificationExtension>(serviceProvider);
            this.UseConfigurationFrom<IPlatformSpecificExtension>(serviceProvider);
        }

        NotificationBuilder AddDescription(string description)
        {
            Notification.Body = description;
            return this;
        }

        NotificationBuilder AddTitle(string title)
        {
            Notification.Title = title;
            return this;
        }

        public UNMutableNotificationContent Notification { get; }

        #region IBuilder implementation

        IBuilder IBuilder.AddDescription(string description) => AddDescription(description);

        IBuilder IBuilder.AddTitle(string title) => AddTitle(title);

        public INotification Build()
        {
            if (build == true)
                throw Exceptions.ExceptionUtils.BuildTwice;
            build = true;
            return new Notification(this, notificationReceiver, permission);
        }

        public IBuilder WhenUsing<T>(Action<T> buildAction) where T : IBuilderExtension<T>
        {
            if (this is T t)
                buildAction(t);
            return this;
        }

        public IBuilder UseConfiguration<T>(T token)
        {
            this.UseConfigurationFrom<IIosNotificationExtension, T>(serviceProvider, token);
            this.UseConfigurationFrom<IPlatformSpecificExtension, T>(serviceProvider, token);
            return this;
        }

        #endregion

        #region IIosNotificationExtentsion implementation

        public IIosNotificationExtension AddBadgeNumber(int number)
        {
            Notification.Badge = number;
            return this;
        }

        public IIosNotificationExtension AddThreadIdentifier(string threadIdentifier)
        {
            Notification.ThreadIdentifier = threadIdentifier;
            return this;
        }

        public IIosNotificationExtension AddTargetContentIdentifier(string targetContentIdentifier)
        {
            Notification.TargetContentIdentifier = targetContentIdentifier;
            return this;
        }

        public IIosNotificationExtension AddSummaryArgumentCount(ulong SummaryArgumentCount)
        {
            Notification.SummaryArgumentCount = (nuint)SummaryArgumentCount;
            return this;
        }

        public IIosNotificationExtension AddSummaryArgument(string summaryArgument)
        {
            Notification.SummaryArgument = summaryArgument;
            return this;
        }

        public IIosNotificationExtension AddSubtitle(string subtitle)
        {
            Notification.Subtitle = subtitle;
            return this;
        }

        public IIosNotificationExtension AddCategoryIdentifier(string categoryIdentifier)
        {
            Notification.CategoryIdentifier = categoryIdentifier;
            return this;
        }

        public IIosNotificationExtension AddBody(string body)
        {
            Notification.Body = body;
            return this;
        }

        public IIosNotificationExtension AddLaunchImageName(string launchImageName)
        {
            Notification.LaunchImageName = launchImageName;
            return this;
        }

        public IIosNotificationExtension WithCustomArg(string key, string value)
        {
            if (customArgs == null)
                Notification.UserInfo = customArgs = new NSMutableDictionary();
            customArgs[key] = NSObject.FromObject(value);
            return this;
        }

        public IIosNotificationExtension WithCustomArgs(IEnumerable<(string key, string value)> args)
        {
            if (customArgs == null)
                Notification.UserInfo = customArgs = new NSMutableDictionary();
            foreach (var (key, value) in args)
                customArgs[key] = NSObject.FromObject(value);
            return this;
        }

        public IIosNotificationExtension Use(IExtensionConfiguration<IIosNotificationExtension> visitor)
        {
            visitor.Configure(this);
            return this;
        }

        #endregion

        #region IPlatformSpecificExtension implementation

        public IPlatformSpecificExtension AddSound(UNNotificationSound sound)
        {
            Notification.Sound = sound;
            return this;
        }

        public IPlatformSpecificExtension AddBadgeNumber(NSNumber number)
        {
            Notification.Badge = number;
            return this;
        }

        public IPlatformSpecificExtension AddSummaryArgumentCount(nuint SummaryArgumentCount)
        {
            Notification.SummaryArgumentCount = SummaryArgumentCount;
            return this;
        }

        public IPlatformSpecificExtension AddAttachments(UNNotificationAttachment[] attachments)
        {
            Notification.Attachments = attachments;
            return this;
        }

        public IPlatformSpecificExtension Use(IExtensionConfiguration<IPlatformSpecificExtension> visitor)
        {
            visitor.Configure(this);
            return this;
        }

        #endregion

        #region IBuilderExtension implementation

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .AddTitle(string title) => AddTitle(title);

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .AddDescription(string description) => AddDescription(description);

        IIosNotificationExtension IBuilderExtension<IIosNotificationExtension>
            .AddTitle(string title) => AddTitle(title);

        IIosNotificationExtension IBuilderExtension<IIosNotificationExtension>
            .AddDescription(string description) => AddDescription(description);

        #endregion

    }
}

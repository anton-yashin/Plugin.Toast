using Foundation;
using Plugin.Toast.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UserNotifications;

namespace Plugin.Toast.IOS
{
    sealed class NotificationBuilder : INotificationBuilder, IPlatformNotificationBuilder, IIosNotificationExtension, IPlatformSpecificExtension
    {
        NSMutableDictionary? customArgs;
        bool build;
        private readonly IToastOptions options;
        private readonly INotificationReceiver notificationReceiver;
        private readonly IPermission permission;
        private readonly IServiceProvider? serviceProvider;
        private readonly List<UNNotificationAttachment> attachments;

        public NotificationBuilder(IToastOptions options, INotificationReceiver notificationReceiver, IPermission permission, IServiceProvider? serviceProvider)
        {
            this.options = options;
            this.notificationReceiver = notificationReceiver;
            this.permission = permission;
            this.serviceProvider = serviceProvider;
            this.Notification = new UNMutableNotificationContent();
            this.attachments = new List<UNNotificationAttachment>();
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

        INotificationBuilder INotificationBuilder.AddDescription(string description) => AddDescription(description);

        INotificationBuilder INotificationBuilder.AddTitle(string title) => AddTitle(title);

        public INotification Build()
        {
            if (build == true)
                throw Exceptions.ExceptionUtils.BuildTwice;
            build = true;
            this.Notification.Attachments = attachments.ToArray();
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

        public IPlatformSpecificExtension AddAttachments(IEnumerable<UNNotificationAttachment> attachments)
        {
            this.attachments.AddRange(attachments);
            return this;
        }

        [Obsolete("Use IPlatformSpecificExtension AddAttachments(IEnumerable<UNNotificationAttachment> attachments);", true)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public IPlatformSpecificExtension AddAttachments(UNNotificationAttachment[] attachments)
        {
            this.attachments.AddRange(attachments);
            return this;
        }

        public IPlatformSpecificExtension AddAttachment(UNNotificationAttachment attachment)
        {
            this.attachments.Add(attachment);
            return this;
        }

        public IPlatformSpecificExtension Use(IExtensionConfiguration<IPlatformSpecificExtension> visitor)
        {
            visitor.Configure(this);
            return this;
        }

        #endregion

        #region IBuilderExtension implementation

        IPlatformSpecificExtension INotificationBuilderExtension<IPlatformSpecificExtension>
            .AddTitle(string title) => AddTitle(title);

        IPlatformSpecificExtension INotificationBuilderExtension<IPlatformSpecificExtension>
            .AddDescription(string description) => AddDescription(description);

        IIosNotificationExtension INotificationBuilderExtension<IIosNotificationExtension>
            .AddTitle(string title) => AddTitle(title);

        IIosNotificationExtension INotificationBuilderExtension<IIosNotificationExtension>
            .AddDescription(string description) => AddDescription(description);

        #endregion

        #region IExtensionPlugin support

        NotificationBuilder Add<T1>(T1 a1)
        {
            this.UsePlugin<IIosNotificationExtension, T1>(serviceProvider, a1);
            this.UsePlugin<IPlatformSpecificExtension, T1>(serviceProvider, a1);
            return this;
        }

        NotificationBuilder Add<T1, T2>(T1 a1, T2 a2)
        {
            this.UsePlugin<IIosNotificationExtension, T1, T2>(serviceProvider, a1, a2);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2>(serviceProvider, a1, a2);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
        {
            this.UsePlugin<IIosNotificationExtension, T1, T2, T3>(serviceProvider, a1, a2, a3);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3>(serviceProvider, a1, a2, a3);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
        {
            this.UsePlugin<IIosNotificationExtension, T1, T2, T3, T4>(serviceProvider, a1, a2, a3, a4);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4>(serviceProvider, a1, a2, a3, a4);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
        {
            this.UsePlugin<IIosNotificationExtension, T1, T2, T3, T4, T5>(serviceProvider, a1, a2, a3, a4, a5);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5>(serviceProvider, a1, a2, a3, a4, a5);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
        {
            this.UsePlugin<IIosNotificationExtension, T1, T2, T3, T4, T5, T6>(serviceProvider, a1, a2, a3, a4, a5, a6);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5, T6>(serviceProvider, a1, a2, a3, a4, a5, a6);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
        {
            this.UsePlugin<IIosNotificationExtension, T1, T2, T3, T4, T5, T6, T7>(serviceProvider, a1, a2, a3, a4, a5, a6, a7);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5, T6, T7>(serviceProvider, a1, a2, a3, a4, a5, a6, a7);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
        {
            this.UsePlugin<IIosNotificationExtension, T1, T2, T3, T4, T5, T6, T7, T8>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5, T6, T7, T8>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
        {
            this.UsePlugin<IIosNotificationExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8, a9);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8, a9);
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

        IIosNotificationExtension IBuilderExtension<IIosNotificationExtension>
            .Add<T1>(T1 a1) => Add(a1);
        IIosNotificationExtension IBuilderExtension<IIosNotificationExtension>
            .Add<T1, T2>(T1 a1, T2 a2) => Add(a1, a2);
        IIosNotificationExtension IBuilderExtension<IIosNotificationExtension>
            .Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3) => Add(a1, a2, a3);
        IIosNotificationExtension IBuilderExtension<IIosNotificationExtension>
            .Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => Add(a1, a2, a3, a4);
        IIosNotificationExtension IBuilderExtension<IIosNotificationExtension>
            .Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => Add(a1, a2, a3, a4, a5);
        IIosNotificationExtension IBuilderExtension<IIosNotificationExtension>
            .Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => Add(a1, a2, a3, a4, a5, a6);
        IIosNotificationExtension IBuilderExtension<IIosNotificationExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => Add(a1, a2, a3, a4, a5, a6, a7);
        IIosNotificationExtension IBuilderExtension<IIosNotificationExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8);
        IIosNotificationExtension IBuilderExtension<IIosNotificationExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8, a9);

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1>(T1 a1) => Add(a1);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2>(T1 a1, T2 a2) => Add(a1, a2);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3) => Add(a1, a2, a3);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => Add(a1, a2, a3, a4);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => Add(a1, a2, a3, a4, a5);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => Add(a1, a2, a3, a4, a5, a6);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => Add(a1, a2, a3, a4, a5, a6, a7);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8);
        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8, a9);

        #endregion
    }
}

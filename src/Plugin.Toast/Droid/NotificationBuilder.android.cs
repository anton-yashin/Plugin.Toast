using Android.App;
using Android.Support.V4.App;
using System;
using System.Collections.Generic;
using Java.Lang;
using Android.Widget;
using Android.OS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Android.Content;
using Plugin.Toast.Abstractions;

namespace Plugin.Toast.Droid
{
    sealed class NotificationBuilder : IPlatformSpecificExtension, IDroidNotificationExtension, IBuilder, IPlatformNotificationBuilder
    {
        private readonly IToastOptions options;
        private readonly IIntentManager intentManager;
        private readonly IAndroidNotificationManager androidNotificationManager;
        private readonly IServiceProvider? serviceProvider;
        private readonly ILogger<NotificationBuilder>? logger;
        private readonly NotificationCompat.Builder builder;
        private readonly Dictionary<string, string> customArgs;
        bool iconSet;
        bool prioritySet;
        bool defaultsSet;
        bool channelIdSet;
        bool forceOpenAppOnNotificationTap;
        bool buildCompleted;

        public NotificationBuilder(
            IToastOptions options,
            IIntentManager intentManager,
            IAndroidNotificationManager androidNotificationManager,
            IServiceProvider? serviceProvider)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));
            this.intentManager = intentManager ?? throw new ArgumentNullException(nameof(intentManager));
            this.androidNotificationManager = androidNotificationManager;
            this.serviceProvider = serviceProvider;
            this.UseConfigurationFrom<IDroidNotificationExtension>(serviceProvider);
            this.UseConfigurationFrom<IPlatformSpecificExtension>(serviceProvider);
            if (serviceProvider != null)
            {
                this.logger = serviceProvider.GetService<ILogger<NotificationBuilder>>();
            }

            this.Timeout = TimeSpan.FromSeconds(7);
            builder = new NotificationCompat.Builder(Application.Context, options.ChannelOptions.Id);
            customArgs = new Dictionary<string, string>();
        }

        NotificationBuilder AddTitle(string title)
        {
            builder.SetContentTitle(title);
            return this;
        }

        NotificationBuilder AddDescription(string description)
        {
            builder.SetContentText(description);
            return this;
        }

        void BuilderSetDefaults()
        {
            if (iconSet == false)
                builder.SetSmallIcon(options.DefaultIconId);
            if (prioritySet == false)
                builder.SetPriority((int)NotificationPriority.High);
            if (defaultsSet == false)
                builder.SetDefaults((int)NotificationDefaults.All);
            if (channelIdSet == false && AndroidPlatform.IsOreo)
            {
                try { SetChannel(DefaultChannelBuild); }
                catch (MissingMethodException ex)
                {
                    logger?.LogError(ex, "SetChannel error");
                }
            }
        }

        void DefaultChannelBuild(IDroidNotifcationChannelBuilder builder)
        {
            builder.EnableVibration(options.ChannelOptions.EnableVibration)
                .SetShowBadge(options.ChannelOptions.ShowBadge)
                .SetName(options.ChannelOptions.Name)
                .SetImportance(options.ChannelOptions.NotificationImportance)
                .SetId(options.ChannelOptions.Id);
        }

        #region INotificationBuilder implmentation

        public void AddCustomArgsTo(Intent intent)
        {
            foreach (var arg in customArgs)
                intent.PutExtra(arg.Key, arg.Value);
        }

        global::Android.App.Notification IPlatformNotificationBuilder.Build()
            => builder.Build();

        public bool GetForceOpenAppOnNotificationTap() => forceOpenAppOnNotificationTap;
        public bool UsingCustomContentIntent { get; private set; }
        public bool UsingCustomDeleteIntent { get; private set; }
        public bool CleanupOnTimeout { get; private set; }
        public TimeSpan Timeout { get; private set; }

        #endregion

        #region IBuilder implementation

        public INotification Build()
        {
            if (buildCompleted == true)
                throw Exceptions.ExceptionUtils.BuildTwice;
            buildCompleted = true;
            BuilderSetDefaults();
            return new Notification(this, intentManager, androidNotificationManager);
        }

        IBuilder IBuilder.AddDescription(string description) => AddDescription(description);

        public IBuilder WhenUsing<T>(Action<T> buildAction) where T : IBuilderExtension<T>
        {
            if (this is T t)
                buildAction(t);
            return this;
        }

        IBuilder IBuilder.AddTitle(string title) => AddTitle(title);

        public IBuilder UseConfiguration<T>(T token)
        {
            this.UseConfigurationFrom<IDroidNotificationExtension, T>(serviceProvider, token);
            this.UseConfigurationFrom<IPlatformSpecificExtension, T>(serviceProvider, token);
            return this;
        }

        #endregion

        #region IDroidNotificationExtension implementation

        public IDroidNotificationExtension SetCleanupOnTimeout(bool cleanup)
        {
            this.CleanupOnTimeout = cleanup;
            return this;
        }

        public IDroidNotificationExtension SetTimeout(TimeSpan timeout)
        {
            this.Timeout = timeout;
            return this;
        }

        public IDroidNotificationExtension AddPerson(string uri)
        {
            builder.AddPerson(uri);
            return this;
        }

        public IDroidNotificationExtension SetAutoCancel(bool autoCancel)
        {
            builder.SetAutoCancel(autoCancel);
            return this;
        }

        public IDroidNotificationExtension SetBadgeIconType(int icon)
        {
            builder.SetBadgeIconType(icon);
            return this;
        }

        public IDroidNotificationExtension SetCategory(string category)
        {
            builder.SetCategory(category);
            return this;
        }

        public IDroidNotificationExtension SetChannelId(string channelId)
        {
            builder.SetChannelId(channelId);
            channelIdSet = true;
            return this;
        }

        public IDroidNotificationExtension SetColor(int argb)
        {
            builder.SetColor(argb);
            return this;
        }

        public IDroidNotificationExtension SetColorized(bool colorize)
        {
            builder.SetColorized(colorize);
            return this;
        }

        public IDroidNotificationExtension SetContentInfo(string info)
        {
            builder.SetContentInfo(info);
            return this;
        }

        public IDroidNotificationExtension SetContentText(string text)
        {
            builder.SetContentText(text);
            return this;
        }

        public IDroidNotificationExtension SetContentTitle(string title)
        {
            builder.SetContentTitle(title);
            return this;
        }

        public IDroidNotificationExtension SetDefaults(int defaults)
        {
            builder.SetDefaults(defaults);
            defaultsSet = true;
            return this;
        }

        public IDroidNotificationExtension SetGroup(string groupKey)
        {
            builder.SetGroup(groupKey);
            return this;
        }

        public IDroidNotificationExtension SetGroupAlertBehavior(int groupAlertBehavior)
        {
            builder.SetGroupAlertBehavior(groupAlertBehavior);
            return this;
        }

        public IDroidNotificationExtension SetGroupSummary(bool isGroupSummary)
        {
            builder.SetGroupSummary(isGroupSummary);
            return this;
        }

        public IDroidNotificationExtension SetLights(int argb, int onMs, int offMs)
        {
            builder.SetLights(argb, onMs, offMs);
            return this;
        }

        public IDroidNotificationExtension SetLocalOnly(bool b)
        {
            builder.SetLocalOnly(b);
            return this;
        }

        public IDroidNotificationExtension SetNumber(int number)
        {
            builder.SetNumber(number);
            return this;
        }

        public IDroidNotificationExtension SetOngoing(bool ongoing)
        {
            builder.SetOngoing(ongoing);
            return this;
        }

        public IDroidNotificationExtension SetOnlyAlertOnce(bool onlyAlertOnce)
        {
            builder.SetOnlyAlertOnce(onlyAlertOnce);
            return this;
        }

        public IDroidNotificationExtension SetPriority(int pri)
        {
            builder.SetPriority(pri);
            prioritySet = true;
            return this;
        }

        public IDroidNotificationExtension SetProgress(int max, int progress, bool indeterminate)
        {
            builder.SetProgress(max, progress, indeterminate);
            return this;
        }

        public IDroidNotificationExtension SetRemoteInputHistory(string[] text)
        {
            builder.SetRemoteInputHistory(text);
            return this;
        }

        public IDroidNotificationExtension SetShortcutId(string shortcutId)
        {
            builder.SetShortcutId(shortcutId);
            return this;
        }

        public IDroidNotificationExtension SetShowWhen(bool show)
        {
            builder.SetShowWhen(show);
            return this;
        }

        public IDroidNotificationExtension SetSmallIcon(int icon)
        {
            builder.SetSmallIcon(icon);
            iconSet = true;
            return this;
        }

        public IDroidNotificationExtension SetSmallIcon(int icon, int level)
        {
            builder.SetSmallIcon(icon, level);
            iconSet = true;
            return this;
        }

        public IDroidNotificationExtension SetSortKey(string sortKey)
        {
            builder.SetSortKey(sortKey);
            return this;
        }

        public IDroidNotificationExtension SetSubText(string text)
        {
            builder.SetSubText(text);
            return this;
        }

        public IDroidNotificationExtension SetTicker(string tickerText)
        {
            builder.SetTicker(tickerText);
            return this;
        }

        public IDroidNotificationExtension SetTimeoutAfter(long durationMs)
        {
            builder.SetTimeoutAfter(durationMs);
            return this;
        }
        public IDroidNotificationExtension SetUsesChronometer(bool b)
        {
            builder.SetUsesChronometer(b);
            return this;
        }
        public IDroidNotificationExtension SetVibrate(long[] pattern)
        {
            builder.SetVibrate(pattern);
            return this;
        }
        public IDroidNotificationExtension SetVisibility(int visibility)
        {
            builder.SetVisibility(visibility);
            return this;
        }
        public IDroidNotificationExtension SetWhen(long when)
        {
            builder.SetWhen(when);
            return this;
        }

        public IDroidNotificationExtension WithCustomArg(string key, string value)
        {
            customArgs[key] = value;
            return this;
        }

        public IDroidNotificationExtension WithCustomArgs(IEnumerable<(string key, string value)> args)
        {
            foreach (var (key, value) in args)
                customArgs[key] = value;
            return this;
        }

        public IDroidNotificationExtension ForceOpenAppOnNotificationTap(bool forceOpenAppOnNotificationTap)
        {
            this.forceOpenAppOnNotificationTap = forceOpenAppOnNotificationTap;
            return this;
        }

        public IDroidNotificationExtension SetChannel(Action<IDroidNotifcationChannelBuilder> buildAction)
        {
            var builder = new NotificationChannelBuilder();
            buildAction(builder);
            var channel = builder.Build();
            if (channel.Id == null)
                throw new InvalidOperationException(ErrorStrings.KChannelIdError);
            SetChannelId(channel.Id);
            return this;
        }

        public IDroidNotificationExtension Use(IExtensionConfiguration<IDroidNotificationExtension> visitor)
        {
            visitor.Configure(this);
            return this;
        }

        #endregion

        #region IPlatformSpecificExtension implementation

        public IPlatformSpecificExtension AddAction(int icon, ICharSequence title, PendingIntent intent)
        {
            builder.AddAction(icon, title, intent);
            return this;
        }

        public IPlatformSpecificExtension AddAction(NotificationCompat.Action action)
        {
            builder.AddAction(action);
            return this;
        }

        public IPlatformSpecificExtension AddExtras(Bundle extras)
        {
            builder.AddExtras(extras);
            return this;
        }

#if __ANDROID_28__

        public IPlatformSpecificExtension AddInvisibleAction(int icon, string title, PendingIntent intent)
        {
            builder.AddInvisibleAction(icon, title, intent);
            return this;
        }

        public IPlatformSpecificExtension AddInvisibleAction(int icon, ICharSequence title, PendingIntent intent)
        {
            builder.AddInvisibleAction(icon, title, intent);
            return this;
        }

        public IPlatformSpecificExtension AddInvisibleAction(NotificationCompat.Action action)
        {
            builder.AddInvisibleAction(action);
            return this;
        }

#endif

        public IPlatformSpecificExtension Extend(NotificationCompat.IExtender extender)
        {
            builder.Extend(extender);
            return this;
        }

        public IPlatformSpecificExtension SetContent(RemoteViews views)
        {
            builder.SetContent(views);
            return this;
        }

        public IPlatformSpecificExtension SetContentInfo(ICharSequence info)
        {
            builder.SetContentInfo(info);
            return this;
        }

        public IPlatformSpecificExtension SetContentIntent(PendingIntent intent)
        {
            builder.SetContentIntent(intent);
            UsingCustomContentIntent = true;
            return this;
        }

        public IPlatformSpecificExtension SetContentText(ICharSequence text)
        {
            builder.SetContentText(text);
            return this;
        }

        public IPlatformSpecificExtension SetContentTitle(ICharSequence title)
        {
            builder.SetContentTitle(title);
            return this;
        }

        public IPlatformSpecificExtension SetCustomBigContentView(RemoteViews contentView)
        {
            builder.SetCustomBigContentView(contentView);
            return this;
        }

        public IPlatformSpecificExtension SetCustomContentView(RemoteViews contentView)
        {
            builder.SetCustomContentView(contentView);
            return this;
        }

        public IPlatformSpecificExtension SetCustomHeadsUpContentView(RemoteViews contentView)
        {
            builder.SetCustomHeadsUpContentView(contentView);
            return this;
        }

        public IPlatformSpecificExtension SetDeleteIntent(PendingIntent intent)
        {
            builder.SetDeleteIntent(intent);
            UsingCustomDeleteIntent = true;
            return this;
        }

        public IPlatformSpecificExtension SetExtras(Bundle extras)
        {
            builder.SetExtras(extras);
            return this;
        }

        public IPlatformSpecificExtension SetFullScreenIntent(PendingIntent intent, bool highPriority)
        {
            builder.SetFullScreenIntent(intent, highPriority);
            return this;
        }

        public IPlatformSpecificExtension SetLargeIcon(global::Android.Graphics.Bitmap icon)
        {
            builder.SetLargeIcon(icon);
            return this;
        }

        public IPlatformSpecificExtension SetPublicVersion(global::Android.App.Notification n)
        {
            builder.SetPublicVersion(n);
            return this;
        }

        public IPlatformSpecificExtension SetRemoteInputHistory(ICharSequence[] text)
        {
            builder.SetRemoteInputHistory(text);
            return this;
        }

        public IPlatformSpecificExtension SetSound(global::Android.Net.Uri sound)
        {
            builder.SetSound(sound);
            return this;
        }

        public IPlatformSpecificExtension SetSound(global::Android.Net.Uri sound, int streamType)
        {
            builder.SetSound(sound, streamType);
            return this;
        }

        public IPlatformSpecificExtension SetStyle(NotificationCompat.Style style)
        {
            builder.SetStyle(style);
            return this;
        }

        public IPlatformSpecificExtension SetSubText(ICharSequence text)
        {
            builder.SetSubText(text);
            return this;
        }

        public IPlatformSpecificExtension SetTicker(string tickerText, RemoteViews views)
        {
            builder.SetTicker(tickerText, views);
            return this;
        }

        public IPlatformSpecificExtension SetTicker(ICharSequence tickerText, RemoteViews views)
        {
            builder.SetTicker(tickerText, views);
            return this;
        }

        public IPlatformSpecificExtension SetTicker(ICharSequence tickerText)
        {
            builder.SetTicker(tickerText);
            return this;
        }

        public IPlatformSpecificExtension Use(IExtensionConfiguration<IPlatformSpecificExtension> visitor)
        {
            visitor.Configure(this);
            return this;
        }

        #endregion

        #region IBuilderExtension implementation

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.AddTitle(string title)
            => AddTitle(title);

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.AddDescription(string description)
            => AddDescription(description);

        IDroidNotificationExtension IBuilderExtension<IDroidNotificationExtension>.AddTitle(string title)
            => AddTitle(title);

        IDroidNotificationExtension IBuilderExtension<IDroidNotificationExtension>.AddDescription(string description)
            => AddDescription(description);

        #endregion

        #region IExtensionPlugin support

        NotificationBuilder Add<T1>(T1 a1)
        {
            this.UsePlugin<IDroidNotificationExtension, T1>(serviceProvider, a1);
            this.UsePlugin<IPlatformSpecificExtension, T1>(serviceProvider, a1);
            return this;
        }

        NotificationBuilder Add<T1, T2>(T1 a1, T2 a2)
        {
            this.UsePlugin<IDroidNotificationExtension, T1, T2>(serviceProvider, a1, a2);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2>(serviceProvider, a1, a2);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
        {
            this.UsePlugin<IDroidNotificationExtension, T1, T2, T3>(serviceProvider, a1, a2, a3);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3>(serviceProvider, a1, a2, a3);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
        {
            this.UsePlugin<IDroidNotificationExtension, T1, T2, T3, T4>(serviceProvider, a1, a2, a3, a4);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4>(serviceProvider, a1, a2, a3, a4);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
        {
            this.UsePlugin<IDroidNotificationExtension, T1, T2, T3, T4, T5>(serviceProvider, a1, a2, a3, a4, a5);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5>(serviceProvider, a1, a2, a3, a4, a5);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
        {
            this.UsePlugin<IDroidNotificationExtension, T1, T2, T3, T4, T5, T6>(serviceProvider, a1, a2, a3, a4, a5, a6);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5, T6>(serviceProvider, a1, a2, a3, a4, a5, a6);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
        {
            this.UsePlugin<IDroidNotificationExtension, T1, T2, T3, T4, T5, T6, T7>(serviceProvider, a1, a2, a3, a4, a5, a6, a7);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5, T6, T7>(serviceProvider, a1, a2, a3, a4, a5, a6, a7);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
        {
            this.UsePlugin<IDroidNotificationExtension, T1, T2, T3, T4, T5, T6, T7, T8>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8);
            this.UsePlugin<IPlatformSpecificExtension, T1, T2, T3, T4, T5, T6, T7, T8>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8);
            return this;
        }

        NotificationBuilder Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
        {
            this.UsePlugin<IDroidNotificationExtension, T1, T2, T3, T4, T5, T6, T7, T8, T9>(serviceProvider, a1, a2, a3, a4, a5, a6, a7, a8, a9);
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

        IDroidNotificationExtension IBuilderExtension<IDroidNotificationExtension>
            .Add<T1>(T1 a1) => Add(a1);
        IDroidNotificationExtension IBuilderExtension<IDroidNotificationExtension>
            .Add<T1, T2>(T1 a1, T2 a2) => Add(a1, a2);
        IDroidNotificationExtension IBuilderExtension<IDroidNotificationExtension>
            .Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3) => Add(a1, a2, a3);
        IDroidNotificationExtension IBuilderExtension<IDroidNotificationExtension>
            .Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => Add(a1, a2, a3, a4);
        IDroidNotificationExtension IBuilderExtension<IDroidNotificationExtension>
            .Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => Add(a1, a2, a3, a4, a5);
        IDroidNotificationExtension IBuilderExtension<IDroidNotificationExtension>
            .Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => Add(a1, a2, a3, a4, a5, a6);
        IDroidNotificationExtension IBuilderExtension<IDroidNotificationExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => Add(a1, a2, a3, a4, a5, a6, a7);
        IDroidNotificationExtension IBuilderExtension<IDroidNotificationExtension>
            .Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => Add(a1, a2, a3, a4, a5, a6, a7, a8);
        IDroidNotificationExtension IBuilderExtension<IDroidNotificationExtension>
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

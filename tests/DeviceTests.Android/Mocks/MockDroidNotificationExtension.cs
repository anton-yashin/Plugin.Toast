#nullable enable
using LightMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Toast.Droid;
using Plugin.Toast;
using Java.Lang;
using Android.Support.V4.App;
using Android.Graphics;

namespace DeviceTests.Android.Mocks
{
    public sealed class MockDroidNotificationExtension : IDroidNotificationExtension, IPlatformSpecificExtension
    {
        private readonly IInvocationContext<IDroidNotificationExtension> context;
        private readonly IInvocationContext<IPlatformSpecificExtension> platform;

        public MockDroidNotificationExtension(IInvocationContext<IDroidNotificationExtension> context)    
            : this(context, new MockContext<IPlatformSpecificExtension>())
        {
        }


        public MockDroidNotificationExtension(
            IInvocationContext<IDroidNotificationExtension> context,
            IInvocationContext<IPlatformSpecificExtension> platform)
        {
            this.context = context;
            this.platform = platform;
        }

        public IDroidNotificationExtension Add<T1>(T1 a1)
            => context.Invoke(_ => _.Add(a1));

        public IDroidNotificationExtension Add<T1, T2>(T1 a1, T2 a2)
            => context.Invoke(_ => _.Add(a1, a2));

        public IDroidNotificationExtension Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
            => context.Invoke(_ => _.Add(a1, a2, a3));

        public IDroidNotificationExtension Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4));

        public IDroidNotificationExtension Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5));

        public IDroidNotificationExtension Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6));

        public IDroidNotificationExtension Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6, a7));

        public IDroidNotificationExtension Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6, a7, a8));

        public IDroidNotificationExtension Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
            => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9));

        public IPlatformSpecificExtension AddAction(int icon, ICharSequence title, PendingIntent intent)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension AddAction(NotificationCompat.Action action)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension AddDescription(string description)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension AddExtras(Bundle extras)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension AddInvisibleAction(int icon, string title, PendingIntent intent)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension AddInvisibleAction(int icon, ICharSequence title, PendingIntent intent)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension AddInvisibleAction(NotificationCompat.Action action)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension AddPerson(string uri)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension AddTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension Extend(NotificationCompat.IExtender extender)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension ForceOpenAppOnNotificationTap(bool forceOpenAppOnNotificationTap)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetAutoCancel(bool autoCancel)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetBadgeIconType(int icon)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetCategory(string category)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetChannel(Action<IDroidNotifcationChannelBuilder> buildAction)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetChannelId(string channelId)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetCleanupOnTimeout(bool cleanup)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetColor(int argb)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetColorized(bool colorize)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetContent(RemoteViews views)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetContentInfo(string info)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetContentInfo(ICharSequence info)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetContentIntent(PendingIntent intent)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetContentText(string text)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetContentText(ICharSequence text)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetContentTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetContentTitle(ICharSequence title)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetCustomBigContentView(RemoteViews contentView)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetCustomContentView(RemoteViews contentView)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetCustomHeadsUpContentView(RemoteViews contentView)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetDefaults(int defaults)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetDeleteIntent(PendingIntent intent)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetExtras(Bundle extras)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetFullScreenIntent(PendingIntent intent, bool highPriority)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetGroup(string groupKey)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetGroupAlertBehavior(int groupAlertBehavior)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetGroupSummary(bool isGroupSummary)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetLargeIcon(Bitmap icon)
            => platform.Invoke(_ => _.SetLargeIcon(icon));

        public IDroidNotificationExtension SetLights(int argb, int onMs, int offMs)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetLocalOnly(bool b)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetNumber(int number)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetOngoing(bool ongoing)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetOnlyAlertOnce(bool onlyAlertOnce)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetPriority(int pri)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetProgress(int max, int progress, bool indeterminate)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetPublicVersion(global::Android.App.Notification n)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetRemoteInputHistory(string[] text)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetRemoteInputHistory(ICharSequence[] text)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetShortcutId(string shortcutId)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetShowWhen(bool show)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetSmallIcon(int icon)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetSmallIcon(int icon, int level)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetSortKey(string sortKey)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetSound(global::Android.Net.Uri sound)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetSound(global::Android.Net.Uri sound, int streamType)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetStyle(NotificationCompat.Style style)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetSubText(string text)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetSubText(ICharSequence text)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetTicker(string tickerText)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetTicker(string tickerText, RemoteViews views)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetTicker(ICharSequence tickerText, RemoteViews views)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension SetTicker(ICharSequence tickerText)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetTimeout(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetTimeoutAfter(long durationMs)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetUsesChronometer(bool b)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetVibrate(long[] pattern)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetVisibility(int visibility)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension SetWhen(long when)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension Use(IExtensionConfiguration<IDroidNotificationExtension> visitor)
        {
            throw new NotImplementedException();
        }

        public IPlatformSpecificExtension Use(IExtensionConfiguration<IPlatformSpecificExtension> visitor)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension WithCustomArg(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IDroidNotificationExtension WithCustomArgs(IEnumerable<(string key, string value)> args)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1>(T1 a1)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2>(T1 a1, T2 a2)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.AddDescription(string description)
        {
            throw new NotImplementedException();
        }

        IPlatformSpecificExtension IBuilderExtension<IPlatformSpecificExtension>.AddTitle(string title)
        {
            throw new NotImplementedException();
        }
    }
}
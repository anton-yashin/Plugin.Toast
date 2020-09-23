using LightMock;
using System;
using System.Collections.Generic;

using Foundation;
using Plugin.Toast;
using Plugin.Toast.IOS;
using UserNotifications;

namespace DeviceTests.iOS.Mocks
{
    public sealed class MockIosNotificationExtension
    {
        public MockContext<IPlatformSpecificExtension> Platform { get; }
            = new MockContext<IPlatformSpecificExtension>();
        public MockContext<IIosNotificationExtension> Context { get; }
            = new MockContext<IIosNotificationExtension>();

        readonly Implementation implementation;
        public IIosNotificationExtension CommonObject => implementation;
        public IPlatformSpecificExtension SpecificObject => implementation;

        public MockIosNotificationExtension()
        {
            implementation = new Implementation(Context, Platform);
        }

        sealed class Implementation : IPlatformSpecificExtension
        {
            private readonly IInvocationContext<IIosNotificationExtension> context;
            private readonly IInvocationContext<IPlatformSpecificExtension> platform;

            public Implementation(IInvocationContext<IIosNotificationExtension> context,
                IInvocationContext<IPlatformSpecificExtension> platform)
            {
                this.context = context;
                this.platform = platform;
            }

            public IIosNotificationExtension Add<T1>(T1 a1)
                => context.Invoke(_ => _.Add(a1));

            public IIosNotificationExtension Add<T1, T2>(T1 a1, T2 a2)
                => context.Invoke(_ => _.Add(a1, a2));

            public IIosNotificationExtension Add<T1, T2, T3>(T1 a1, T2 a2, T3 a3)
                => context.Invoke(_ => _.Add(a1, a2, a3));

            public IIosNotificationExtension Add<T1, T2, T3, T4>(T1 a1, T2 a2, T3 a3, T4 a4)
                => context.Invoke(_ => _.Add(a1, a2, a3, a4));

            public IIosNotificationExtension Add<T1, T2, T3, T4, T5>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5)
                => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5));

            public IIosNotificationExtension Add<T1, T2, T3, T4, T5, T6>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6)
                => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6));

            public IIosNotificationExtension Add<T1, T2, T3, T4, T5, T6, T7>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7)
                => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6, a7));

            public IIosNotificationExtension Add<T1, T2, T3, T4, T5, T6, T7, T8>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8)
                => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6, a7, a8));

            public IIosNotificationExtension Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 a1, T2 a2, T3 a3, T4 a4, T5 a5, T6 a6, T7 a7, T8 a8, T9 a9)
                => context.Invoke(_ => _.Add(a1, a2, a3, a4, a5, a6, a7, a8, a9));

            public IPlatformSpecificExtension AddAttachment(UNNotificationAttachment attachment)
                => platform.Invoke(_ => _.AddAttachment(attachment));

            public IPlatformSpecificExtension AddAttachments(IEnumerable<UNNotificationAttachment> attachments)
                => platform.Invoke(_ => _.AddAttachments(attachments));

            public IPlatformSpecificExtension AddAttachments(UNNotificationAttachment[] attachments)
            {
                throw new NotImplementedException();
            }

            public IPlatformSpecificExtension AddBadgeNumber(NSNumber number)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension AddBadgeNumber(int number)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension AddBody(string body)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension AddCategoryIdentifier(string categoryIdentifier)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension AddDescription(string description)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension AddLaunchImageName(string launchImageName)
            {
                throw new NotImplementedException();
            }

            public IPlatformSpecificExtension AddSound(UNNotificationSound sound)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension AddSummaryArgument(string summaryArgument)
            {
                throw new NotImplementedException();
            }

            public IPlatformSpecificExtension AddSummaryArgumentCount(nuint SummaryArgumentCount)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension AddSummaryArgumentCount(ulong SummaryArgumentCount)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension AddTargetContentIdentifier(string targetContentIdentifier)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension AddThreadIdentifier(string threadIdentifier)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension AddTitle(string title)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension Use(IExtensionConfiguration<IIosNotificationExtension> visitor)
            {
                throw new NotImplementedException();
            }

            public IPlatformSpecificExtension Use(IExtensionConfiguration<IPlatformSpecificExtension> visitor)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension WithCustomArg(string key, string value)
            {
                throw new NotImplementedException();
            }

            public IIosNotificationExtension WithCustomArgs(IEnumerable<(string key, string value)> args)
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
}
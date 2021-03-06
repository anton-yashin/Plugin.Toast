﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Toast;
using ManualTests.ResX;
using ManualTests.Tests.Base;
using Plugin.Toast.Abstractions;

namespace ManualTests.Tests
{
    sealed class ShowAlternativeScheduled : AbstractTest<ShowAlternativeScheduled>
    {
        public ShowAlternativeScheduled(IServiceProvider serviceProvider)
            :base (serviceProvider, Localization.R_REQUIRED_ACTION_IGNORE_NOTIFICATION, Localization.R_TEST_NAME_ALT_SCHEDULED)
        { }

        public override bool IsAvailable => Device.RuntimePlatform != Device.UWP;

        protected override async Task DoRunAsync()
        {
            await serviceProvider.GetRequiredService<IInitialization>().InitializeAsync();
            var scheduleTo = DateTimeOffset.Now + TimeSpan.FromSeconds(3);
            var builder = serviceProvider.GetRequiredService<INotificationBuilder<ISnackbarExtension, IIosLocalNotificationExtension>>();
            using var token = builder.AddTitle(Localization.R_SCHEDULED_TITLE)
                .WhenUsing<ISnackbarExtension>(_ => _.AddDescription(Localization.R_CANT_SCHEDULE_SNACKBAR))
                .WhenUsing<IIosLocalNotificationExtension>(_ => _.AddDescription(Localization.R_PLEASE_IGNORE))
                .Build().ScheduleTo(scheduleTo);
            await Task.Delay(TimeSpan.FromSeconds(6));
            Assert(true);
        }
    }
}

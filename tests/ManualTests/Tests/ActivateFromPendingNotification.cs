﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Plugin.Toast;
using ManualTests.Tests.Base;
using System.IO;
using Newtonsoft.Json;

namespace ManualTests.Tests
{
    sealed class ActivateFromPendingNotification : AbstractTest<IgnoreStandard>
    {
        private readonly INotificationEventSource eventSender;
        private readonly JsonSerializer jsonSerializer;
        ToastId? tid;

        public ActivateFromPendingNotification(IServiceProvider serviceProvider)
            :base (serviceProvider, "Click run button then close app and tap to notification", "Activation of inactive app from notification center")
        {
            this.eventSender = serviceProvider.GetRequiredService<INotificationEventSource>();
            this.jsonSerializer = JsonSerializer.CreateDefault();
        }

        private void OnNotificationEvent(object sender, NotificationEvent e)
        {
            Assert(e.ToastId == tid);
            eventSender.NotificationReceived -= OnNotificationEvent;
        }

        protected override async Task DoRunAsync()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nameof(ActivateFromPendingNotification) + ".testdata");
            if (File.Exists(path))
            {
                using (var file = File.OpenRead(path))
                using (var sr = new StreamReader(file))
                using (var jr = new JsonTextReader(sr))
                    tid = jsonSerializer.Deserialize<ToastId>(jr);
                File.Delete(path);
                eventSender.NotificationReceived += OnNotificationEvent;
                eventSender.SendPendingEvents();
            }
            else
            {
                var task = serviceProvider.GetService<IBuilder>()
                    .AddTitle("First close app").AddDescription("then tap me")
                    .Build().ShowAsync(out tid);
                using (var file = File.Create(path))
                using (var sw = new StreamWriter(file))
                using (var jw = new JsonTextWriter(sw))
                {
                    jsonSerializer.Serialize(jw, tid);
                    await jw.FlushAsync();
                }
                var result = await task;
                if (result == NotificationResult.Activated)
                    Assert(false);
            }
        }
    }
}
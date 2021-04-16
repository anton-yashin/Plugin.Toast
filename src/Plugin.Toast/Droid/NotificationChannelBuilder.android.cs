using Android.App;
using System;
using ANotificationChannel = global::Android.App.NotificationChannel;
using ANotificationImportance = global::Android.App.NotificationImportance;
using ANotificationManager = Android.App.NotificationManager;

namespace Plugin.Toast.Droid
{

    public interface INotificationChannelBuilder : IDroidNotifcationChannelBuilder
    {
        INotificationChannelBuilder SetSound(global::Android.Net.Uri sound, global::Android.Media.AudioAttributes audioAttributes);
    }

    internal interface IInternalNotificationChannelBuilder : INotificationChannelBuilder
    {
        ANotificationChannel Build();
    }

    sealed class NotificationChannelBuilder : INotificationChannelBuilder, IInternalNotificationChannelBuilder
    {
        bool? lights;
        bool? vibration;
        bool? allowBubbles;
        bool? bypassDnd;
        string? group;
        string? id;
        DroidNotificationImportance? notificationImportance;
        string? name;
        bool? showBadge;
        long[]? vibrationPattern;
        string? description;
        global::Android.Net.Uri? sound;
        global::Android.Media.AudioAttributes? audioAttributes;

        public NotificationChannelBuilder()
        {
        }

        public ANotificationChannel Build()
        {
            if (name == null)
                throw new InvalidOperationException("name required");
            if (id == null)
                id = GenerateIdByName(name);
            var nm = ANotificationManager.FromContext(Application.Context)
                ?? throw new InvalidOperationException(ErrorStrings.KNotificationManagerError);
            var channel = nm.GetNotificationChannel(id);
            if (channel != null)
                return channel;
            ANotificationImportance ni = this.notificationImportance != null 
                ? (ANotificationImportance)(int)this.notificationImportance
                : ANotificationImportance.Default;
            var anc = new ANotificationChannel(id, name, ni);
            if (description != null)
                anc.Description = description;
            Apply(lights, anc.EnableLights);
            Apply(vibration, anc.EnableVibration);
#if __ANDROID_29__
            Apply(allowBubbles, anc.SetAllowBubbles);
#endif
            Apply(bypassDnd, anc.SetBypassDnd);
            if (group != null) anc.Group = group;
            Apply(showBadge, anc.SetShowBadge);
            if (vibrationPattern != null) anc.SetVibrationPattern(vibrationPattern);
            if (sound != null && audioAttributes != null)
                anc.SetSound(sound, audioAttributes);
            nm.CreateNotificationChannel(anc);
            return anc;
        }

        void Apply(bool? value, Action<bool> to)
        {
            if (value != null)
                to(value.Value);
        }

        private string GenerateIdByName(string name) => name.Replace(" ", "").ToLower();

        public IDroidNotifcationChannelBuilder EnableLights(bool lights)
        {
            this.lights = lights;
            return this;
        }

        public IDroidNotifcationChannelBuilder EnableVibration(bool vibration)
        {
            this.vibration = vibration;
            return this;
        }

        public IDroidNotifcationChannelBuilder SetAllowBubbles(bool allowBubbles)
        {
            this.allowBubbles = allowBubbles;
            return this;
        }

        public IDroidNotifcationChannelBuilder SetBypassDnd(bool bypassDnd)
        {
            this.bypassDnd = bypassDnd;
            return this;
        }

        public IDroidNotifcationChannelBuilder SetGroup(string group)
        {
            this.group = group;
            return this;
        }

        public IDroidNotifcationChannelBuilder SetId(string id)
        {
            this.id = id;
            return this;
        }

        public IDroidNotifcationChannelBuilder SetImportance(DroidNotificationImportance notificationImportance)
        {
            this.notificationImportance = notificationImportance;
            return this;
        }

        public IDroidNotifcationChannelBuilder SetName(string name)
        {
            this.name = name;
            return this;
        }

        public IDroidNotifcationChannelBuilder SetShowBadge(bool showBadge)
        {
            this.showBadge = showBadge;
            return this;
        }

        public IDroidNotifcationChannelBuilder SetVibrationPattern(long[] vibrationPattern)
        {
            this.vibrationPattern = (long[])vibrationPattern.Clone();
            return this;
        }

        public IDroidNotifcationChannelBuilder SetDescription(string description)
        {
            this.description = description;
            return this;
        }

        public INotificationChannelBuilder SetSound(global::Android.Net.Uri sound, global::Android.Media.AudioAttributes audioAttributes)
        {
            this.sound = sound;
            this.audioAttributes = audioAttributes;
            return this;
        }

    }
}

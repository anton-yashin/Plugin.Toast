using Android.Content;
using Android.Support.Annotation;
using Plugin.Toast.Droid;
using System;
using System.Runtime.Serialization;
using System.Threading;

namespace Plugin.Toast
{
    public sealed partial class ToastId : IEquatable<ToastId>
    {
        int id;
        string tag;

        [DataMember]
        public int Id
        {
            get => id;
            [Obsolete(KObsoleteMessage, true)]
            set => id = value;
        }


        [DataMember]
        public string Tag 
        {
            get => tag;
            [Obsolete(KObsoleteMessage, true)]
            set => tag = value;
        }

        public ToastId(int id, string tag)
        {
            this.id = id;
            this.tag = tag;
        }

        (int id, string tag) AsTuple() => (Id, Tag);

        static int idGenerator;

        /// <summary>
        /// Creates a new <see cref="ToastId"/> with unique pair of values
        /// </summary>
        /// <returns>new <see cref="ToastId"/></returns>
        public static ToastId New()
            => new ToastId(Interlocked.Increment(ref idGenerator), Guid.NewGuid().ToString());

        const int KInvalidId = -1;

        /// <summary>
        /// Construct <see cref="ToastId"/> from <see cref="Intent"/>.
        /// </summary>
        /// <param name="intent">Intent to check</param>
        /// <returns><see cref="ToastId"/> if the required data was found in the <see cref="Intent"/>, otherwise null</returns>
        public static ToastId? FromIntent(Intent? intent)
        {
            if (intent != null)
            {
                int notificationId = intent.Extras?.GetInt(IntentConstants.KNotificationId, KInvalidId) ?? KInvalidId;
                string? notificationTag = intent.Extras?.GetString(IntentConstants.KNotifcationTag);
                if (notificationId != KInvalidId && notificationTag != null)
                    return new ToastId(notificationId, notificationTag);
            }
            return null;
        }

        /// <summary>
        /// Write <see cref="ToastId"/> to <see cref="Intent"/>
        /// </summary>
        /// <param name="intent">Intent to store data</param>
        public void ToIntent(Intent intent)
        {
            intent.PutExtra(IntentConstants.KNotifcationTag, Tag);
            intent.PutExtra(IntentConstants.KNotificationId, Id);
        }

        bool PlatformEquals(ToastId? other) => other != null && AsTuple() == other.AsTuple();
        bool PlatformEquals(object? obj) => PlatformEquals(obj as ToastId);
        private int PlatformGetHashCode() => (Id, Tag).GetHashCode();
        private string PlatformToString() => string.Format("Id: {0}, Tag: {1}", Id, Tag);

        private int GetPlatformPersistentHashCode()
            => CombineHashCode(CombineHashCode(KMagicSeed, Id), Tag);
    }
}

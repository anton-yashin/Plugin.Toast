using System;
using System.Runtime.Serialization;
using System.Threading;

namespace Plugin.Toast
{
    public sealed partial class ToastId : IEquatable<ToastId>
    {
        [DataMember]
        public int Id { get; }

        [DataMember]
        public string Tag { get; }

        public ToastId(int id, string tag)
        {
            Id = id;
            Tag = tag;
        }

        (int id, string tag) AsTuple() => (Id, Tag);

        static int idGenerator;

        public static ToastId New()
            => new ToastId(Interlocked.Increment(ref idGenerator), Guid.NewGuid().ToString());

        bool PlatformEquals(ToastId? other) => other != null && AsTuple() == other.AsTuple();
        bool PlatformEquals(object? obj) => PlatformEquals(obj as ToastId);
        private int PlatformGetHashCode() => (Id, Tag).GetHashCode();
        private string PlatformToString() => string.Format("Id: {0}, Tag: {1}", Id, Tag);

        private int GetPlatformPersistentHashCode()
            => CombineHashCode(CombineHashCode(KMagicSeed, Id), Tag);
    }
}

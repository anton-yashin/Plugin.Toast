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

        public bool Equals(ToastId? other) => other != null && AsTuple() == other.AsTuple();

        public override bool Equals(object? obj) => Equals(obj as ToastId);

        public override int GetHashCode() => (Id, Tag).GetHashCode();


        static int idGenerator;

        public static ToastId New()
            => new ToastId(Interlocked.Increment(ref idGenerator), Guid.NewGuid().ToString());

        private int GetPlatformPersistentHashCode()
            => CombineHashCode(CombineHashCode(KMagicSeed, Id), Tag);
    }
}

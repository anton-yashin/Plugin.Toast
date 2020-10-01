﻿using System;
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

        public bool Equals(ToastId? other) => other != null && (Id, Tag) == (other.Id, other.Tag);

        public override bool Equals(object obj) => Equals(obj as ToastId);

        public override int GetHashCode() => (Id, Tag).GetHashCode();

        public int ActivityRequestCode => GetHashCode();

        static int idGenerator;

        public static ToastId New()
            => new ToastId(Interlocked.Increment(ref idGenerator), Guid.NewGuid().ToString());
    }
}

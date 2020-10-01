using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Plugin.Toast
{
    public partial class ToastId : IEquatable<ToastId>
    {
        public ToastId(string id) => Id = id;

        [DataMember]
        public string Id { get; }

        public bool Equals(ToastId? other) => other != null && Id == other.Id;

        public override bool Equals(object obj) => Equals(obj as ToastId);

        public override int GetHashCode() => Id.GetHashCode();
    }
}

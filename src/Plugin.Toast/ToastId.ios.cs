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

        bool PlatformEquals(ToastId? other) => other != null && Id == other.Id;
        bool PlatformEquals(object? obj) => PlatformEquals(obj as ToastId);
        private int PlatformGetHashCode() => Id.GetHashCode();
        private string PlatformToString() => "Id: " + Id;


        int GetPlatformPersistentHashCode() => CombineHashCode(KMagicSeed, Id);
    }
}

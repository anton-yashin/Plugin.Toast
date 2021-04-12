using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Foundation;

namespace Plugin.Toast
{
    public partial class ToastId : IEquatable<ToastId>
    {
        string id;
        public ToastId(string id) => this.id = id;

        [DataMember, Preserve]
        public string Id 
        {
            get => id;
            [Obsolete(KObsoleteMessage, true)]
            set => id = value;
        }

        bool PlatformEquals(ToastId? other) => other != null && Id == other.Id;
        bool PlatformEquals(object? obj) => PlatformEquals(obj as ToastId);
        private int PlatformGetHashCode() => Id.GetHashCode();
        private string PlatformToString() => "Id: " + Id;


        int GetPlatformPersistentHashCode() => CombineHashCode(KMagicSeed, Id);
    }
}

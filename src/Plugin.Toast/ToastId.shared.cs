using System;
using System.Runtime.Serialization;

namespace Plugin.Toast
{
    /// <summary>
    /// An identifier that you can obtain after notification is shown or scheduled
    /// See also: 
    /// <seealso cref="IHistory"/><br/>
    /// <seealso cref="INotification.ShowAsync(out ToastId, System.Threading.CancellationToken)"/><br/>
    /// <seealso cref="IScheduledToastCancellation.ToastId"/><br/>
    /// </summary>
    [DataContract]
    public sealed partial class ToastId
    {
        private ToastId() => throw Exceptions.ExceptionUtils.NotSupportedOrImplementedException;

        /// <summary>
        /// Calculates a hash code that persistent between runs
        /// </summary>
        /// <returns>Hash code</returns>
        public int GetPersistentHashCode() => persistentHashCode ?? (persistentHashCode = GetPlatformPersistentHashCode()).Value;

        int? persistentHashCode;

        internal const int KMagicSeed = 1558670046; // A random number. Must be persistent between runs.

        internal static int CombineHashCode(int h1, int h2)
        {
            unchecked
            {
                uint num = (uint)(h1 << 5) | ((uint)h1 >> 27);
                return ((int)num + h1) ^ h2;
            }
        }

        internal static int CombineHashCode(int h1, string? h2)
        {
            if (h2 != null)
            {
                int i = 0;
                for (; i + 1 < h2.Length; i += 2)
                    h1 = CombineHashCode(h1, h2[i + 1] << 16 | h2[i]);
                if (i < h2.Length)
                    h1 = CombineHashCode(h1, h2[i]);
            }
            return h1;
        }

    }
}

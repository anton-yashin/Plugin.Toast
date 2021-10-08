using System;

namespace Plugin.Toast
{
    internal static class DateTimeExtensions
    {
        public static long ToAndroidTimeStamp(this DateTime @this)
        {
            const long KUnixEpochBase = 62135596800000L;
            return @this.Ticks / 10000 - KUnixEpochBase;
        }
    }
}

using Foundation;
using System;

namespace Plugin.Toast.IOS
{
    internal static class DateExtensions
    {
        public static NSDateComponents ToNSDateComponents(this DateTimeOffset date)
            => new NSDateComponents()
            {
                Year = date.Year,
                Minute = date.Minute,
                Second = date.Second,
                Hour = date.Hour,
                Month = date.Month,
                Day = date.Day,
                TimeZone = NSTimeZone.FromGMT((nint)date.Offset.TotalSeconds)
            };
    }
}

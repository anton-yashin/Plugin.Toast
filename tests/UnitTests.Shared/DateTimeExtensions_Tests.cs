using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class DateTimeExtensions_Tests
    {
        [Fact]
        public void UnixEpochStart()
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var timestamp = dt.ToAndroidTimeStamp();
            Assert.Equal(0, timestamp);
        }
    }
}

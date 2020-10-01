using System;
using System.Collections.Generic;
using Plugin.Toast;
using Xunit;

namespace DeviceTests.iOS
{
    public class ToastId_Tests
    {
        [Fact]
        public void Equals_()
        {
            var someId = Guid.NewGuid().ToString();
            var left = new ToastId(someId);
            var right = new ToastId(someId);

            Assert.Equal(left, right);
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }
    }
}

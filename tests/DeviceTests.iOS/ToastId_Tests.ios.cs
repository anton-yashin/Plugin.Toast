#nullable enable
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

        [Theory, MemberData(nameof(GetTestData_GetPersistentHashCode))]
        public void GetPersistentHashCode(int expected, string id)
        {
            // preapre
            var tid = new ToastId(id);

            // act & verify
            Assert.Equal(expected, tid.GetPersistentHashCode());
        }

        static IEnumerable<object?[]> GetTestData_GetPersistentHashCode()
        {
            yield return new object?[] { ToastId.KMagicSeed, null};
            yield return new object?[] { -103495992, "a" };
            yield return new object?[] { -105462072, "ab" };
            yield return new object?[] { 814718852, "abc" };
        }


    }
}

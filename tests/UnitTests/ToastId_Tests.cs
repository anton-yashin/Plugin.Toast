#nullable enable
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class ToastId_Tests
    {
        [Theory, MemberData(nameof(GetTestData_CombineHashCode_int_int))]
        public void CombineHashCode_int_int(int expected, int left, int right)
        {
            Assert.Equal(expected, ToastId.CombineHashCode(left, right));
        }

        public static IEnumerable<object[]> GetTestData_CombineHashCode_int_int()
        {
            yield return new object[] { 65, 2, 3};
            yield return new object[] { -103496022, ToastId.KMagicSeed, 3 };
        }

        [Theory, MemberData(nameof(GetTestData_CombineHashCode_int_string))]
        public void CombineHashCode_int_string(int expected, int left, string right)
        {
            Assert.Equal(expected, ToastId.CombineHashCode(left, right));
        }

        public static IEnumerable<object?[]> GetTestData_CombineHashCode_int_string()
        {
            yield return new object?[] { (1, 2).GetHashCode(), (1, 2).GetHashCode(), null };
            yield return new object?[] { -103495992, ToastId.KMagicSeed, "a" };
            yield return new object?[] { -105462072, ToastId.KMagicSeed, "ab" };
            yield return new object?[] { 814718852, ToastId.KMagicSeed, "abc" };
        }
    }
}

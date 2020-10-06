﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Toast;
using Xunit;

namespace DeviceTests.Android
{
    public class ToastId_Tests
    {
        [Theory, MemberData(nameof(GetTestData_Equals_))]
        public void Equals_(int id, string tag)
        {
            // prepare
            var left = new ToastId(id, tag);
            var right = new ToastId(id, tag);

            // act & verify
            Assert.Equal(left, right);
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }

        static IEnumerable<object?[]> GetTestData_Equals_()
        {
            var r = new Random();
            yield return new object?[] { r.Next(), Guid.NewGuid().ToString() };
            yield return new object?[] { r.Next(), null };
        }

        [Theory, MemberData(nameof(GetTestData_GetPersistentHashCode))]
        public void GetPersistentHashCode(int expected, int id, string tag)
        {
            // preapre
            var tid = new ToastId(id, tag);

            // act & verify
            Assert.Equal(expected, tid.GetPersistentHashCode());
        }

        static IEnumerable<object?[]> GetTestData_GetPersistentHashCode()
        {
            yield return new object?[] { -103495839, 456, null };
            yield return new object?[] { 879604673, 456, "a" };
            yield return new object?[] { 873444289, 456, "ab" };
            yield return new object?[] { -1241109628, 456, "abc" };
        }

        [Theory, MemberData(nameof(GetTestData_OperatorEquals))]
        public void OperatorEquals(bool expected, ToastId left, ToastId right)
        {
            var actual = (left == right);
            Assert.Equal(expected, actual);
        }

        [Theory, MemberData(nameof(GetTestData_OperatorEquals))]
        public void OperatorNotEquals(bool expected, ToastId left, ToastId right)
        {
            var actual = (left != right);
            Assert.NotEqual(expected, actual);
        }

        static IEnumerable<object?[]> GetTestData_OperatorEquals()
        {
            yield return new object?[] { true, new ToastId(123, "def"), new ToastId(123, "def") };
            yield return new object?[] { true, new ToastId(123, null!), new ToastId(123, null!) };
            yield return new object?[] { true, null, null };
            yield return new object?[] { false, new ToastId(123, "def"), null };
            yield return new object?[] { false, null, new ToastId(123, "def") };
            yield return new object?[] { false, new ToastId(123, "def"), new ToastId(456, "abc") };
        }
    }
}
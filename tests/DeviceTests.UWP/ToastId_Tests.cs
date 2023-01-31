#nullable enable
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Xunit;

namespace DeviceTests.UWP
{
    public class ToastId_Tests
    {
        [Theory, MemberData(nameof(GetEqualsData))]
        public void Equals_(string tag, string group)
        {
            // prepare
            var left = new ToastId(tag, group);
            var right = new ToastId(tag, group);

            // act & verify
            Assert.Equal(left, right);
            Assert.Equal(left.GetHashCode(), right.GetHashCode());
        }

        public static IEnumerable<object?[]> GetEqualsData()
        {
            yield return new object?[] { Guid.NewGuid().ToString(), Guid.NewGuid().ToString()};
            yield return new object?[] { null, Guid.NewGuid().ToString() };
            yield return new object?[] { Guid.NewGuid().ToString(), null };
            yield return new object?[] { null, null };
        }

        [Fact]
        public void FromToastNotification()
        {
            var tn = new ToastNotification(new XmlDocument())
            {
                Tag = Guid.NewGuid().ToString(),
                Group = Guid.NewGuid().ToString(),
            };

            var toastId = ToastId.FromNotification(tn);

            Assert.Equal(tn.Tag, toastId.Tag);
            Assert.Equal(tn.Group, toastId.Group);
        }

        [Fact]
        public void FromScheduledToastNotification()
        {
            var tn = new ScheduledToastNotification(new XmlDocument(), DateTimeOffset.Now + TimeSpan.FromDays(1))
            {
                Tag = Guid.NewGuid().ToString(),
                Group = Guid.NewGuid().ToString(),
            };

            var toastId = ToastId.FromNotification(tn);

            Assert.Equal(tn.Tag, toastId.Tag);
            Assert.Equal(tn.Group, toastId.Group);
        }

        [Theory, MemberData(nameof(GetTestData_GetPersistentHashCode))]
        public void GetPersistentHashCode(int expected, string tag, string group)
        {
            // preapre
            var tid = new ToastId(tag, group);

            // act & verify
            Assert.Equal(expected, tid.GetPersistentHashCode());
        }

        public static IEnumerable<object?[]> GetTestData_GetPersistentHashCode()
        {
            yield return new object?[] { 1558670046, null, null };
            yield return new object?[] { 1314885743, "ab", "cde" };
            yield return new object?[] { -105462072, "ab", null };
            yield return new object?[] { 801742924, null, "cde" };
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

        public static IEnumerable<object?[]> GetTestData_OperatorEquals()
        {
            yield return new object?[] { true, new ToastId("abc", "def"), new ToastId("abc", "def") };
            yield return new object?[] { true, new ToastId("abc", null!), new ToastId("abc", null!) };
            yield return new object?[] { true, new ToastId(null!, "abc"), new ToastId(null!, "abc") };
            yield return new object?[] { true, new ToastId(null!, null!), new ToastId(null!, null!) };
            yield return new object?[] { true, null, null };
            yield return new object?[] { false, new ToastId("abc", "def"), null };
            yield return new object?[] { false, null, new ToastId("abc", "def") };
            yield return new object?[] { false, new ToastId("abc", "def"), new ToastId("def", "abc") };
        }

        [Fact]
        public void NewtownSoftJsonSerialization()
        {
            // prepare
            byte[] data;
            ToastId? result;
            var expected = new ToastId(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            var jsonSerializer = Newtonsoft.Json.JsonSerializer.CreateDefault();

            // act
            using (var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms))
            {
                jsonSerializer.Serialize(sw, expected);
                sw.Flush();
                data = ms.ToArray();
            }

            using (var ms = new MemoryStream(data))
            using (var sr = new StreamReader(ms))
            {
                result = (ToastId?)jsonSerializer.Deserialize(sr, typeof(ToastId));
            }

            // verify
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task SystemTextJsonSerialization()
        {
            // prepare
            byte[] data;
            ToastId? result;
            var expected = new ToastId(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            // act
            using (var ms = new MemoryStream())
            {
                await System.Text.Json.JsonSerializer.SerializeAsync(ms, expected);
                data = ms.ToArray();
            }

            using (var ms = new MemoryStream(data))
            {
                result = await System.Text.Json.JsonSerializer.DeserializeAsync<ToastId>(ms);
            }

            // verify
            Assert.Equal(expected, result);
        }
    }
}

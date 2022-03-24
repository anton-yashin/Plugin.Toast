#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
            yield return new object?[] { true, new ToastId("abc"), new ToastId("abc") };
            yield return new object?[] { true, new ToastId(null!), new ToastId(null!) };
            yield return new object?[] { true, null, null };
            yield return new object?[] { false, new ToastId("abc"), null };
            yield return new object?[] { false, null, new ToastId("abc") };
            yield return new object?[] { false, new ToastId("def"), new ToastId("abc") };
        }

        [Fact]
        public void NewtownSoftJsonSerialization()
        {
            // prepare
            byte[] data;
            ToastId? result;
            var expected = new ToastId(Guid.NewGuid().ToString());
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
            var expected = new ToastId(Guid.NewGuid().ToString());

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

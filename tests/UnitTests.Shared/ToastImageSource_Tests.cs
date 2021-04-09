using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using LightMock.Generator;

namespace UnitTests
{
    public class ToastImageSource_Tests
    {
#if NETCORE_APP

        [Fact]
        public void AllowToUseMoq()
        {
            var mock = new Mock<ToastImageSource>();

            Assert.NotNull(mock.Object);
        }

#endif
    }
}

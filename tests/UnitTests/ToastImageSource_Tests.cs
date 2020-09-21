using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
#if NETCORE_APP
using Moq;
#endif

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

using DeviceTests.Utils;
using DeviceTests.UWP.Mocks;
using LightMock;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DeviceTests.UWP
{
    public class UwpImageRouter_Tests
    {
        [Theory, MemberData(nameof(GetInvalidRoutes))]
        public void ThrowsExceptionOnUnknownRoute(object route)
        {
            // prepare
            var router = new UwpImageRouter();
            var mockContext = new MockContext<IUwpExtension>();
            var mockExtension = new MockUwpExtension(mockContext);
            var ims = new SealedToastImageSource(new Uri("https://www.example.com/some.png"));

            // act && verify
            Assert.Throws<InvalidOperationException>(() => router.Configure(mockExtension, ims, (Router.Route)route));
        }

        public static IEnumerable<object[]> GetInvalidRoutes()
            => EnumUtils.GetEnumValuesExclude(Router.Route.Default).Select(i => new object[] { i });

        [Fact]
        public void Configure()
        {
            // prepare
            var uri = new Uri("https://www.example.com/some.png");
            var router = new UwpImageRouter();
            var mockContext = new MockContext<IUwpExtension>();
            var mockExtension = new MockUwpExtension(mockContext);
            var ims = new SealedToastImageSource(uri);

            // act
            router.Configure(mockExtension, ims, Router.Route.Default);

            // verify
            mockContext.Assert(_ => _.AddAppLogoOverride(uri, null, null, null));
        }
    }
}

using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace UnitTests
{
    public class ResourceToFileNameStrategy_Tests
    {
        [Fact]
        public void Convert()
        {
            // prepare
            var assembly = Assembly.GetExecutingAssembly();
            var name = assembly.GetName();
            const string KResource = "some.resource.png";
            var strategy = new ResourceToFileNameStrategy();

            // act
            var result = strategy.Convert(KResource, assembly);

            // verify
            Assert.Equal(ResourceToFileNameStrategy.KFolder + name.Name + "_" + name.Version + "_" + KResource, result);
        }
    }
}

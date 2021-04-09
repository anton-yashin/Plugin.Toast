using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using LightMock.Generator;
using Plugin.Toast;

namespace UnitTests.Mocks
{
    static class Declarations
    {
        static void Yield()
        {
            new Mock<IImageCacher>();
            new Mock<IUriToFileNameStrategy>();
            new Mock<IResourceToFileNameStrategy>();
            new Mock<IBundleToFileNameStrategy>();
            new Mock<IMimeDetector>();
            new Mock<IHttpClientFactory>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace ManualTests.Tests.Base
{
    public static class TestResolver
    {
        static readonly Type KAbstractTestType = typeof(IAbstractTest);

        public static IServiceCollection AddTests(this IServiceCollection sc)
        {
            foreach (var i in from i in typeof(TestResolver).Assembly.GetTypes()
                              from j in i.GetInterfaces()
                              where j == KAbstractTestType && i.IsAbstract == false
                              select i)
            {
                sc.Add(new ServiceDescriptor(KAbstractTestType, i, ServiceLifetime.Transient));
            }
            return sc;
        }
    }
}

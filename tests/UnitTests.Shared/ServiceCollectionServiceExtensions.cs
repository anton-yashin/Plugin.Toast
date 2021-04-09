using Microsoft.Extensions.DependencyInjection;
using System;

namespace UnitTests
{
    internal static class ServiceCollectionServiceExtensions
    {
        public static IServiceCollection AddMock<TMock>(this IServiceCollection services) where TMock : class
        {
            services.AddScoped((sp) => new LightMock.Generator.Mock<TMock>());
            services.AddScoped((sp) => sp.GetRequiredService<LightMock.Generator.Mock<TMock>>().Object);
            return services;
        }

        public static IServiceCollection AddMock<TMock>(this IServiceCollection services, Action<LightMock.Generator.Mock<TMock>> mockSetup) where TMock : class
        {
            services.AddScoped((sp) =>
            {
                var mock = new LightMock.Generator.Mock<TMock>();
                mockSetup(mock);
                return mock;
            });
            services.AddScoped((sp) => sp.GetRequiredService<LightMock.Generator.Mock<TMock>>().Object);
            return services;
        }


        public static IServiceCollection AddMock<TMock>(this IServiceCollection services, Action<IServiceProvider, LightMock.Generator.Mock<TMock>> mockSetup) where TMock : class
        {
            services.AddScoped((sp) =>
            {
                var mock = new LightMock.Generator.Mock<TMock>();
                mockSetup(sp, mock);
                return mock;
            });
            services.AddScoped((sp) => sp.GetRequiredService<LightMock.Generator.Mock<TMock>>().Object);
            return services;
        }


        public static IServiceCollection AddMock<T1, T2>(this IServiceCollection services)
            where T1 : class
            where T2 : class
        {
            return services.AddMock<T1>().AddMock<T2>();
        }

        public static IServiceCollection AddMock<T1, T2, T3>(this IServiceCollection services)
            where T1 : class
            where T2 : class
            where T3 : class
        {
            return services.AddMock<T1>().AddMock<T2>().AddMock<T3>();
        }

        public static IServiceCollection AddMock<T1, T2, T3, T4>(this IServiceCollection services)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
        {
            return services.AddMock<T1>().AddMock<T2>().AddMock<T3>().AddMock<T4>();
        }

        public static IServiceCollection AddMock<T1, T2, T3, T4, T5>(this IServiceCollection services)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
        {
            return services.AddMock<T1>().AddMock<T2>().AddMock<T3>().AddMock<T4>().AddMock<T5>();
        }

        public static IServiceCollection AddMock<T1, T2, T3, T4, T5, T6>(this IServiceCollection services)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
        {
            return services.AddMock<T1>().AddMock<T2>().AddMock<T3>().AddMock<T4>().AddMock<T5>()
                .AddMock<T6>();
        }

        public static IServiceCollection AddMock<T1, T2, T3, T4, T5, T6, T7>(this IServiceCollection services)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
        {
            return services.AddMock<T1>().AddMock<T2>().AddMock<T3>().AddMock<T4>().AddMock<T5>()
                .AddMock<T6>().AddMock<T7>();
        }

        public static IServiceCollection AddMock<T1, T2, T3, T4, T5, T6, T7, T8>(this IServiceCollection services)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
        {
            return services.AddMock<T1>().AddMock<T2>().AddMock<T3>().AddMock<T4>().AddMock<T5>()
                .AddMock<T6>().AddMock<T7>().AddMock<T8>();
        }

        public static IServiceCollection AddMock<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IServiceCollection services)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
        {
            return services.AddMock<T1>().AddMock<T2>().AddMock<T3>().AddMock<T4>().AddMock<T5>()
                .AddMock<T6>().AddMock<T7>().AddMock<T8>().AddMock<T9>();
        }

        public static IServiceCollection AddMock<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IServiceCollection services)
            where T1 : class
            where T2 : class
            where T3 : class
            where T4 : class
            where T5 : class
            where T6 : class
            where T7 : class
            where T8 : class
            where T9 : class
            where T10 : class
        {
            return services.AddMock<T1>().AddMock<T2>().AddMock<T3>().AddMock<T4>().AddMock<T5>()
                .AddMock<T6>().AddMock<T7>().AddMock<T8>().AddMock<T9>().AddMock<T10>();
        }

    }
}

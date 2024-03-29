﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using ManualTests.Tests.Base;
using ManualTests.Xamarin.Services;

namespace ManualTests
{
    public partial class App : Application
    {
        public static new App Current => (App)Application.Current;

        private readonly ServiceProvider serviceProvider;
        public IServiceProvider ServiceProvider => serviceProvider;

        public App(Action<IServiceCollection>? configurePlatformServices)
        {
            var sc = new ServiceCollection();
            configurePlatformServices?.Invoke(sc);
            sc.AddLogging(_ => _.AddDebug()).AddTests()
                .AddSingleton<IRuntimePlatform, RuntimePlatform>()
                .AddSingleton<ICommandFactory, CommandFactory>()
                .AddSingleton<ICommandUpdater, CommandUpdater>();
            this.serviceProvider = sc.BuildServiceProvider();
            DependencyResolver.ResolveUsing(ServiceProvider.GetService);

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

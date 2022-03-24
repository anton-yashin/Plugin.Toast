using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using ManualTests.Tests.Base;

namespace ManualTests
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        IServiceProvider serviceProvider;
        ILogger<MainPage> logger;

        public MainPage()
        {
            serviceProvider = App.Current.ServiceProvider;
            logger = serviceProvider.GetRequiredService<ILogger<MainPage>>();

            InitializeComponent();

            var tests = serviceProvider.GetServices<IAbstractTest>().ToArray();
            foreach (var i in tests.Where(t => t.IsAvailable == false))
                logger.LogInformation("test {i} is not available on current platform", i);

            testCollectionView.ItemsSource = tests.Where(t => t.IsAvailable).ToArray();
        }
    }
}

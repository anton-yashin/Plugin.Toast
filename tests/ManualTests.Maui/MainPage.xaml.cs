using ManualTests.Tests.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManualTests.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage(ILogger<MainPage> logger, IEnumerable<IAbstractTest> tests)
        {
            InitializeComponent();

            foreach (var i in tests.Where(t => t.IsAvailable == false))
                logger.LogInformation("test {i} is not available on current platform", i);

            testCollectionView.ItemsSource = tests.Where(t => t.IsAvailable).ToArray();
        }

    }
}

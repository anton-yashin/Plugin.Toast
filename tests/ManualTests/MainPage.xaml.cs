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
        List<IAbstractTest> tests;
        public MainPage()
        {
            serviceProvider = App.Current.ServiceProvider;
            logger = serviceProvider.GetService<ILogger<MainPage>>();

            InitializeComponent();

            var typeParams = new Type[] { typeof(IServiceProvider) };
            var @params = new object[] { serviceProvider };
            tests = new List<IAbstractTest>();
            foreach (var i in (from i in this.GetType().Assembly.GetTypes() 
                               from j in i.GetInterfaces()
                               where j == typeof(IAbstractTest) && i.IsAbstract == false
                               select i))
            {
                var constructor = i.GetConstructor(typeParams);
                if (constructor == null)
                {
                    logger.LogInformation("constructor for type {i} not found", i);
                }
                else
                {
                    var t = (IAbstractTest)constructor.Invoke(@params);
                    if (t.IsAvailable)
                        tests.Add(t);
                    else
                        logger.LogInformation("test {i} is not available on current platform", i);
                }
            }
            testCollectionView.ItemsSource = tests;
        }
    }
}

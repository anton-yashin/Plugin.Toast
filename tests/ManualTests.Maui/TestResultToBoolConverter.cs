using System;
using System.Globalization;
using ManualTests.Tests.Base;
using Microsoft.Maui.Controls;

namespace ManualTests.Maui
{
    public class TestResultToBoolConverter : IValueConverter
    {
        public TestResultToBoolConverter() { }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var p = parameter as string ?? throw new ArgumentException("parameter is required", nameof(parameter));
            if (value is TestResult == false)
                throw new ArgumentException("value must be " + nameof(TestResult));
            var v = (TestResult)value;
            return v.ToString() == p;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

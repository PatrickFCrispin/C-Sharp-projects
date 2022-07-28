using System;
using System.Globalization;
using Xamarin.Forms;

using static DetalhesAtivo.Models.SnapshotResponse;

namespace DetalhesAtivo
{
    public static class Extensions
    {
        public static bool IsValid(this Security security)
        {
            return !string.IsNullOrWhiteSpace(security.Symbol);
        }
    }

    class ValidSecurityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(parameter is View view)) { return null; }
            if (!(view.BindingContext is Security security)) { return null; }

            Application.Current.Resources.TryGetValue("RedColor", out var redColor);
            Application.Current.Resources.TryGetValue("GreenColor", out var greenColor);

            return security.IsValid() ? greenColor : redColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    class ValidSecurityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Security)?.IsValid() ?? false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

    class InvalidSecurityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (!(value as Security)?.IsValid()) ?? true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
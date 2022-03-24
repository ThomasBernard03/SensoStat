using System;
using System.Globalization;
using Xamarin.Forms;

namespace SensoStat.Mobile.Converters
{
    public class TextToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color color = Color.Default;
            if (value != null && value.ToString().Contains("?"))
            {
                color = Color.Blue;
            }
            else
            {
                color = Color.Green;
            }
            return color.ToHex();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Windows.Data;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Ui.Converters
{
    public class AutoKlasseEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (AutoKlasse)value;
        }
    }
}

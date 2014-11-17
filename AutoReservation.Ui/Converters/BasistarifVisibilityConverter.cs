using System;
using System.Windows.Data;
using AutoReservation.Common.DataTransferObjects;
using System.Windows;

namespace AutoReservation.Ui.Converters
{
    public class BasistarifVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((AutoKlasse)value == AutoKlasse.Luxusklasse)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

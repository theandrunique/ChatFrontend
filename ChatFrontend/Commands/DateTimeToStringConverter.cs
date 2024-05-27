using System;
using System.Globalization;
using System.Windows.Data;

namespace TaskManager_Noskov.Commands
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToString("dd/MM/yyyy");
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (DateTime.TryParse(value as string, out DateTime dateTime))
            {
                return dateTime;
            }

            return null;
        }
    }

}

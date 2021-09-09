namespace Haushaltsplan.Presentation.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime date)
            {
                return date.ToString("d", CultureInfo.CreateSpecificCulture("de-DE"));
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

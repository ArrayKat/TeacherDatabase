using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherDatabase.Converters
{
    internal class DateConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTime)
            {
                
                return new DateTimeOffset((DateTime)value, TimeSpan.Zero);
                
            }
            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is DateTimeOffset myDateTimeOffset)
            {
                return new DateTime(myDateTimeOffset.Year, myDateTimeOffset.Month, myDateTimeOffset.Day);
            }
            return null;
        }
    }
}

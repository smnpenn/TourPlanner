using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TourPlanner.UI.Service
{
    public class StarColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int rating && parameter is string starIndexString)
            {
                if (int.TryParse(starIndexString, out int starIndex))
                {
                    return rating >= starIndex ? Brushes.Yellow : Brushes.Gray;
                }
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}




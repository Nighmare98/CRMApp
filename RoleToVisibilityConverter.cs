using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CRMApp
{
    public class RoleToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Role role && parameter is string roleString)
            {
                Role requiredRole = (Role)Enum.Parse(typeof(Role), roleString);
                return role == requiredRole ? Visibility.Visible : Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

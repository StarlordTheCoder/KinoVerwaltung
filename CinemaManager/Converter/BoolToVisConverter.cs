using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CinemaManager.Converter
{
	//TODO Proper Implementation with tests and configurable True & False values
	public class BoolToVisConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var temp = value as bool?;

			if (temp.HasValue && temp.Value)
			{
				return Visibility.Visible;
			}

			return Visibility.Hidden;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var temp = value as Visibility?;

			if (temp.HasValue && temp.Value == Visibility.Visible)
			{
				return true;
			}

			return false;
		}
	}
}

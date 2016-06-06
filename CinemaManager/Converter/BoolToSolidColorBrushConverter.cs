// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CinemaManager.Converter
{
	/// <summary>
	///     Konfigurierbarer Boolean to Visibility Konverter.
	///     <see cref="ConvertBack" /> gibt immer false zurück, ausser value ist die gleiche Visibility wie <see cref="True" />
	/// </summary>
	public class BoolToSolidColorBrushConverter : IValueConverter
	{
		/// <summary>
		///     <see cref="SolidColorBrush" /> im Falle von true
		/// </summary>
		public Color True { get; set; }

		/// <summary>
		///     <see cref="SolidColorBrush" /> im Falle von false
		/// </summary>
		public Color False { get; set; }

		/// <summary>Konvertiert einen Wert. </summary>
		/// <returns>Ein konvertierter Wert.Wenn die Methode null zurückgibt, wird der gültige NULL-Wert verwendet.</returns>
		/// <param name="value">Der von der Bindungsquelle erzeugte Wert.</param>
		/// <param name="targetType">Der Typ der Bindungsziel-Eigenschaft.</param>
		/// <param name="parameter">Der zu verwendende Konverterparameter.</param>
		/// <param name="culture">Die im Konverter zu verwendende Kultur.</param>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var b = value as bool?;

			if (b.HasValue && b.Value)
			{
				return new SolidColorBrush(True);
			}

			return new SolidColorBrush(False);
		}

		/// <summary>Konvertiert einen Wert. </summary>
		/// <returns>Ein konvertierter Wert.Wenn die Methode null zurückgibt, wird der gültige NULL-Wert verwendet.</returns>
		/// <param name="value">Der Wert, der vom Bindungsziel erzeugt wird.</param>
		/// <param name="targetType">Der Typ, in den konvertiert werden soll.</param>
		/// <param name="parameter">Der zu verwendende Konverterparameter.</param>
		/// <param name="culture">Die im Konverter zu verwendende Kultur.</param>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
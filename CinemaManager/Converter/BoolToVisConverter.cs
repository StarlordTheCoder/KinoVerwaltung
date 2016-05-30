// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CinemaManager.Converter
{
	/// <summary>
	///     Konfigurierbarer Boolean to Visibility Konverter. 
	///     <see cref="ConvertBack"/> gibt immer false zurück, ausser value ist die gleiche Visibility wie <see cref="True"/>
	/// </summary>
	public class BoolToVisConverter : IValueConverter
	{
		/// <summary>
		///     Vibility im Falle von true
		///     Default: <see cref="Visibility.Visible"/>
		/// </summary>
		public Visibility True { get; set; } = Visibility.Visible;

		/// <summary>
		///     Visibility im Falle von false
		///     Default: <see cref="Visibility.Collapsed"/>
		/// </summary>
		public Visibility False { get; set; } = Visibility.Collapsed;

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
				return True;
			}

			return False;
		}

		/// <summary>Konvertiert einen Wert. </summary>
		/// <returns>Ein konvertierter Wert.Wenn die Methode null zurückgibt, wird der gültige NULL-Wert verwendet.</returns>
		/// <param name="value">Der Wert, der vom Bindungsziel erzeugt wird.</param>
		/// <param name="targetType">Der Typ, in den konvertiert werden soll.</param>
		/// <param name="parameter">Der zu verwendende Konverterparameter.</param>
		/// <param name="culture">Die im Konverter zu verwendende Kultur.</param>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var v = value as Visibility?;

			if (v.HasValue && v.Value == True)
			{
				return true;
			}

			return false;
		}
	}
}
// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using CinemaManager.Modules;

namespace CinemaManager.Filter
{
	/// <summary>
	///     Gibt dem Modul die Möglichkeit seine Filter zu konfigurieren.
	///     Die Filter können in der View angezeigt werden.
	/// </summary>
	/// <typeparam name="T">Das DTO, welches gefiltert wird</typeparam>
	public interface IFilterConfigurator<T>
	{
		/// <summary>
		///     Fügt einen StringFilter hinzu
		/// </summary>
		/// <returns>This</returns>
		IFilterConfigurator<T> StringFilter(string label, params Func<T, string>[] valueToCompareTo);

		/// <summary>
		///     Fügt einen Zahlen-Filter hinzu
		/// </summary>
		/// <returns>This</returns>
		IFilterConfigurator<T> NumberFilter(string label, params Func<T, int>[] valueToCompareTo);

		/// <summary>
		///     Fügt einen Datums-Filter hinzu
		/// </summary>
		/// <returns>This</returns>
		IFilterConfigurator<T> DateFilter(string label, params Func<T, DateTime?>[] valueToCompareTo);

		/// <summary>
		///     Fügt einen Modul-Filter hinzu
		/// </summary>
		/// <returns>This</returns>
		IFilterConfigurator<T> ComplexFilter<TM>(TM module, Func<TM, IEnumerable<T>> valueToCompareTo) where TM : IModule;

		/// <summary>
		///     Filtert die Daten
		/// </summary>
		/// <param name="data">Zu filternde Daten</param>
		/// <returns>Gefilterte Daten</returns>
		IEnumerable<T> FilterData(IEnumerable<T> data);

		/// <summary>
		///     Einer der Filter hat <see cref="IFilter{T}.FilterChanged" /> geworfen
		/// </summary>
		event EventHandler FilterChanged;
	}
}
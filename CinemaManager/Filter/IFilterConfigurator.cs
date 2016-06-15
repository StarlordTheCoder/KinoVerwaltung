// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CinemaManager.Filter
{
	/// <summary>
	///     Gibt dem Modul die Möglichkeit seine Filter zu konfigurieren.
	///     Die Filter könne in der View angezeigt werden.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IFilterConfigurator<T>
	{
		/// <summary>
		///     Fügt einen <see cref="IComplexFilter{T,TM}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		IFilterConfigurator<T> ComplexFilter(IFilter<T> filter);

		/// <summary>
		///     Fügt einen <see cref="IDateFilter{T}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		IFilterConfigurator<T> DateFilter(IFilter<T> filter);

		/// <summary>
		///     Fügt einen <see cref="IStringFilter{T}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		IFilterConfigurator<T> StringFilter(IFilter<T> filter);

		/// <summary>
		///     Fügt einen <see cref="INumberFilter{T}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		IFilterConfigurator<T> NumberFilter(IFilter<T> filter);

		/// <summary>
		///     Einer der Filter hat <see cref="IFilter{T}.FilterChanged" /> geworfen
		/// </summary>
		event EventHandler FilterChanged;

		/// <summary>
		///     Filter die Daten
		/// </summary>
		/// <param name="data">Zu filternde Daten</param>
		/// <returns>Gefilterte Daten</returns>
		IEnumerable<T> FilterData(IEnumerable<T> data);
	}
}
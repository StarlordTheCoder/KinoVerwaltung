// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using CinemaManager.Filter.Complex;
using CinemaManager.Filter.Date;
using CinemaManager.Filter.Number;
using CinemaManager.Filter.String;
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
		///     Ruft <see cref="StringFilter(IStringFilter{T})" /> auf
		/// </summary>
		/// <returns>This</returns>
		IFilterConfigurator<T> StringFilter(string label, params Func<T, string>[] valueToCompareTo);

		/// <summary>
		///     Fügt einen <see cref="IStringFilter{T}" /> hinzu
		/// </summary>
		/// <returns>This</returns>
		IFilterConfigurator<T> StringFilter(IStringFilter<T> filter);


		/// <summary>
		///     Ruft <see cref="NumberFilter(INumberFilter{T})" /> auf
		/// </summary>
		/// <returns>This</returns>
		IFilterConfigurator<T> NumberFilter(string label, params Func<T, int>[] valueToCompareTo);

		/// <summary>
		///     Fügt einen <see cref="INumberFilter{T}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		IFilterConfigurator<T> NumberFilter(INumberFilter<T> filter);


		/// <summary>
		///     Ruft <see cref="DateFilter(IDateFilter{T})" /> auf
		/// </summary>
		/// <returns>This</returns>
		IFilterConfigurator<T> DateFilter(string label, params Func<T, DateTime?>[] valueToCompareTo);

		/// <summary>
		///     Fügt einen <see cref="IDateFilter{T}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		IFilterConfigurator<T> DateFilter(IDateFilter<T> filter);


		/// <summary>
		///     Ruft <see cref="ComplexFilter{TM}(IComplexFilter{T,TM})" /> auf
		/// </summary>
		/// <returns>This</returns>
		IFilterConfigurator<T> ComplexFilter<TM>(TM module, Func<TM, IEnumerable<T>> valueToCompareTo) where TM : IModule;

		/// <summary>
		///     Fügt einen <see cref="IComplexFilter{T,TM}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		IFilterConfigurator<T> ComplexFilter<TM>(IComplexFilter<T, TM> filter) where TM : IModule;

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
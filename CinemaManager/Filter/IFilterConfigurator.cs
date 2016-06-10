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
		ObservableCollection<IFilter<T>> ComplexFilters { get; }
		ObservableCollection<IDateFilter<T>> DateFilters { get; }
		ObservableCollection<IStringFilter<T>> StringFilters { get; }

		IFilterConfigurator<T> ComplexFilter(IFilter<T> filter);
		IFilterConfigurator<T> DateFilter(IDateFilter<T> filter);
		IFilterConfigurator<T> StringFilter(IStringFilter<T> filter);

		event EventHandler FilterChanged;

		IEnumerable<T> FilterData(IEnumerable<T> data);
	}
}
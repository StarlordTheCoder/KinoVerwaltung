// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CinemaManager.Filter
{
	public interface IFilterConfigurator<T>
	{
		ObservableCollection<IFilter<T>> ComplexFilters { get; }
		ObservableCollection<IDateFilter<T>> DateFilters { get; }
		ObservableCollection<IStringFilter<T>> StringFilters { get; }

		IFilterConfigurator<T> ComplexFilter(IFilter<T> filter);
		IFilterConfigurator<T> DateFilter(IDateFilter<T> filter);
		IFilterConfigurator<T> StringFilter(IStringFilter<T> filter);

		IEnumerable<T> FilterData(IEnumerable<T> data);
	}
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CinemaManager.Model;

namespace CinemaManager.Filter
{
	public interface IFilterConfigurator<T>
	{
		ObservableCollection<IFilter<T>> ComplexFilters { get; }
		ObservableCollection<DateFilter<T>> DateFilters { get; }
		ObservableCollection<StringFilter<T>> StringFilters { get; }

		IFilterConfigurator<T> ComplexFilter(IFilter<T> filter);
		IFilterConfigurator<T> DateFilter(DateFilter<T> filter);
		IFilterConfigurator<T> StringFilter(StringFilter<T> filter);

		IEnumerable<T> FilterData(IEnumerable<T> data);
	}
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CinemaManager.Model;

namespace CinemaManager.Filter
{
	public class FilterConfigurator<T> : IFilterConfigurator<T>
	{
		public ObservableCollection<DateFilter<T>> DateFilters { get; } = new ObservableCollection<DateFilter<T>>();

		public ObservableCollection<StringFilter<T>> StringFilters { get; } = new ObservableCollection<StringFilter<T>>();

		public ObservableCollection<IFilter<T>> ComplexFilters { get; } = new ObservableCollection<IFilter<T>>();

		public IFilterConfigurator<T> StringFilter(StringFilter<T> filter)
		{
			StringFilters.Add(filter);

			return this;
		}

		public IFilterConfigurator<T> DateFilter(DateFilter<T> filter)
		{
			DateFilters.Add(filter);

			return this;
		}

		public IFilterConfigurator<T> ComplexFilter(IFilter<T> filter)
		{
			ComplexFilters.Add(filter);

			return this;
		}

		public IEnumerable<T> FilterData(IEnumerable<T> data)
		{
			var filters = DateFilters.Concat(ComplexFilters).Concat(StringFilters).Where(f => f.IsEnabled).ToList();

			foreach (var item in data)
			{
				if (filters.All(f => f.Check(item)))
				{
					yield return item;
				}
			}
		}
	}
}

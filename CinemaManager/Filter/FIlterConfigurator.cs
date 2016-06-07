// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CinemaManager.Filter
{
	public class FilterConfigurator<T> : IFilterConfigurator<T>
	{
		public ObservableCollection<IDateFilter<T>> DateFilters { get; } = new ObservableCollection<IDateFilter<T>>();

		public ObservableCollection<IStringFilter<T>> StringFilters { get; } = new ObservableCollection<IStringFilter<T>>();

		public ObservableCollection<IFilter<T>> ComplexFilters { get; } = new ObservableCollection<IFilter<T>>();


		public IFilterConfigurator<T> StringFilter(IStringFilter<T> filter)
		{
			StringFilters.Add(filter);

			filter.FilterChangedEvent += (sender, e) => OnFilterChanged();

			return this;
		}

		public event EventHandler FilterChanged;

		public IFilterConfigurator<T> DateFilter(IDateFilter<T> filter)
		{
			DateFilters.Add(filter);

			filter.FilterChangedEvent += (sender, e) => OnFilterChanged();

			return this;
		}

		public IFilterConfigurator<T> ComplexFilter(IFilter<T> filter)
		{
			ComplexFilters.Add(filter);

			filter.FilterChangedEvent += (sender, e) => OnFilterChanged();

			return this;
		}

		public IEnumerable<T> FilterData(IEnumerable<T> data)
		{
			var filters = DateFilters.Concat(ComplexFilters).Concat(StringFilters).Where(f => f.IsEnabled).ToList();

			if (filters.Any())
			{
				foreach (var item in data)
				{
					if (filters.All(f => f.Check(item)))
					{
						yield return item;
					}
				}
			}
			else
			{
				foreach (var i in data)
				{
					yield return i;
				}
			}
		}

		protected virtual void OnFilterChanged()
		{
			FilterChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
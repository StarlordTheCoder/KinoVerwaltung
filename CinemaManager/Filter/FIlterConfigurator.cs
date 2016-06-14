﻿// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using CinemaManager.Properties;

namespace CinemaManager.Filter
{
	public class FilterConfigurator<T> : IFilterConfigurator<T>, INotifyPropertyChanged
	{
		public GridLength FirstColumnWidth
			=> StringFilters.Any() ? new GridLength(1, GridUnitType.Star) : new GridLength(0, GridUnitType.Star);

		public GridLength SecondColumnWidth
			=> DateFilters.Any() ? new GridLength(1, GridUnitType.Star) : new GridLength(0, GridUnitType.Star);

		public GridLength ThirdColumnWidth
			=> ComplexFilters.Any() ? new GridLength(1, GridUnitType.Star) : new GridLength(0, GridUnitType.Star);

		/// <summary>
		///     List der <see cref="IDateFilter{T}" />
		/// </summary>
		public ObservableCollection<IFilter<T>> DateFilters { get; } = new ObservableCollection<IFilter<T>>();

		/// <summary>
		///     List der <see cref="IStringFilter{T}" />
		/// </summary>
		public ObservableCollection<IFilter<T>> StringFilters { get; } = new ObservableCollection<IFilter<T>>();

		/// <summary>
		///     List der <see cref="IComplexFilter{T,TM}" />
		/// </summary>
		public ObservableCollection<IFilter<T>> ComplexFilters { get; } = new ObservableCollection<IFilter<T>>();


		/// <summary>
		///     Fügt einen <see cref="IStringFilter{T}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		public IFilterConfigurator<T> StringFilter(IFilter<T> filter)
		{
			StringFilters.Add(filter);

			filter.FilterChanged += (sender, e) => OnFilterChanged();

			OnPropertyChanged(nameof(FirstColumnWidth));

			return this;
		}

		/// <summary>
		///     Einer der Filter hat <see cref="IFilter{T}.FilterChanged" /> geworfen
		/// </summary>
		public event EventHandler FilterChanged;

		/// <summary>
		///     Fügt einen <see cref="IDateFilter{T}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		public IFilterConfigurator<T> DateFilter(IFilter<T> filter)
		{
			DateFilters.Add(filter);

			filter.FilterChanged += (sender, e) => OnFilterChanged();

			OnPropertyChanged(nameof(SecondColumnWidth));

			return this;
		}

		/// <summary>
		///     Fügt einen <see cref="IComplexFilter{T,TM}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		public IFilterConfigurator<T> ComplexFilter(IFilter<T> filter)
		{
			ComplexFilters.Add(filter);

			filter.FilterChanged += (sender, e) => OnFilterChanged();

			OnPropertyChanged(nameof(ThirdColumnWidth));

			return this;
		}

		/// <summary>
		///     Filter die Daten
		/// </summary>
		/// <param name="data">Zu filternde Daten</param>
		/// <returns>Gefilterte Daten</returns>
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

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		///     Event invokator for <see cref="FilterChanged" />
		/// </summary>
		protected virtual void OnFilterChanged()
		{
			FilterChanged?.Invoke(this, EventArgs.Empty);
		}

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
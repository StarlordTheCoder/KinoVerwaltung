// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using CinemaManager.Filter.Complex;
using CinemaManager.Filter.Date;
using CinemaManager.Filter.Number;
using CinemaManager.Filter.String;
using CinemaManager.Infrastructure;
using CinemaManager.Modules;

namespace CinemaManager.Filter
{
	/// <summary>
	///     Implementation für <see cref="IFilterConfigurator{T}" />
	/// </summary>
	public class FilterConfigurator<T> : NotifyPropertyChangedBase, IFilterConfigurator<T>
	{
		/// <summary>
		///     Alle Filter in einer Liste kombiniert
		/// </summary>
		public IEnumerable<IFilter<T>> AllFilters
			=>
				NumberFilters.Concat<IFilter<T>>(DateFilters)
					.Concat(ComplexFilters)
					.Concat(StringFilters)
					.Where(f => f.IsEnabled);

		/// <summary>
		///     Ruft <see cref="StringFilter(IStringFilter{T})" /> auf
		/// </summary>
		/// <returns>This</returns>
		public IFilterConfigurator<T> StringFilter(string label, params Func<T, string>[] valueToCompareTo)
		{
			return StringFilter(new StringFilter<T>(label, valueToCompareTo));
		}


		/// <summary>
		///     Ruft <see cref="NumberFilter(INumberFilter{T})" /> auf
		/// </summary>
		/// <returns>This</returns>
		public IFilterConfigurator<T> NumberFilter(string label, params Func<T, int>[] valueToCompareTo)
		{
			return NumberFilter(new NumberFilter<T>(label, valueToCompareTo));
		}


		/// <summary>
		///     Ruft <see cref="DateFilter(IDateFilter{T})" /> auf
		/// </summary>
		/// <returns>This</returns>
		public IFilterConfigurator<T> DateFilter(string label, params Func<T, DateTime?>[] valueToCompareTo)
		{
			return DateFilter(new DateFilter<T>(label, valueToCompareTo));
		}

		/// <summary>
		///     Ruft <see cref="ComplexFilter{TM}(IComplexFilter{T,TM})" /> auf
		/// </summary>
		/// <returns>This</returns>
		public IFilterConfigurator<T> ComplexFilter<TM>(TM module, Func<TM, IEnumerable<T>> valueToCompareTo)
			where TM : IModule
		{
			return ComplexFilter(new ComplexFilter<T, TM>(module, valueToCompareTo));
		}

		/// <summary>
		///     Filtert die Daten
		/// </summary>
		/// <param name="data">Zu filternde Daten</param>
		/// <returns>Gefilterte Daten</returns>
		public IEnumerable<T> FilterData(IEnumerable<T> data)
		{
			if (AllFilters.Any())
			{
				foreach (var item in data)
				{
					if (AllFilters.All(f => f.Check(item)))
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

		/// <summary>
		///     Fügt einen <see cref="IStringFilter{T}" /> hinzu
		/// </summary>
		/// <returns>This</returns>
		public IFilterConfigurator<T> StringFilter(IStringFilter<T> filter)
		{
			filter.FilterChanged += (sender, e) => OnFilterChanged();

			StringFilters.Add(filter);

			OnPropertyChanged(nameof(StringColumnWidth));

			return this;
		}

		/// <summary>
		///     Fügt einen <see cref="IStringFilter{T}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		public IFilterConfigurator<T> NumberFilter(INumberFilter<T> filter)
		{
			NumberFilters.Add(filter);

			filter.FilterChanged += (sender, e) => OnFilterChanged();

			OnPropertyChanged(nameof(NumberColumnWidth));

			return this;
		}

		/// <summary>
		///     Fügt einen <see cref="IDateFilter{T}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		public IFilterConfigurator<T> DateFilter(IDateFilter<T> filter)
		{
			DateFilters.Add(filter);

			filter.FilterChanged += (sender, e) => OnFilterChanged();

			OnPropertyChanged(nameof(DateColumnWidth));

			return this;
		}


		/// <summary>
		///     Fügt einen <see cref="IComplexFilter{T,TM}" /> hinzu
		/// </summary>
		/// <param name="filter">Filter</param>
		/// <returns>This</returns>
		public IFilterConfigurator<T> ComplexFilter<TM>(IComplexFilter<T, TM> filter) where TM : IModule
		{
			ComplexFilters.Add((IComplexFilter<T, IModule>) filter);

			filter.FilterChanged += (sender, e) => OnFilterChanged();

			OnPropertyChanged(nameof(ComplexColumnWidth));

			return this;
		}

		#region Columns

		/// <summary>
		///     Die Spaltenbreite der <see cref="StringFilters" />
		/// </summary>
		public GridLength StringColumnWidth
			=> StringFilters.Any() ? new GridLength(1, GridUnitType.Star) : new GridLength(0, GridUnitType.Star);

		/// <summary>
		///     Die Spaltenbreite der <see cref="NumberFilters" />
		/// </summary>
		public GridLength NumberColumnWidth
			=> NumberFilters.Any() ? new GridLength(1, GridUnitType.Star) : new GridLength(0, GridUnitType.Star);

		/// <summary>
		///     Die Spaltenbreite der <see cref="DateFilters" />
		/// </summary>
		public GridLength DateColumnWidth
			=> DateFilters.Any() ? new GridLength(1, GridUnitType.Star) : new GridLength(0, GridUnitType.Star);

		/// <summary>
		///     Die Spaltenbreite der <see cref="ComplexFilters" />
		/// </summary>
		public GridLength ComplexColumnWidth
			=> ComplexFilters.Any() ? new GridLength(1, GridUnitType.Star) : new GridLength(0, GridUnitType.Star);

		#endregion

		#region ObservableCollections

		/// <summary>
		///     List der <see cref="IStringFilter{T}" />
		/// </summary>
		public ObservableCollection<IStringFilter<T>> StringFilters { get; } = new ObservableCollection<IStringFilter<T>>();

		/// <summary>
		///     List der <see cref="INumberFilter{T}" />
		/// </summary>
		public ObservableCollection<INumberFilter<T>> NumberFilters { get; } = new ObservableCollection<INumberFilter<T>>();

		/// <summary>
		///     List der <see cref="IDateFilter{T}" />
		/// </summary>
		public ObservableCollection<IDateFilter<T>> DateFilters { get; } = new ObservableCollection<IDateFilter<T>>();

		/// <summary>
		///     List der <see cref="IComplexFilter{T,TM}" />
		/// </summary>
		public ObservableCollection<IComplexFilter<T, IModule>> ComplexFilters { get; } =
			new ObservableCollection<IComplexFilter<T, IModule>>();

		#endregion

		#region FilterChanged

		/// <summary>
		///     Einer der Filter hat <see cref="IFilter{T}.FilterChanged" /> geworfen
		/// </summary>
		public event EventHandler FilterChanged;

		/// <summary>
		///     Event invokator for <see cref="FilterChanged" />
		/// </summary>
		private void OnFilterChanged()
		{
			FilterChanged?.Invoke(this, EventArgs.Empty);
		}

		#endregion
	}

	/// <summary>
	///     Dummy-Class for XAML
	/// </summary>
	public class FilterConfigurator : FilterConfigurator<object>
	{
	}
}
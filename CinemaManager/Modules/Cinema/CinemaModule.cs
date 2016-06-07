// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CinemaManager.Filter;
using CinemaManager.Model;

namespace CinemaManager.Modules.Cinema
{
	public class CinemaModule : ModuleBase
	{
		private CinemaModel _currentItem;

		public CinemaModule()
		{
			CinemaFilterConfigurator
				.StringFilter(new StringFilter<CinemaModel>("Name", c => c.Name, c => c.Address));

			CinemaFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();
		}

		public override string Title => "Cinema Manager";
		public override void Refresh()
		{
			_allCinemas.Clear();

			_allCinemas.AddRange(Session.Instance.DataModel.CinemasModel.Cinemas);

			FilterChanged();
		}

		public void FilterChanged()
		{
			var filteredData = CinemaFilterConfigurator.FilterData(_allCinemas);
			Cinemas.Clear();

			foreach (var cinema in filteredData)
			{
				Cinemas.Add(cinema);
			}
		}

		public IFilterConfigurator<CinemaModel> CinemaFilterConfigurator { get; } = new FilterConfigurator<CinemaModel>();
		public ObservableCollection<CinemaModel> Cinemas { get; } = new ObservableCollection<CinemaModel>();
		public List<CinemaModel> _allCinemas { get; } = new List<CinemaModel>();

		public CinemaModel CurrentItem
		{
			get { return _currentItem; }
			set
			{
				if (Equals(value, _currentItem)) return;
				_currentItem = value;
				OnPropertyChanged();
			}
		}
	}
}
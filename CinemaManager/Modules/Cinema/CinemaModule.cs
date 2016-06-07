// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CinemaManager.Filter;
using CinemaManager.Model;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.Cinema
{
	public class CinemaModule : ModuleBase
	{
		private CinemaModel _currentItem;

		public ICommand AddCinemaCommand { get; }
		public ICommand RemoveCinemaCommand { get; }

		public CinemaModule()
		{
			CinemaFilterConfigurator
				.StringFilter(new StringFilter<CinemaModel>("Name / Address", c => c.Name, c => c.Address));

			CinemaFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();

			AddCinemaCommand = new DelegateCommand(AddCinema);
			RemoveCinemaCommand = new DelegateCommand(RemoveCinema);

			Session.Instance.PrepareForSave += (sender, e) =>
			{
				var list = Session.Instance.DataModel.CinemasModel.Cinemas.ToList();

				list.Except(_allCinemas).ToList().ForEach(l => Session.Instance.DataModel.CinemasModel.Cinemas.Remove(l));

				Session.Instance.DataModel.CinemasModel.Cinemas.AddRange(_allCinemas.Except(list));
			};
		}

		private void RemoveCinema()
		{
			_allCinemas.Remove(CurrentItem);
			Cinemas.Remove(CurrentItem);
			CurrentItem = Cinemas.FirstOrDefault();
		}

		private void AddCinema()
		{
			var newCinema = new CinemaModel
			{
				IsActive = true,
				Address = "Hier",
				Name = "Beispiel Kino"
			};

			_allCinemas.Add(newCinema);
			Cinemas.Add(newCinema);

			CurrentItem = newCinema;
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
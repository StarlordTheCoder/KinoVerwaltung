﻿// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

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
		private CinemaModel _selectedCinema;

		public CinemaModule()
		{
			CinemaFilterConfigurator
				.StringFilter(new StringFilter<CinemaModel>("Name / Address", c => c.Name, c => c.Address));

			CinemaFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();

			AddCinemaCommand = new DelegateCommand(AddCinema);
			RemoveCinemaCommand = new DelegateCommand(RemoveCinema);
		}

		/// <summary>
		///     Command for <see cref="AddCinema" />
		/// </summary>
		public ICommand AddCinemaCommand { get; }

		/// <summary>
		///     Command for <see cref="RemoveCinema" />
		/// </summary>
		public ICommand RemoveCinemaCommand { get; }

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Cinema Manager";

		/// <summary>
		///     Filter-Konfigurator für die Kinos
		/// </summary>
		public IFilterConfigurator<CinemaModel> CinemaFilterConfigurator { get; } = new FilterConfigurator<CinemaModel>();

		public ObservableCollection<CinemaModel> Cinemas { get; } = new ObservableCollection<CinemaModel>();

		public CinemaModel SelectedCinema
		{
			get { return _selectedCinema; }
			set
			{
				if (Equals(value, _selectedCinema)) return;
				_selectedCinema = value;
				OnPropertyChanged();
			}
		}

		private void RemoveCinema()
		{
			CinemaModels.Remove(SelectedCinema);
			Cinemas.Remove(SelectedCinema);
			SelectedCinema = Cinemas.FirstOrDefault();
		}

		private void AddCinema()
		{
			var newCinema = new CinemaModel
			{
				Address = "Examplestreet 42",
				Name = $"Cinema #{CinemaModels.Count + 1}"
			};

			CinemaModels.Add(newCinema);
			Cinemas.Add(newCinema);

			SelectedCinema = newCinema;
		}

		/// <summary>
		///     Aktualisiert die Daten im Modul.
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		public override void Refresh()
		{
			FilterChanged();
		}

		private static IList<CinemaModel> CinemaModels => Session.Instance.DataModel.CinemasModel.Cinemas;

		private void FilterChanged()
		{
			var filteredData = CinemaFilterConfigurator.FilterData(CinemaModels);
			Cinemas.Clear();

			foreach (var cinema in filteredData)
			{
				Cinemas.Add(cinema);
			}
		}
	}
}
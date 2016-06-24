// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CinemaManager.Filter;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.Cinema
{
	/// <summary>
	/// Module of the Cinemas in the GUI
	/// </summary>
	public class CinemaModule : ModuleBase
	{
		private readonly Action<IModule> _refreshModules;
		private CinemaModel _selectedCinema;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="refreshModules"></param>
		public CinemaModule(Action<IModule> refreshModules)
		{
			_refreshModules = refreshModules;
			CinemaFilterConfigurator
				.StringFilter("Name / Address", c => c.Name, c => c.Address);

			CinemaFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();

			AddCinemaCommand = new DelegateCommand(AddCinema);
			RemoveCinemaCommand = new DelegateCommand(RemoveCinema, () => SelectedCinema != null);
		}

		/// <summary>
		///     Command for <see cref="AddCinema" />
		/// </summary>
		public ICommand AddCinemaCommand { get; }

		/// <summary>
		///     Command for <see cref="RemoveCinema" />
		/// </summary>
		public DelegateCommand RemoveCinemaCommand { get; }

		/// <summary>
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public override bool Enabled { get; } = true;

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Cinema Manager";

		/// <summary>
		///     Filter-Konfigurator für die Kinos
		/// </summary>
		public IFilterConfigurator<CinemaModel> CinemaFilterConfigurator { get; set; } = new FilterConfigurator<CinemaModel>();

		/// <summary>
		/// Liste der <see cref="CinemaModel"/>
		/// </summary>
		public ObservableCollection<CinemaModel> Cinemas { get; } = new ObservableCollection<CinemaModel>();

		/// <summary>
		/// Gibt das Ausgewählte Kino im Gui zurück
		/// </summary>
		public CinemaModel SelectedCinema
		{
			get { return _selectedCinema; }
			set
			{
				if (Equals(value, _selectedCinema)) return;
				_selectedCinema = value;
				OnPropertyChanged();
				RemoveCinemaCommand.RaiseCanExecuteChanged();

				_refreshModules.Invoke(this);
			}
		}

		private static IList<CinemaModel> CinemaModels => Session.Instance.DataModel.CinemasModel.Cinemas;

		/// <summary>
		/// Entfernt ein Kino
		/// </summary>
		public void RemoveCinema()
		{
			CinemaModels.Remove(SelectedCinema);
			Cinemas.Remove(SelectedCinema);
			SelectedCinema = Cinemas.FirstOrDefault();
		}

		/// <summary>
		/// Fügt ein neues Kino mit Defaultdaten hinzu
		/// </summary>
		public void AddCinema()
		{
			var newCinema = new CinemaModel
			{
				Address = "Examplestreet 42",
				Name = $"Cinema #{CinemaModels.Count + 1}",
				Films = new List<FilmModel>(),
				Presentations = new List<PresentationModel>(),
				Users = new List<UserModel>(),
				Rooms = new List<RoomModel>(),
				SeatTypes = new List<SeatType>(),
				IsActive = true
			};

			CinemaModels.Add(newCinema);
			Cinemas.Add(newCinema);

			if (SelectedCinema != null)
			{
				SelectedCinema.IsActive = false;
			}

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

		private void FilterChanged()
		{
			var filteredData = CinemaFilterConfigurator.FilterData(CinemaModels);
			Cinemas.Clear();

			foreach (var cinema in filteredData)
			{
				Cinemas.Add(cinema);
			}

			SelectedCinema = Cinemas.FirstOrDefault(c => c.IsActive);
		}
	}
}
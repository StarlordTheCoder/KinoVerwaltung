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

namespace CinemaManager.Modules.Film
{
	/// <summary>
	///     GUi module des Filmes
	/// </summary>
	public class FilmModule : ModuleBase
	{
		private FilmModel _selectedFilm;

		/// <summary>
		///     Constructor
		/// </summary>
		public FilmModule()
		{
			AddFilmCommand = new DelegateCommand(AddFilm);
			RemoveFilmCommand = new DelegateCommand(RemoveFilm, () => ValueSelected);

			FilmFilterConfigurator
				.StringFilter("Name", f => f.FilmName)
				.StringFilter("Director", f => f.Director, f => f.Publisher);

			FilmFilterConfigurator.FilterChanged += (sender, e) => OnFilterChanged();
		}

		/// <summary>
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public override bool Enabled => FilmModels != null;

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Filme";

		/// <summary>
		///     FilterKonfigurator
		/// </summary>
		public IFilterConfigurator<FilmModel> FilmFilterConfigurator { get; set; } = new FilterConfigurator<FilmModel>();

		/// <summary>
		///     Alle gefilterten Filme
		/// </summary>
		public ObservableCollection<FilmModel> Films { get; set; } = new ObservableCollection<FilmModel>();

		/// <summary>
		///     Ausgewählter Film
		/// </summary>
		public FilmModel SelectedFilm
		{
			get { return _selectedFilm; }
			set
			{
				if (Equals(_selectedFilm, value)) return;
				_selectedFilm = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(ValueSelected));
				OnModuleDataChanged();
				RemoveFilmCommand.RaiseCanExecuteChanged();
			}
		}

		/// <summary>
		///     Command für <see cref="AddFilm" />
		/// </summary>
		public ICommand AddFilmCommand { get; }

		/// <summary>
		///     Command für <see cref="RemoveFilm" />
		/// </summary>
		public DelegateCommand RemoveFilmCommand { get; }

		private static IList<FilmModel> FilmModels => Session.Instance.SelectedCinemaModel?.Films;

		/// <summary>
		///     Altersbeschränkung des Filmes
		/// </summary>
		public static IEnumerable<AgeRestriction> AgeRestrictions
			=> Enum.GetValues(typeof(AgeRestriction)).Cast<AgeRestriction>();

		/// <summary>
		///     Gibt zurück, ob ein FIl Ausgewählt ist
		/// </summary>
		public bool ValueSelected => SelectedFilm != null;

		/// <summary>
		///     Entfernt den ausgewählten film
		/// </summary>
		public void RemoveFilm()
		{
			FilmModels.Remove(SelectedFilm);
			Films.Remove(SelectedFilm);
			SelectedFilm = Films.FirstOrDefault();
		}

		/// <summary>
		///     Fügt einen neuen Film mit Dfaultdaten hinzu
		/// </summary>
		public void AddFilm()
		{
			var newFilm = new FilmModel
			{
				Director = "Director",
				FilmName = "Film #" + FilmModels.Count,
				FilmId = Films.Any() ? Films.Max(f => f.FilmId) + 1 : 1
			};

			Films.Add(newFilm);
			FilmModels.Add(newFilm);
			SelectedFilm = newFilm;
		}

		/// <summary>
		///     Aktualisiert die Daten im Modul.
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		public override void Refresh()
		{
			OnFilterChanged();
		}

		private void OnFilterChanged()
		{
			if (FilmModels != null)
			{
				var films = FilmFilterConfigurator.FilterData(FilmModels).ToList();

				Films.Clear();

				films.ForEach(f => Films.Add(f));
			}

			OnPropertyChanged(nameof(Enabled));
		}
	}
}
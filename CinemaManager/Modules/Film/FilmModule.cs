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
	public class FilmModule : ModuleBase
	{
		private FilmModel _selectedFilm;

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
		/// FilterKonfigurator
		/// </summary>
		public IFilterConfigurator<FilmModel> FilmFilterConfigurator { get; set; } = new FilterConfigurator<FilmModel>();

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

		public ICommand AddFilmCommand { get; }

		public DelegateCommand RemoveFilmCommand { get; }

		private static IList<FilmModel> FilmModels => Session.Instance.SelectedCinemaModel?.Films;

		public IEnumerable<AgeRestriction> AgeRestrictions => Enum.GetValues(typeof(AgeRestriction)).Cast<AgeRestriction>();

		public bool ValueSelected => SelectedFilm != null;

		public void RemoveFilm()
		{
			FilmModels.Remove(SelectedFilm);
			Films.Remove(SelectedFilm);
			SelectedFilm = Films.FirstOrDefault();
		}

		public void AddFilm()
		{
			var newFilm = new FilmModel
			{
				Director = "Director",
				FilmName = "Film #" + FilmModels.Count
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
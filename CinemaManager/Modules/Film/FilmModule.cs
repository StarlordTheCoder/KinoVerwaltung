// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CinemaManager.Filter;
using CinemaManager.Filter.String;
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

			FilterConfigurator
				.StringFilter(new StringFilter<FilmModel>("Name", f => f.FilmName))
				.StringFilter(new StringFilter<FilmModel>("Director", f => f.Director, f => f.Publisher));

			FilterConfigurator.FilterChanged += (sender, e) => OnFilterChanged();
		}

		public override bool Enabled => FilmModels != null;

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Filme";

		public IFilterConfigurator<FilmModel> FilterConfigurator { get; } = new FilterConfigurator<FilmModel>();

		public ObservableCollection<FilmModel> FilmList { get; set; } = new ObservableCollection<FilmModel>();

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
				RemoveFilmCommand.RaiseCanExecuteChanged();
			}
		}

		public ICommand AddFilmCommand { get; }

		public DelegateCommand RemoveFilmCommand { get; }

		private static IList<FilmModel> FilmModels => Session.Instance.SelectedCinemaModel?.Films;

		public IEnumerable<AgeRestriction> AgeRestrictions => Enum.GetValues(typeof(AgeRestriction)).Cast<AgeRestriction>();

		public bool ValueSelected => SelectedFilm != null;

		private void RemoveFilm()
		{
			FilmModels.Remove(SelectedFilm);
			FilmList.Remove(SelectedFilm);
			SelectedFilm = FilmList.FirstOrDefault();
		}

		private void AddFilm()
		{
			var newFilm = new FilmModel
			{
				Director = "Director",
				FilmName = "Film #" + FilmModels.Count
			};

			FilmList.Add(newFilm);
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
				var films = FilterConfigurator.FilterData(FilmModels).ToList();

				FilmList.Clear();

				films.ForEach(f => FilmList.Add(f));
			}

			OnPropertyChanged(nameof(Enabled));
		}
	}
}
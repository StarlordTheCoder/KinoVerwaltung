// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

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
			RemoveFilmCommand = new DelegateCommand(RemoveFilm);

			FilterConfigurator
				.StringFilter(new StringFilter<FilmModel>("Name", f => f.FilmName))
				.StringFilter(new StringFilter<FilmModel>("Director", f => f.Director, f => f.Publisher));

			FilterConfigurator.FilterChanged += (sender, e) => OnFilterChanged();
		}

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
			}
		}

		public ICommand AddFilmCommand { get; private set; }

		public ICommand RemoveFilmCommand { get; private set; }

		private static CinemaModel CinemaModel => Session.Instance.SelectedCinemaModel;

		private void RemoveFilm()
		{
			FilmList.Remove(SelectedFilm);
			CinemaModel.Films.Remove(SelectedFilm);
			SelectedFilm = FilmList.FirstOrDefault();
		}

		private void AddFilm()
		{
			var newFilm = new FilmModel
			{
				Director = "Director",
				FilmName = "Film #" + CinemaModel.Films.Count
			};

			FilmList.Add(newFilm);
			CinemaModel.Films.Add(newFilm);
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
			if (CinemaModel != null)
			{
				var films = FilterConfigurator.FilterData(CinemaModel.Films).ToList();

				FilmList.Clear();

				films.ForEach(f => FilmList.Add(f));
			}
		}
	}
}
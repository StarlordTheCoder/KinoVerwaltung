// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Input;
using CinemaManager.Filter;
using CinemaManager.Model;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.Film
{
	public class FilmModule : ModuleBase
	{
		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Filme";

		public IFilterConfigurator<FilmModel> FilterConfigurator { get; } = new FilterConfigurator<FilmModel>();

		public ObservableCollection<FilmModel> FilmList { get; set; } = new ObservableCollection<FilmModel>();

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

		private CinemaModel CinemaModel;
		private FilmModel _selectedFilm;

		public FilmModule()
		{
			AddFilmCommand = new DelegateCommand(AddFilm);
			RemoveFilmCommand = new DelegateCommand(RemoveFilm);

			FilterConfigurator
				.StringFilter(new StringFilter<FilmModel>("Name", f => f.FilmName))
				.StringFilter(new StringFilter<FilmModel>("Director", f => f.Director, f => f.Publisher));

			FilterConfigurator.FilterChanged += (sender, e) => Refresh();
		}

		private void RemoveFilm()
		{
			FilmList.Remove(SelectedFilm);
			CinemaModel.Films.Remove(SelectedFilm);
			SelectedFilm = FilmList.FirstOrDefault();
		}

		private void AddFilm()
		{
			var newFilm = new FilmModel();
			newFilm.Director = "ExampleDirector";
			newFilm.FilmName = "Example Name";
			FilmList.Add(newFilm);
			CinemaModel.Films.Add(newFilm);
			SelectedFilm = newFilm;
		}

		public override void Refresh()
		{
			CinemaModel = Session.Instance.SelectedCinemaModel;

			var list = FilterConfigurator.FilterData(CinemaModel?.Films).ToList();

			FilmList.Clear();

			list.ForEach(l => FilmList.Add(l));
		}
	}
}
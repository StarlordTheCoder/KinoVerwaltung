// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using CinemaManager.Filter;
using CinemaManager.Model;

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

		public FilmModule()
		{
			FilterConfigurator.StringFilter(new StringFilter<FilmModel>("Schinkechäästoast", f => f.FilmName
					)).StringFilter(new StringFilter<FilmModel>("SchinkeToastChääs", f => f.Director, f => f.Publisher));
		}

		public override void Refresh()
		{
			var list = Session.Instance.SelectedCinemaModel?.Films;

			FilmList.Clear();
			if (list?.Any() ?? false)
			{
				list.ForEach(l => FilmList.Add(l));
			}
		}
	}
}
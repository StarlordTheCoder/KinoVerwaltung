// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.ObjectModel;
using CinemaManager.Model;

namespace CinemaManager.Modules.Film
{
	/// <summary>
	///     Interface for <see cref="FilmModule" />
	/// </summary>
	public interface IFilmModule : IModule
	{
		/// <summary>
		///     Alle gefilterten Filme
		/// </summary>
		ObservableCollection<FilmModel> Films { get; set; }

		/// <summary>
		///     Ausgewählter Film
		/// </summary>
		FilmModel SelectedFilm { get; set; }

		/// <summary>
		///     Fügt einen neuen Film mit Defaultdaten hinzu
		/// </summary>
		void AddFilm();

		/// <summary>
		///     Entfernt den ausgewählten film
		/// </summary>
		void RemoveFilm();
	}
}
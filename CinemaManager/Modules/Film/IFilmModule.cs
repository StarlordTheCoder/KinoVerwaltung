using System.Collections.ObjectModel;
using System.Windows.Input;
using CinemaManager.Filter;
using CinemaManager.Model;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.Film
{
	/// <summary>
	///     Interface for <see cref="FilmModule"/>
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
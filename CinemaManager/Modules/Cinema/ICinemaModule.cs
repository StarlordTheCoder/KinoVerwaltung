using System.Collections.ObjectModel;
using CinemaManager.Model;

namespace CinemaManager.Modules.Cinema
{
	/// <summary>
	///     Interface for <see cref="CinemaModule"/>
	/// </summary>
	public interface ICinemaModule : IModule
	{
		/// <summary>
		///     Liste der <see cref="CinemaModel" />
		/// </summary>
		ObservableCollection<CinemaModel> Cinemas { get; }
		/// <summary>
		///     Gibt das Ausgewählte Kino im Gui zurück
		/// </summary>
		CinemaModel SelectedCinema { get; set; }
		/// <summary>
		///     Fügt ein neues Kino mit Defaultdaten hinzu
		/// </summary>

		void AddCinema();
	}
}
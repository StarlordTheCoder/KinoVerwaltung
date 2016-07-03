using System.Collections.ObjectModel;

namespace CinemaManager.Modules.Room
{
	/// <summary>
	///     Interface for <see cref="RoomModule"/>
	/// </summary>
	public interface IRoomModule : IModule
	{/// <summary>
	 ///     Der ausgewählte Sitz
	 /// </summary>
		SeatViewModel SelectedSeat { get; set; }
		/// <summary>
		///     Alle gefilterten Räume
		/// </summary>
		ObservableCollection<RoomViewModel> Rooms { get; }
		/// <summary>
		///     Ausgewählter Raum
		/// </summary>
		RoomViewModel SelectedRoom { get; set; }
		/// <summary>
		///     Fügt einen neuen Raum hinzu
		/// </summary>
		void AddRoom();
		/// <summary>
		///     Entfernt den ausgewählten Raum
		/// </summary>
		void RemoveRoom();
	}
}
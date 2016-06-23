using System.Collections.ObjectModel;
using CinemaManager.Model;

namespace CinemaManager.Modules.Room
{
	public interface IRoomViewModel
	{
		RoomModel Model { get; set; }
		ObservableCollection<RowViewModel> Rows { get; }
		RowViewModel SelectedRow { get; set; }
		ObservableCollection<SeatViewModel> SelectedSeats { get; }

		void AddRow();
		void AddSeat();
		void RemoveRow();
		void RemoveSeat();
	}
}
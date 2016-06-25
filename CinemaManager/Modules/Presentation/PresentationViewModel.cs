using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Room;

namespace CinemaManager.Modules.Presentation
{
	public class PresentationViewModel
	{
		public PresentationViewModel(PresentationModel model)
		{
			Model = model;
		}

		public FilmModel Film => Cinema.Films.FirstOrDefault(f => f.FilmId == Model.FilmId);

		public RoomViewModel RoomViewModel
		{
			get
			{
				var roomModel = Cinema.Rooms.FirstOrDefault(r => r.RoomNumber == Model.RoomNumber);

				return roomModel != null ? new RoomViewModel(roomModel) : null;
			}
		}

		private static CinemaModel Cinema => Session.Instance.SelectedCinemaModel;

		public PresentationModel Model { get; }
	}
}

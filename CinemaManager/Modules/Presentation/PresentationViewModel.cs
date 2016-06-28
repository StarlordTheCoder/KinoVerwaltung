// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Room;

namespace CinemaManager.Modules.Presentation
{
	/// <summary>
	///     ViewModel für <see cref="PresentationModel" />
	/// </summary>
	public class PresentationViewModel : NotifyPropertyChangedBase
	{
		private RoomViewModel _roomViewModel;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="model">
		///     <see cref="Model" />
		/// </param>
		public PresentationViewModel(PresentationModel model)
		{
			Model = model;
		}

		/// <summary>
		///     Der Film, welcher gespielt wird
		/// </summary>
		public FilmModel Film
		{
			get { return Cinema.Films.FirstOrDefault(f => f.FilmId == Model.FilmId); }
			set
			{
				if (value == null || Equals(value.FilmId, Model.FilmId)) return;
				Model.FilmId = value.FilmId;
				OnPropertyChanged();
			}
		}

		/// <summary>
		///     Der Saal, in welchem diese Vorstellung ist
		/// </summary>
		public RoomViewModel RoomViewModel
		{
			get { return _roomViewModel ?? (_roomViewModel = CalculateRoomViewModel()); }
			set
			{
				if (value?.Model == null || Equals(value.Model.RoomNumber, Model.RoomNumber)) return;
				Model.RoomNumber = value.Model.RoomNumber;
				_roomViewModel = CalculateRoomViewModel();
				OnPropertyChanged();
			}
		}

		private static CinemaModel Cinema => Session.Instance.SelectedCinemaModel;


		/// <summary>
		///     Das Original-Model
		/// </summary>
		public PresentationModel Model { get; }

		private RoomViewModel CalculateRoomViewModel()
		{
			var roomModel = Cinema.Rooms.FirstOrDefault(r => r.RoomNumber == Model.RoomNumber);

			if (roomModel == null) return null;
			var roomViewModel = new RoomViewModel(roomModel, 1);

			foreach (var seat in roomViewModel.Rows.SelectMany(r => r.Seats))
			{
				seat.IsReserved = Model.Reservations.SelectMany(r => r.Seats).Contains(seat.Model.Place);
			}
			return roomViewModel;
		}
	}
}
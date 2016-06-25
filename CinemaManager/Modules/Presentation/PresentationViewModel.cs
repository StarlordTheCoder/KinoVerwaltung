// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Linq;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using CinemaManager.Modules.Room;

namespace CinemaManager.Modules.Presentation
{
	public class PresentationViewModel : NotifyPropertyChangedBase
	{
		public PresentationViewModel(PresentationModel model)
		{
			Model = model;
		}

		public FilmModel Film
		{
			get { return Cinema.Films.FirstOrDefault(f => f.FilmId == Model.FilmId); }
			set
			{
				if (Equals(value.FilmId, Model.FilmId)) return;
				Model.FilmId = value.FilmId;
				OnPropertyChanged();
			}
		}

		public RoomViewModel RoomViewModel
		{
			get
			{
				var roomModel = Cinema.Rooms.FirstOrDefault(r => r.RoomNumber == Model.RoomNumber);

				return roomModel != null ? new RoomViewModel(roomModel) : null;
			}
			set
			{
				if (Equals(value.Model.RoomNumber, Model.RoomNumber)) return;
				Model.RoomNumber = value.Model.RoomNumber;
				OnPropertyChanged();
			}
		}

		private static CinemaModel Cinema => Session.Instance.SelectedCinemaModel;

		public PresentationModel Model { get; }
	}
}
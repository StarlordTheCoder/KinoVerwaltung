// CinemaManager created by Seraphin, Pascal & Alain as a school project
// Copyright (c) 2016 All Rights Reserved

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CinemaManager.Filter;
using CinemaManager.Infrastructure;
using CinemaManager.Model;
using Microsoft.Practices.Prism.Commands;

namespace CinemaManager.Modules.Room
{
	public class RoomModule : ModuleBase
	{
		private RoomViewModel _selectedRoom;

		/// <summary>
		///     Manages The Gui of the Rooms
		/// </summary>
		public RoomModule()
		{
			AddRoomCommand = new DelegateCommand(AddRoom);
			RemoveRoomCommand = new DelegateCommand(RemoveRoom);
			AddRowCommand = new DelegateCommand(AddRow);
			RemoveRowCommand = new DelegateCommand(RemoveRow);
			AddSeatCommand = new DelegateCommand(AddSeat);
			RemoveSeatCommand = new DelegateCommand(RemoveSeat);

			RoomFilterConfigurator
				.NumberFilter("Room Number", c => c.RoomNumber);

			RoomFilterConfigurator.FilterChanged += (sender, e) => FilterChanged();
		}

		/// <summary>
		///     Raum Hunzufügen
		/// </summary>
		public ICommand AddRoomCommand { get; }

		/// <summary>
		///     Raum entfernen
		/// </summary>
		public DelegateCommand RemoveRoomCommand { get; }

		/// <summary>
		///     Sitzreie hinzufügen
		/// </summary>
		public DelegateCommand AddRowCommand { get; }

		/// <summary>
		///     Sizreihe Entfernen
		/// </summary>
		public DelegateCommand RemoveRowCommand { get; }

		/// <summary>
		///     Sitz hinzufügen
		/// </summary>
		public DelegateCommand AddSeatCommand { get; }

		/// <summary>
		///     Sitz entfernen
		/// </summary>
		public DelegateCommand RemoveSeatCommand { get; }

		/// <summary>
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public override bool Enabled => RoomModels != null;

		/// <summary>
		///     Shows if there is a selected Room
		/// </summary>
		public bool ValueSelected => SelectedRoom != null;

		/// <summary>
		///     Titel für das Dockingframework
		/// </summary>
		public override string Title => "Room Module";

		/// <summary>
		///     Alle gefilterten Räume
		/// </summary>
		public ObservableCollection<RoomViewModel> Rooms { get; } = new ObservableCollection<RoomViewModel>();

		/// <summary>
		///     Filter-Configurator für die Räume
		/// </summary>
		public IFilterConfigurator<RoomModel> RoomFilterConfigurator { get; set; } = new FilterConfigurator<RoomModel>();

		/// <summary>
		///     Ausgewählter Raum
		/// </summary>
		public RoomViewModel SelectedRoom
		{
			get { return _selectedRoom; }
			set
			{
				if (Equals(value, _selectedRoom)) return;
				_selectedRoom = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(ValueSelected));
			}
		}

		private static IList<RoomModel> RoomModels => Session.Instance.SelectedCinemaModel?.Rooms;

		/// <summary>
		///     Seattypes for showing in GUI
		/// </summary>
		public IEnumerable<SeatType> SeatTypes => Session.Instance.SelectedCinemaModel?.SeatTypes;

		/// <summary>
		///     Fügt einen neuen Raum hinzu
		/// </summary>
		public void AddRoom()
		{
			var room = new RoomModel
			{
				Seats = new List<SeatModel>(),
				RoomNumber = RoomModels.Any() ? RoomModels.Max(r => r.RoomNumber) + 1 : 1
			};
			RoomModels.Add(room);
			var roomRoomModel = new RoomViewModel(room);

			Rooms.Add(roomRoomModel);
			SelectedRoom = roomRoomModel;
		}

		/// <summary>
		///     Entfernt den ausgewählten Raum
		/// </summary>
		public void RemoveRoom()
		{
			RoomModels.Remove(SelectedRoom?.Model);
			Rooms.Remove(SelectedRoom);
			SelectedRoom = Rooms.FirstOrDefault();
		}

		/// <summary>
		///     Fügt eine neue Reihe hinzu
		/// </summary>
		public void AddRow()
		{
			SelectedRoom.AddRow();
		}

		/// <summary>
		///     Entfernt die Ausgewählte Reihe
		/// </summary>
		public void RemoveRow()
		{
			SelectedRoom.RemoveRow();
		}

		/// <summary>
		///     Fügt einen neuen Sitz hinzu
		/// </summary>
		public void AddSeat()
		{
			SelectedRoom.AddSeat();
		}

		/// <summary>
		///     Entfernt den ausgewählten Sitz
		/// </summary>
		public void RemoveSeat()
		{
			SelectedRoom.RemoveSeat();
		}


		/// <summary>
		///     Aktualisiert die Daten im Modul.
		///     Beispielsweise wenn sich die Daten verändert haben.
		/// </summary>
		public override void Refresh()
		{
			FilterChanged();
		}

		private void FilterChanged()
		{
			if (RoomModels != null)
			{
				var filteredData = RoomFilterConfigurator.FilterData(RoomModels);
				Rooms.Clear();

				foreach (var room in filteredData)
				{
					Rooms.Add(new RoomViewModel(room));
				}
			}

			OnPropertyChanged(nameof(Enabled));
		}
	}
}
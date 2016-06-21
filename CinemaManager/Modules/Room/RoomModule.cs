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
		/// <summary>
		/// Raum Hunzufügen
		/// </summary>
		public ICommand AddRoomCommand { get; }
		/// <summary>
		/// Raum entfernen
		/// </summary>
		public DelegateCommand RemoveRoomCommand { get; }
		/// <summary>
		/// Sitzreie hinzufügen
		/// </summary>
		public DelegateCommand AddRowCommand { get; }
		/// <summary>
		/// Sizreihe Entfernen
		/// </summary>
		public DelegateCommand RemoveRowCommand { get; }
		/// <summary>
		/// Sitz hinzufügen
		/// </summary>
		public DelegateCommand AddSeatCommand { get; }
		/// <summary>
		/// Sitz entfernen
		/// </summary>
		public DelegateCommand RemoveSeatCommand { get; }
		private RoomViewModel _selectedRoom;

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
		///     True, wenn das Modul aktiv ist.
		/// </summary>
		public override bool Enabled => RoomModels != null;

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
		public IFilterConfigurator<RoomModel> RoomFilterConfigurator { get; } = new FilterConfigurator<RoomModel>();

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

		public IEnumerable<SeatType> SeatTypes => Session.Instance.SelectedCinemaModel?.SeatTypes;

		private void AddRoom()
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

		private void RemoveRoom()
		{
			RoomModels.Remove(SelectedRoom.Model);
			Rooms.Remove(SelectedRoom);
			SelectedRoom = Rooms.FirstOrDefault();
		}

		private void AddRow()
		{
			//TODO: Add row
		}

		private void RemoveRow()
		{
			//TODO: Remove row
		}

		private void AddSeat()
		{
			//TODO: Add seat
		}

		private void RemoveSeat()
		{
			//TODO: remove Seat
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